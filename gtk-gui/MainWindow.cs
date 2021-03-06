
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.HBox hbox1;
	
	private global::Gtk.Table table1;
	
	private global::Gtk.ComboBox albumList;
	
	private global::Gtk.Entry albumUser;
	
	private global::Gtk.Button checkAlbumsButton;
	
	private global::Gtk.Button downloadButton;
	
	private global::Gtk.Image imageLoading;
	
	private global::Gtk.FileChooserButton selectDestinationFolder;
	
	private global::Gtk.ProgressBar progressBar;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TextView console;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("FxPicasaWebAlbumDownloader");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.albumList = global::Gtk.ComboBox.NewText ();
		this.albumList.Name = "albumList";
		this.table1.Add (this.albumList);
		global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.albumList]));
		w1.TopAttach = ((uint)(1));
		w1.BottomAttach = ((uint)(2));
		w1.XOptions = ((global::Gtk.AttachOptions)(4));
		w1.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.albumUser = new global::Gtk.Entry ();
		this.albumUser.CanFocus = true;
		this.albumUser.Name = "albumUser";
		this.albumUser.Text = global::Mono.Unix.Catalog.GetString ("oefenschool.2c");
		this.albumUser.IsEditable = true;
		this.albumUser.InvisibleChar = '•';
		this.table1.Add (this.albumUser);
		global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.albumUser]));
		w2.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.checkAlbumsButton = new global::Gtk.Button ();
		this.checkAlbumsButton.CanFocus = true;
		this.checkAlbumsButton.Name = "checkAlbumsButton";
		this.checkAlbumsButton.UseUnderline = true;
		this.checkAlbumsButton.Label = global::Mono.Unix.Catalog.GetString ("Load Album List");
		this.table1.Add (this.checkAlbumsButton);
		global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkAlbumsButton]));
		w3.LeftAttach = ((uint)(1));
		w3.RightAttach = ((uint)(2));
		w3.XOptions = ((global::Gtk.AttachOptions)(4));
		w3.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.downloadButton = new global::Gtk.Button ();
		this.downloadButton.CanFocus = true;
		this.downloadButton.Name = "downloadButton";
		this.downloadButton.UseUnderline = true;
		this.downloadButton.Label = global::Mono.Unix.Catalog.GetString ("Download album(s)");
		this.table1.Add (this.downloadButton);
		global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.downloadButton]));
		w4.TopAttach = ((uint)(1));
		w4.BottomAttach = ((uint)(2));
		w4.LeftAttach = ((uint)(1));
		w4.RightAttach = ((uint)(2));
		w4.XOptions = ((global::Gtk.AttachOptions)(4));
		w4.YOptions = ((global::Gtk.AttachOptions)(4));
		this.hbox1.Add (this.table1);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.table1]));
		w5.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.imageLoading = new global::Gtk.Image ();
		this.imageLoading.Name = "imageLoading";
		this.hbox1.Add (this.imageLoading);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.imageLoading]));
		w6.Position = 1;
		w6.Expand = false;
		w6.Fill = false;
		this.vbox1.Add (this.hbox1);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
		w7.Position = 0;
		w7.Expand = false;
		w7.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.selectDestinationFolder = new global::Gtk.FileChooserButton (global::Mono.Unix.Catalog.GetString ("Select destination folder"), ((global::Gtk.FileChooserAction)(2)));
		this.selectDestinationFolder.Name = "selectDestinationFolder";
		this.vbox1.Add (this.selectDestinationFolder);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.selectDestinationFolder]));
		w8.Position = 1;
		w8.Expand = false;
		w8.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.progressBar = new global::Gtk.ProgressBar ();
		this.progressBar.Name = "progressBar";
		this.vbox1.Add (this.progressBar);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.progressBar]));
		w9.Position = 2;
		w9.Expand = false;
		w9.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.console = new global::Gtk.TextView ();
		this.console.CanFocus = true;
		this.console.Name = "console";
		this.GtkScrolledWindow.Add (this.console);
		this.vbox1.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
		w11.Position = 3;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 775;
		this.DefaultHeight = 535;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.downloadButton.Clicked += new global::System.EventHandler (this.OnDownloadButtonClicked);
		this.checkAlbumsButton.Clicked += new global::System.EventHandler (this.OnCheckAlbumsButtonClicked);
	}
}
