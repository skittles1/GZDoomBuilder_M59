namespace CodeImp.DoomBuilder.Windows
{
	partial class TextureBrowserForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureBrowserForm));
			this.cancel = new System.Windows.Forms.Button();
			this.apply = new System.Windows.Forms.Button();
			this.smallimages = new System.Windows.Forms.ImageList(this.components);
			this.tvTextureSets = new CodeImp.DoomBuilder.GZBuilder.Controls.MultiSelectTreeview();
			this.browser = new CodeImp.DoomBuilder.Controls.ImageBrowserControl();
			this.splitter = new CodeImp.DoomBuilder.Controls.CollapsibleSplitter();
			this.panel = new System.Windows.Forms.Panel();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancel
			// 
			this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(683, 411);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(98, 25);
			this.cancel.TabIndex = 3;
			this.cancel.TabStop = false;
			this.cancel.Text = "Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// apply
			// 
			this.apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.apply.Location = new System.Drawing.Point(581, 411);
			this.apply.Name = "apply";
			this.apply.Size = new System.Drawing.Size(98, 25);
			this.apply.TabIndex = 2;
			this.apply.TabStop = false;
			this.apply.Text = "OK";
			this.apply.UseVisualStyleBackColor = true;
			this.apply.Click += new System.EventHandler(this.apply_Click);
			// 
			// smallimages
			// 
			this.smallimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallimages.ImageStream")));
			this.smallimages.TransparentColor = System.Drawing.Color.Transparent;
			this.smallimages.Images.SetKeyName(0, "KnownTextureSet2.ico");
			this.smallimages.Images.SetKeyName(1, "AllTextureSet2.ico");
			this.smallimages.Images.SetKeyName(2, "FileTextureSet.ico");
			this.smallimages.Images.SetKeyName(3, "FolderTextureSet.ico");
			this.smallimages.Images.SetKeyName(4, "PK3TextureSet.ico");
			this.smallimages.Images.SetKeyName(5, "WadTextureSet.png");
			this.smallimages.Images.SetKeyName(6, "FolderImage.png");
			this.smallimages.Images.SetKeyName(7, "ArchiveImage.png");
			this.smallimages.Images.SetKeyName(8, "TextLump.png");
			// 
			// tvTextureSets
			// 
			this.tvTextureSets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tvTextureSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tvTextureSets.HideSelection = false;
			this.tvTextureSets.ImageIndex = 0;
			this.tvTextureSets.ImageList = this.smallimages;
			this.tvTextureSets.Location = new System.Drawing.Point(0, 3);
			this.tvTextureSets.Name = "tvTextureSets";
			this.tvTextureSets.SelectedImageIndex = 0;
			this.tvTextureSets.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			this.tvTextureSets.SelectionMode = CodeImp.DoomBuilder.GZBuilder.Controls.TreeViewSelectionMode.SingleSelect;
			this.tvTextureSets.Size = new System.Drawing.Size(199, 402);
			this.tvTextureSets.TabIndex = 4;
			this.tvTextureSets.TabStop = false;
			this.tvTextureSets.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvTextureSets_KeyUp);
			this.tvTextureSets.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTextureSets_NodeMouseClick);
			// 
			// browser
			// 
			this.browser.BrowseFlats = false;
			this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.browser.HideInputBox = false;
			this.browser.Location = new System.Drawing.Point(0, 0);
			this.browser.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.browser.Name = "browser";
			this.browser.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.browser.PreventSelection = false;
			this.browser.Size = new System.Drawing.Size(574, 442);
			this.browser.TabIndex = 1;
			this.browser.TabStop = false;
			this.browser.SelectedItemDoubleClicked += new CodeImp.DoomBuilder.Controls.ImageBrowserControl.SelectedItemDoubleClickDelegate(this.browser_SelectedItemDoubleClicked);
			this.browser.SelectedItemChanged += new CodeImp.DoomBuilder.Controls.ImageBrowserControl.SelectedItemChangedDelegate(this.browser_SelectedItemChanged);
			// 
			// splitter
			// 
			this.splitter.AnimationDelay = 20;
			this.splitter.AnimationStep = 20;
			this.splitter.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
			this.splitter.ControlToHide = this.panel;
			this.splitter.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter.ExpandParentForm = false;
			this.splitter.Location = new System.Drawing.Point(574, 0);
			this.splitter.Margin = new System.Windows.Forms.Padding(0);
			this.splitter.MinSize = 100;
			this.splitter.Name = "collapsibleSplitter1";
			this.splitter.TabIndex = 5;
			this.splitter.TabStop = false;
			this.splitter.UseAnimations = false;
			this.splitter.VisualStyle = CodeImp.DoomBuilder.Controls.VisualStyles.DoubleDots;
			this.splitter.CollapsedChanged += new System.EventHandler(this.splitter_CollapsedChanged);
			// 
			// panel
			// 
			this.panel.Controls.Add(this.tvTextureSets);
			this.panel.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel.Location = new System.Drawing.Point(582, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(202, 442);
			this.panel.TabIndex = 6;
			// 
			// TextureBrowserForm
			// 
			this.AcceptButton = this.apply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(784, 442);
			this.Controls.Add(this.apply);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.browser);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.panel);
			this.MinimizeBox = false;
			this.Name = "TextureBrowserForm";
			this.Opacity = 1;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Browse Textures";
			this.Load += new System.EventHandler(this.TextureBrowserForm_Load);
			this.Shown += new System.EventHandler(this.TextureBrowserForm_Shown);
			this.Activated += new System.EventHandler(this.TextureBrowserForm_Activated);
			this.Move += new System.EventHandler(this.TextureBrowserForm_Move);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextureBrowserForm_FormClosing);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.TextureBrowserForm_HelpRequested);
			this.ResizeEnd += new System.EventHandler(this.TextureBrowserForm_ResizeEnd);
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private CodeImp.DoomBuilder.Controls.ImageBrowserControl browser;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Button apply;
		private System.Windows.Forms.ImageList smallimages;
		private CodeImp.DoomBuilder.GZBuilder.Controls.MultiSelectTreeview tvTextureSets;
		private CodeImp.DoomBuilder.Controls.CollapsibleSplitter splitter;
		private System.Windows.Forms.Panel panel;
	}
}