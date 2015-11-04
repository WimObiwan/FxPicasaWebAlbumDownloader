using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Gdk.PixbufAnimation animation = Gdk.PixbufAnimation.LoadFromResource ("FxPicasaWebAlbumDownloader.loading.gif");
		albumList.Sensitive = false;
		downloadButton.Sensitive = false;
		selectDestinationFolder.Sensitive = false;
		imageLoading.Animation = animation;
		imageLoading.Visible = false;
		progressBar.Visible = false;
		selectDestinationFolder.SetCurrentFolder (Environment.GetFolderPath (Environment.SpecialFolder.MyPictures));
		this.FocusChain = new Widget[] { albumUser, checkAlbumsButton, albumList, downloadButton, selectDestinationFolder };
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	private void ShowLogging (string str)
	{
		Gtk.Application.Invoke (delegate {
			TextIter iter = console.Buffer.EndIter;
			console.Buffer.Insert (ref iter, str + "\n");
			while (Application.EventsPending ())
				Application.RunIteration ();
			console.Buffer.MoveMark (console.Buffer.InsertMark, console.Buffer.EndIter);
			console.ScrollToMark (console.Buffer.InsertMark, 0.4,
				true, 0.0, 1.0);
		});
	}

	private void ShowLogging (string format, params object[] prm)
	{
		ShowLogging (string.Format (format, prm));
	}

	protected async void OnCheckAlbumsButtonClicked (object sender, EventArgs e)
	{
		try {
			Gtk.Application.Invoke (delegate {
				imageLoading.Visible = true;
				albumList.Clear ();
			});

			XmlDocument doc = new XmlDocument ();

			string username = albumUser.Text;
			string url = string.Format ("https://picasaweb.google.com/data/feed/api/user/{0}", username);
			//oefenschool.2c

			ShowLogging ("Loading album list from {0}", url);

			WebClient webClient = new WebClient ();
			byte[] buffer = await webClient.DownloadDataTaskAsync (url);

			doc.Load (new MemoryStream (buffer));
			var root = doc.DocumentElement;

			XmlNamespaceManager nsmgr = new XmlNamespaceManager (doc.NameTable);
			nsmgr.AddNamespace ("Atom", "http://www.w3.org/2005/Atom");
			nsmgr.AddNamespace ("gphoto", "http://schemas.google.com/photos/2007");

			ListStore listStore = new Gtk.ListStore (
				                      typeof(string), typeof(string));

			var nodes = root.SelectNodes ("/Atom:feed/Atom:entry", nsmgr);
			ShowLogging ("Found {0} albums", nodes.Count);

			listStore.AppendValues (null, "<All albums>");

			foreach (XmlNode albumXml in nodes) {
				//string id = albumXml.SelectSingleNode ("./Atom:id", nsmgr).InnerText;
				string title = albumXml.SelectSingleNode ("./Atom:title", nsmgr).InnerText;

				string id = albumXml.SelectSingleNode ("./gphoto:id", nsmgr).InnerText;
				//string xml = albumXml.InnerXML;
				string albumUrl = url + "/albumid/" + id;

				ShowLogging (title);
				listStore.AppendValues (albumUrl, title);
			}

			Gtk.Application.Invoke (delegate {
				albumList.Clear ();
				albumList.Model = listStore;
				CellRendererText text = new CellRendererText ();
				albumList.PackStart (text, false);
				albumList.AddAttribute (text, "text", 1); 
				albumList.Sensitive = true;
				TreeIter iter;
				albumList.Model.GetIterFirst (out iter);
				albumList.SetActiveIter (iter);
				downloadButton.Sensitive = true;
				selectDestinationFolder.Sensitive = true;
			});
		} catch (Exception x) {
			ShowLogging ("Exception happened: {0}", x);
			System.Diagnostics.Debug.Assert (false);
			Gtk.Application.Invoke (delegate {
				albumList.Clear ();
				albumList.Sensitive = false;
				downloadButton.Sensitive = false;
			});
		} finally {
			Gtk.Application.Invoke (delegate {
				imageLoading.Visible = false;
			});
			ShowLogging ("Done.");
		}
	}

	protected async void OnDownloadButtonClicked (object sender, EventArgs e)
	{
		TreeIter iter;
		if (albumList.GetActiveIter (out iter)) {
			string url = (string)albumList.Model.GetValue (iter, 0);
			string title = (string)albumList.Model.GetValue (iter, 1);
			// https://picasaweb.google.com/data/entry/api/user/103900829694648025501/albumid/6195827230188879713
			// https://picasaweb.google.com/data/feed/api/user/oefenschool.2c/albumid/6195827230188879713

			if (url != null) {
				await DownloadAlbum (title, url, 0, 1);
			} else {
				TreeIter it;
				if (albumList.Model.GetIterFirst (out it)) {
					int totalCount = albumList.Model.IterNChildren () - 1;
					if (totalCount <= 0)
						totalCount = 1;
					int current = 0;
					do {
						url = (string)albumList.Model.GetValue (it, 0);
						title = (string)albumList.Model.GetValue (it, 1);
						if (url != null) {
							await DownloadAlbum (title, url, current, totalCount);
							current++;
						}
					} while (albumList.Model.IterNext (ref it));
				}
			}
		}
		ShowLogging ("Done.");
	}

	protected async Task DownloadAlbum (string title, string url, int currentGlobal, int totalGlobal)
	{
		try {
			ShowLogging ("Downloading photo's for album {0}", title);

			string username;
			Gtk.Application.Invoke (delegate {
				imageLoading.Visible = true;
				progressBar.Visible = true;
				username = albumUser.Text;
			});

			XmlDocument doc = new XmlDocument ();

			ShowLogging ("Loading photo list from {0}", url);

			doc.Load (url);
			var root = doc.DocumentElement;

			XmlNamespaceManager nsmgr = new XmlNamespaceManager (doc.NameTable);
			nsmgr.AddNamespace ("Atom", "http://www.w3.org/2005/Atom");
			nsmgr.AddNamespace ("gphoto", "http://schemas.google.com/photos/2007");
			nsmgr.AddNamespace ("media", "http://search.yahoo.com/mrss/");
			nsmgr.AddNamespace ("exif", "http://schemas.google.com/photos/exif/2007");

			string targetDirectory = selectDestinationFolder.CurrentFolder;
			if (!System.IO.Directory.Exists (targetDirectory))
				System.IO.Directory.CreateDirectory (targetDirectory);
			targetDirectory = System.IO.Path.Combine (targetDirectory, albumUser.Text);
			if (!System.IO.Directory.Exists (targetDirectory))
				System.IO.Directory.CreateDirectory (targetDirectory);
			string albumPath = System.IO.Path.Combine (targetDirectory, title);
			if (!System.IO.Directory.Exists (albumPath))
				System.IO.Directory.CreateDirectory (albumPath);

			Gtk.Application.Invoke (delegate {
				this.progressBar.Fraction = (double)currentGlobal / (double)totalGlobal;
			});

			var nodes = root.SelectNodes ("/Atom:feed/Atom:entry", nsmgr);
			int totalCount = nodes.Count;
			ShowLogging ("Found {0} photos", totalCount);
			ShowLogging ("Saving to {0}", albumPath);
			int current = 0;
			foreach (XmlNode albumXml in nodes) {
				//string id = albumXml.SelectSingleNode ("./Atom:id", nsmgr).InnerText;
				string photoTitle = albumXml.SelectSingleNode ("./Atom:title", nsmgr).InnerText;
				string contentUrl = albumXml.SelectSingleNode ("./Atom:content", nsmgr).Attributes.GetNamedItem ("src").InnerXml;
				//string exifTime = albumXml.SelectSingleNode ("./exif:tags/exif:time", nsmgr).InnerText;
				//UInt64 exifTimeMS = UInt64.Parse (exifTime);
				//DateTime exifTimeDT = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds (exifTimeMS);
				string width = albumXml.SelectSingleNode ("./gphoto:width", nsmgr).InnerText;
				//int widthN = int.Parse (width);

				int lastSlash = contentUrl.LastIndexOf ('/');
				if (lastSlash > 0) {
					contentUrl = contentUrl.Substring (0, lastSlash) + "/s" + width + contentUrl.Substring (lastSlash);
				}

				string photoPath = System.IO.Path.Combine (albumPath, photoTitle);

				ShowLogging ("Found photo {0}", photoTitle);
				ShowLogging ("Downloading from {0}", contentUrl);

				WebClient webClient = new WebClient ();
				await webClient.DownloadFileTaskAsync (contentUrl, photoPath);

				current++;
				Gtk.Application.Invoke (delegate {
					this.progressBar.Fraction = ((double)currentGlobal + (double)current / (double)totalCount) / (double)totalGlobal;
				});
			}
		} catch (Exception x) {
			ShowLogging ("Exception happened: {0}", x);
			System.Diagnostics.Debug.Assert (false);
		} finally {
			Gtk.Application.Invoke (delegate {
				imageLoading.Visible = false;
				progressBar.Visible = false;
			});
		}
	}

}
