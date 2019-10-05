namespace TranslationScriptMaker
{
	partial class RawsViewerForm
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
			if ( disposing && (components != null) )
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawsViewerForm));
			this.InputGroupBox = new System.Windows.Forms.GroupBox();
			this.CurrentPageComboBox = new System.Windows.Forms.ComboBox();
			this.IsPageASpreadCheckBox = new System.Windows.Forms.CheckBox();
			this.PanelsWithSFXGroupBox = new System.Windows.Forms.GroupBox();
			this.Panel1CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel2CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel3CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel4CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel5CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel6CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel7CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel8CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel9CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel10CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel11CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel12CheckBox = new System.Windows.Forms.CheckBox();
			this.TotalPanelsLabel = new System.Windows.Forms.Label();
			this.TotalPanelsTextBox = new System.Windows.Forms.TextBox();
			this.NextImageButton = new System.Windows.Forms.Button();
			this.PreviousImageButton = new System.Windows.Forms.Button();
			this.InputPanel = new System.Windows.Forms.Panel();
			this.RawsViewerPanel = new System.Windows.Forms.Panel();
			this.ViewersSplitContainer = new System.Windows.Forms.SplitContainer();
			this.RawsViewerGroupBox = new System.Windows.Forms.GroupBox();
			this.RawsImageBox = new Cyotek.Windows.Forms.ImageBox();
			this.ScriptViewerGroupBox = new System.Windows.Forms.GroupBox();
			this.ScriptViewerRichTextBox = new System.Windows.Forms.RichTextBox();
			this.ScriptViewerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.InsertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SFXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BubbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TranslatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TranslatorToReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TranslatorToProofreaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TranslatorToTypesetterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ProofreadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ProofreaderToReaderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.ProofreaderToTranslatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ProofreaderToTypesetterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.InputGroupBox.SuspendLayout();
			this.PanelsWithSFXGroupBox.SuspendLayout();
			this.InputPanel.SuspendLayout();
			this.RawsViewerPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ViewersSplitContainer)).BeginInit();
			this.ViewersSplitContainer.Panel1.SuspendLayout();
			this.ViewersSplitContainer.Panel2.SuspendLayout();
			this.ViewersSplitContainer.SuspendLayout();
			this.RawsViewerGroupBox.SuspendLayout();
			this.ScriptViewerGroupBox.SuspendLayout();
			this.ScriptViewerContextMenuStrip.SuspendLayout();
			this.MainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// InputGroupBox
			// 
			this.InputGroupBox.Controls.Add(this.CurrentPageComboBox);
			this.InputGroupBox.Controls.Add(this.IsPageASpreadCheckBox);
			this.InputGroupBox.Controls.Add(this.PanelsWithSFXGroupBox);
			this.InputGroupBox.Controls.Add(this.TotalPanelsLabel);
			this.InputGroupBox.Controls.Add(this.TotalPanelsTextBox);
			this.InputGroupBox.Controls.Add(this.NextImageButton);
			this.InputGroupBox.Controls.Add(this.PreviousImageButton);
			this.InputGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InputGroupBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.InputGroupBox.Location = new System.Drawing.Point(0, 0);
			this.InputGroupBox.Name = "InputGroupBox";
			this.InputGroupBox.Size = new System.Drawing.Size(1137, 51);
			this.InputGroupBox.TabIndex = 2;
			this.InputGroupBox.TabStop = false;
			this.InputGroupBox.Text = "Input Controls";
			// 
			// CurrentPageComboBox
			// 
			this.CurrentPageComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.CurrentPageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CurrentPageComboBox.FormattingEnabled = true;
			this.CurrentPageComboBox.Location = new System.Drawing.Point(12, 21);
			this.CurrentPageComboBox.Name = "CurrentPageComboBox";
			this.CurrentPageComboBox.Size = new System.Drawing.Size(88, 21);
			this.CurrentPageComboBox.TabIndex = 7;
			this.CurrentPageComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CurrentPageComboBox_DrawItem);
			this.CurrentPageComboBox.SelectionChangeCommitted += new System.EventHandler(this.CurrentPageComboBox_SelectionChangeCommitted);
			// 
			// IsPageASpreadCheckBox
			// 
			this.IsPageASpreadCheckBox.AutoSize = true;
			this.IsPageASpreadCheckBox.Location = new System.Drawing.Point(869, 28);
			this.IsPageASpreadCheckBox.Name = "IsPageASpreadCheckBox";
			this.IsPageASpreadCheckBox.Size = new System.Drawing.Size(86, 17);
			this.IsPageASpreadCheckBox.TabIndex = 6;
			this.IsPageASpreadCheckBox.Text = "Is a Spread?";
			this.IsPageASpreadCheckBox.UseVisualStyleBackColor = true;
			this.IsPageASpreadCheckBox.CheckedChanged += new System.EventHandler(this.IsPageASpreadCheckBox_CheckedChanged);
			// 
			// PanelsWithSFXGroupBox
			// 
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel1CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel2CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel3CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel4CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel5CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel6CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel7CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel8CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel9CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel10CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel11CheckBox);
			this.PanelsWithSFXGroupBox.Controls.Add(this.Panel12CheckBox);
			this.PanelsWithSFXGroupBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.PanelsWithSFXGroupBox.Location = new System.Drawing.Point(391, 12);
			this.PanelsWithSFXGroupBox.Name = "PanelsWithSFXGroupBox";
			this.PanelsWithSFXGroupBox.Size = new System.Drawing.Size(471, 39);
			this.PanelsWithSFXGroupBox.TabIndex = 5;
			this.PanelsWithSFXGroupBox.TabStop = false;
			this.PanelsWithSFXGroupBox.Text = "Panels With SFX";
			// 
			// Panel1CheckBox
			// 
			this.Panel1CheckBox.AutoSize = true;
			this.Panel1CheckBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Panel1CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel1CheckBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.Panel1CheckBox.Location = new System.Drawing.Point(6, 16);
			this.Panel1CheckBox.Name = "Panel1CheckBox";
			this.Panel1CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel1CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel1CheckBox.TabIndex = 0;
			this.Panel1CheckBox.Text = "1";
			this.Panel1CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel1CheckBox.UseVisualStyleBackColor = false;
			this.Panel1CheckBox.Visible = false;
			this.Panel1CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel2CheckBox
			// 
			this.Panel2CheckBox.AutoSize = true;
			this.Panel2CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel2CheckBox.Location = new System.Drawing.Point(44, 16);
			this.Panel2CheckBox.Name = "Panel2CheckBox";
			this.Panel2CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel2CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel2CheckBox.TabIndex = 1;
			this.Panel2CheckBox.Text = "2";
			this.Panel2CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel2CheckBox.UseVisualStyleBackColor = true;
			this.Panel2CheckBox.Visible = false;
			this.Panel2CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel3CheckBox
			// 
			this.Panel3CheckBox.AutoSize = true;
			this.Panel3CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel3CheckBox.Location = new System.Drawing.Point(82, 16);
			this.Panel3CheckBox.Name = "Panel3CheckBox";
			this.Panel3CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel3CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel3CheckBox.TabIndex = 2;
			this.Panel3CheckBox.Text = "3";
			this.Panel3CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel3CheckBox.UseVisualStyleBackColor = true;
			this.Panel3CheckBox.Visible = false;
			this.Panel3CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel4CheckBox
			// 
			this.Panel4CheckBox.AutoSize = true;
			this.Panel4CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel4CheckBox.Location = new System.Drawing.Point(120, 16);
			this.Panel4CheckBox.Name = "Panel4CheckBox";
			this.Panel4CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel4CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel4CheckBox.TabIndex = 3;
			this.Panel4CheckBox.Text = "4";
			this.Panel4CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel4CheckBox.UseVisualStyleBackColor = true;
			this.Panel4CheckBox.Visible = false;
			this.Panel4CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel5CheckBox
			// 
			this.Panel5CheckBox.AutoSize = true;
			this.Panel5CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel5CheckBox.Location = new System.Drawing.Point(158, 16);
			this.Panel5CheckBox.Name = "Panel5CheckBox";
			this.Panel5CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel5CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel5CheckBox.TabIndex = 4;
			this.Panel5CheckBox.Text = "5";
			this.Panel5CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel5CheckBox.UseVisualStyleBackColor = true;
			this.Panel5CheckBox.Visible = false;
			this.Panel5CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel6CheckBox
			// 
			this.Panel6CheckBox.AutoSize = true;
			this.Panel6CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel6CheckBox.Location = new System.Drawing.Point(196, 16);
			this.Panel6CheckBox.Name = "Panel6CheckBox";
			this.Panel6CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel6CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel6CheckBox.TabIndex = 5;
			this.Panel6CheckBox.Text = "6";
			this.Panel6CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel6CheckBox.UseVisualStyleBackColor = true;
			this.Panel6CheckBox.Visible = false;
			this.Panel6CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel7CheckBox
			// 
			this.Panel7CheckBox.AutoSize = true;
			this.Panel7CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel7CheckBox.Location = new System.Drawing.Point(234, 16);
			this.Panel7CheckBox.Name = "Panel7CheckBox";
			this.Panel7CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel7CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel7CheckBox.TabIndex = 6;
			this.Panel7CheckBox.Text = "7";
			this.Panel7CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel7CheckBox.UseVisualStyleBackColor = true;
			this.Panel7CheckBox.Visible = false;
			this.Panel7CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel8CheckBox
			// 
			this.Panel8CheckBox.AutoSize = true;
			this.Panel8CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel8CheckBox.Location = new System.Drawing.Point(272, 16);
			this.Panel8CheckBox.Name = "Panel8CheckBox";
			this.Panel8CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel8CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel8CheckBox.TabIndex = 7;
			this.Panel8CheckBox.Text = "8";
			this.Panel8CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel8CheckBox.UseVisualStyleBackColor = true;
			this.Panel8CheckBox.Visible = false;
			this.Panel8CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel9CheckBox
			// 
			this.Panel9CheckBox.AutoSize = true;
			this.Panel9CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel9CheckBox.Location = new System.Drawing.Point(310, 16);
			this.Panel9CheckBox.Name = "Panel9CheckBox";
			this.Panel9CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel9CheckBox.Size = new System.Drawing.Size(32, 17);
			this.Panel9CheckBox.TabIndex = 8;
			this.Panel9CheckBox.Text = "9";
			this.Panel9CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel9CheckBox.UseVisualStyleBackColor = true;
			this.Panel9CheckBox.Visible = false;
			this.Panel9CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel10CheckBox
			// 
			this.Panel10CheckBox.AutoSize = true;
			this.Panel10CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel10CheckBox.Location = new System.Drawing.Point(348, 16);
			this.Panel10CheckBox.Name = "Panel10CheckBox";
			this.Panel10CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel10CheckBox.Size = new System.Drawing.Size(38, 17);
			this.Panel10CheckBox.TabIndex = 9;
			this.Panel10CheckBox.Text = "10";
			this.Panel10CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel10CheckBox.UseVisualStyleBackColor = true;
			this.Panel10CheckBox.Visible = false;
			this.Panel10CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel11CheckBox
			// 
			this.Panel11CheckBox.AutoSize = true;
			this.Panel11CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel11CheckBox.Location = new System.Drawing.Point(386, 16);
			this.Panel11CheckBox.Name = "Panel11CheckBox";
			this.Panel11CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel11CheckBox.Size = new System.Drawing.Size(38, 17);
			this.Panel11CheckBox.TabIndex = 10;
			this.Panel11CheckBox.Text = "11";
			this.Panel11CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel11CheckBox.UseVisualStyleBackColor = true;
			this.Panel11CheckBox.Visible = false;
			this.Panel11CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// Panel12CheckBox
			// 
			this.Panel12CheckBox.AutoSize = true;
			this.Panel12CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel12CheckBox.Location = new System.Drawing.Point(424, 16);
			this.Panel12CheckBox.Name = "Panel12CheckBox";
			this.Panel12CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel12CheckBox.Size = new System.Drawing.Size(38, 17);
			this.Panel12CheckBox.TabIndex = 12;
			this.Panel12CheckBox.Text = "12";
			this.Panel12CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Panel12CheckBox.UseVisualStyleBackColor = true;
			this.Panel12CheckBox.Visible = false;
			this.Panel12CheckBox.CheckedChanged += new System.EventHandler(this.PanelCheckBox_CheckedChanged);
			// 
			// TotalPanelsLabel
			// 
			this.TotalPanelsLabel.AutoSize = true;
			this.TotalPanelsLabel.Location = new System.Drawing.Point(268, 25);
			this.TotalPanelsLabel.Name = "TotalPanelsLabel";
			this.TotalPanelsLabel.Size = new System.Drawing.Size(69, 13);
			this.TotalPanelsLabel.TabIndex = 4;
			this.TotalPanelsLabel.Text = "Total Panels:";
			// 
			// TotalPanelsTextBox
			// 
			this.TotalPanelsTextBox.Location = new System.Drawing.Point(343, 22);
			this.TotalPanelsTextBox.MaxLength = 2;
			this.TotalPanelsTextBox.Name = "TotalPanelsTextBox";
			this.TotalPanelsTextBox.Size = new System.Drawing.Size(42, 20);
			this.TotalPanelsTextBox.TabIndex = 0;
			this.TotalPanelsTextBox.TextChanged += new System.EventHandler(this.TotalPanelsTextBox_TextChanged);
			this.TotalPanelsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TotalPanelsTextBox_KeyPress);
			// 
			// NextImageButton
			// 
			this.NextImageButton.BackColor = System.Drawing.SystemColors.Control;
			this.NextImageButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.NextImageButton.Location = new System.Drawing.Point(187, 20);
			this.NextImageButton.Name = "NextImageButton";
			this.NextImageButton.Size = new System.Drawing.Size(75, 23);
			this.NextImageButton.TabIndex = 2;
			this.NextImageButton.Text = "Next";
			this.NextImageButton.UseVisualStyleBackColor = false;
			this.NextImageButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NextImageButton_MouseClick);
			// 
			// PreviousImageButton
			// 
			this.PreviousImageButton.BackColor = System.Drawing.SystemColors.Control;
			this.PreviousImageButton.Enabled = false;
			this.PreviousImageButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.PreviousImageButton.Location = new System.Drawing.Point(106, 20);
			this.PreviousImageButton.Name = "PreviousImageButton";
			this.PreviousImageButton.Size = new System.Drawing.Size(75, 23);
			this.PreviousImageButton.TabIndex = 1;
			this.PreviousImageButton.Text = "Previous";
			this.PreviousImageButton.UseVisualStyleBackColor = false;
			this.PreviousImageButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PreviousImageButton_MouseClick);
			// 
			// InputPanel
			// 
			this.InputPanel.Controls.Add(this.InputGroupBox);
			this.InputPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.InputPanel.Location = new System.Drawing.Point(0, 0);
			this.InputPanel.Name = "InputPanel";
			this.InputPanel.Size = new System.Drawing.Size(1137, 51);
			this.InputPanel.TabIndex = 1;
			// 
			// RawsViewerPanel
			// 
			this.RawsViewerPanel.Controls.Add(this.ViewersSplitContainer);
			this.RawsViewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RawsViewerPanel.Location = new System.Drawing.Point(0, 51);
			this.RawsViewerPanel.Name = "RawsViewerPanel";
			this.RawsViewerPanel.Size = new System.Drawing.Size(1137, 423);
			this.RawsViewerPanel.TabIndex = 2;
			// 
			// ViewersSplitContainer
			// 
			this.ViewersSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ViewersSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.ViewersSplitContainer.Name = "ViewersSplitContainer";
			// 
			// ViewersSplitContainer.Panel1
			// 
			this.ViewersSplitContainer.Panel1.Controls.Add(this.RawsViewerGroupBox);
			// 
			// ViewersSplitContainer.Panel2
			// 
			this.ViewersSplitContainer.Panel2.Controls.Add(this.ScriptViewerGroupBox);
			this.ViewersSplitContainer.Size = new System.Drawing.Size(1137, 423);
			this.ViewersSplitContainer.SplitterDistance = 512;
			this.ViewersSplitContainer.TabIndex = 3;
			// 
			// RawsViewerGroupBox
			// 
			this.RawsViewerGroupBox.AutoSize = true;
			this.RawsViewerGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.RawsViewerGroupBox.Controls.Add(this.RawsImageBox);
			this.RawsViewerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RawsViewerGroupBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.RawsViewerGroupBox.Location = new System.Drawing.Point(0, 0);
			this.RawsViewerGroupBox.Name = "RawsViewerGroupBox";
			this.RawsViewerGroupBox.Size = new System.Drawing.Size(512, 423);
			this.RawsViewerGroupBox.TabIndex = 1;
			this.RawsViewerGroupBox.TabStop = false;
			this.RawsViewerGroupBox.Text = "Raws Viewer - Page: XXX / XXX";
			// 
			// RawsImageBox
			// 
			this.RawsImageBox.BackColor = System.Drawing.Color.DimGray;
			this.RawsImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RawsImageBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.RawsImageBox.GridColor = System.Drawing.Color.Gray;
			this.RawsImageBox.GridColorAlternate = System.Drawing.Color.DimGray;
			this.RawsImageBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			this.RawsImageBox.Location = new System.Drawing.Point(3, 16);
			this.RawsImageBox.Name = "RawsImageBox";
			this.RawsImageBox.Size = new System.Drawing.Size(506, 404);
			this.RawsImageBox.TabIndex = 0;
			this.RawsImageBox.ZoomChanged += new System.EventHandler(this.RawsImageBox_ZoomChanged);
			this.RawsImageBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.RawsImageBox_MouseWheel);
			// 
			// ScriptViewerGroupBox
			// 
			this.ScriptViewerGroupBox.Controls.Add(this.ScriptViewerRichTextBox);
			this.ScriptViewerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScriptViewerGroupBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.ScriptViewerGroupBox.Location = new System.Drawing.Point(0, 0);
			this.ScriptViewerGroupBox.Name = "ScriptViewerGroupBox";
			this.ScriptViewerGroupBox.Size = new System.Drawing.Size(621, 423);
			this.ScriptViewerGroupBox.TabIndex = 2;
			this.ScriptViewerGroupBox.TabStop = false;
			this.ScriptViewerGroupBox.Text = "Script Viewer - Page: XXX / XXX";
			// 
			// ScriptViewerRichTextBox
			// 
			this.ScriptViewerRichTextBox.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.ScriptViewerRichTextBox.ContextMenuStrip = this.ScriptViewerContextMenuStrip;
			this.ScriptViewerRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScriptViewerRichTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ScriptViewerRichTextBox.ForeColor = System.Drawing.SystemColors.Control;
			this.ScriptViewerRichTextBox.Location = new System.Drawing.Point(3, 16);
			this.ScriptViewerRichTextBox.Name = "ScriptViewerRichTextBox";
			this.ScriptViewerRichTextBox.Size = new System.Drawing.Size(615, 404);
			this.ScriptViewerRichTextBox.TabIndex = 0;
			this.ScriptViewerRichTextBox.Text = "";
			this.ScriptViewerRichTextBox.WordWrap = false;
			this.ScriptViewerRichTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScriptViewerRichTextBox_MouseClick);
			this.ScriptViewerRichTextBox.TextChanged += new System.EventHandler(this.ScriptViewerRichTextBox_TextChanged);
			this.ScriptViewerRichTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ScriptViewerRichTextBox_KeyUp);
			this.ScriptViewerRichTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScriptViewerRichTextBox_MouseUp);
			// 
			// ScriptViewerContextMenuStrip
			// 
			this.ScriptViewerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CutToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.DeleteToolStripMenuItem,
            this.InsertToolStripMenuItem});
			this.ScriptViewerContextMenuStrip.Name = "ScriptViewerContextMenuStrip";
			this.ScriptViewerContextMenuStrip.Size = new System.Drawing.Size(113, 114);
			// 
			// CutToolStripMenuItem
			// 
			this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
			this.CutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.CutToolStripMenuItem.Text = "Cut";
			this.CutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
			// 
			// CopyToolStripMenuItem
			// 
			this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
			this.CopyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.CopyToolStripMenuItem.Text = "Copy";
			this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
			// 
			// PasteToolStripMenuItem
			// 
			this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
			this.PasteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.PasteToolStripMenuItem.Text = "Paste";
			this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
			// 
			// DeleteToolStripMenuItem
			// 
			this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
			this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.DeleteToolStripMenuItem.Text = "Delete";
			this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
			// 
			// InsertToolStripMenuItem
			// 
			this.InsertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SFXToolStripMenuItem,
            this.BubbleToolStripMenuItem,
            this.NotesToolStripMenuItem});
			this.InsertToolStripMenuItem.Name = "InsertToolStripMenuItem";
			this.InsertToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.InsertToolStripMenuItem.Text = "Insert...";
			// 
			// SFXToolStripMenuItem
			// 
			this.SFXToolStripMenuItem.Name = "SFXToolStripMenuItem";
			this.SFXToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.SFXToolStripMenuItem.Text = "SFX";
			this.SFXToolStripMenuItem.Click += new System.EventHandler(this.SFXToolStripMenuItem_Click);
			// 
			// BubbleToolStripMenuItem
			// 
			this.BubbleToolStripMenuItem.Name = "BubbleToolStripMenuItem";
			this.BubbleToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.BubbleToolStripMenuItem.Text = "Bubble";
			this.BubbleToolStripMenuItem.Click += new System.EventHandler(this.BubbleToolStripMenuItem_Click);
			// 
			// NotesToolStripMenuItem
			// 
			this.NotesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TranslatorsToolStripMenuItem,
            this.ProofreadersToolStripMenuItem});
			this.NotesToolStripMenuItem.Name = "NotesToolStripMenuItem";
			this.NotesToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.NotesToolStripMenuItem.Text = "Notes...";
			// 
			// TranslatorsToolStripMenuItem
			// 
			this.TranslatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TranslatorToReaderToolStripMenuItem,
            this.TranslatorToProofreaderToolStripMenuItem,
            this.TranslatorToTypesetterToolStripMenuItem});
			this.TranslatorsToolStripMenuItem.Name = "TranslatorsToolStripMenuItem";
			this.TranslatorsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.TranslatorsToolStripMenuItem.Text = "Translator\'s...";
			// 
			// TranslatorToReaderToolStripMenuItem
			// 
			this.TranslatorToReaderToolStripMenuItem.Name = "TranslatorToReaderToolStripMenuItem";
			this.TranslatorToReaderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.TranslatorToReaderToolStripMenuItem.Text = "To Reader";
			this.TranslatorToReaderToolStripMenuItem.Click += new System.EventHandler(this.TranslatorToReaderToolStripMenuItem_Click);
			// 
			// TranslatorToProofreaderToolStripMenuItem
			// 
			this.TranslatorToProofreaderToolStripMenuItem.Name = "TranslatorToProofreaderToolStripMenuItem";
			this.TranslatorToProofreaderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.TranslatorToProofreaderToolStripMenuItem.Text = "To Proofreader";
			this.TranslatorToProofreaderToolStripMenuItem.Click += new System.EventHandler(this.TranslatorToProofreaderToolStripMenuItem_Click);
			// 
			// TranslatorToTypesetterToolStripMenuItem
			// 
			this.TranslatorToTypesetterToolStripMenuItem.Name = "TranslatorToTypesetterToolStripMenuItem";
			this.TranslatorToTypesetterToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.TranslatorToTypesetterToolStripMenuItem.Text = "To Typesetter";
			this.TranslatorToTypesetterToolStripMenuItem.Click += new System.EventHandler(this.TranslatorToTypesetterToolStripMenuItem_Click);
			// 
			// ProofreadersToolStripMenuItem
			// 
			this.ProofreadersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProofreaderToReaderToolStripMenuItem1,
            this.ProofreaderToTranslatorToolStripMenuItem,
            this.ProofreaderToTypesetterToolStripMenuItem1});
			this.ProofreadersToolStripMenuItem.Name = "ProofreadersToolStripMenuItem";
			this.ProofreadersToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.ProofreadersToolStripMenuItem.Text = "Proofreader\'s...";
			// 
			// ProofreaderToReaderToolStripMenuItem1
			// 
			this.ProofreaderToReaderToolStripMenuItem1.Name = "ProofreaderToReaderToolStripMenuItem1";
			this.ProofreaderToReaderToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
			this.ProofreaderToReaderToolStripMenuItem1.Text = "To Reader";
			this.ProofreaderToReaderToolStripMenuItem1.Click += new System.EventHandler(this.ProofreaderToReaderToolStripMenuItem_Click);
			// 
			// ProofreaderToTranslatorToolStripMenuItem
			// 
			this.ProofreaderToTranslatorToolStripMenuItem.Name = "ProofreaderToTranslatorToolStripMenuItem";
			this.ProofreaderToTranslatorToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
			this.ProofreaderToTranslatorToolStripMenuItem.Text = "To Translator";
			this.ProofreaderToTranslatorToolStripMenuItem.Click += new System.EventHandler(this.ProofreaderToTranslatorToolStripMenuItem_Click);
			// 
			// ProofreaderToTypesetterToolStripMenuItem1
			// 
			this.ProofreaderToTypesetterToolStripMenuItem1.Name = "ProofreaderToTypesetterToolStripMenuItem1";
			this.ProofreaderToTypesetterToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
			this.ProofreaderToTypesetterToolStripMenuItem1.Text = "To Typesetter";
			this.ProofreaderToTypesetterToolStripMenuItem1.Click += new System.EventHandler(this.ProofreaderToTypesetterToolStripMenuItem_Click);
			// 
			// MainPanel
			// 
			this.MainPanel.Controls.Add(this.RawsViewerPanel);
			this.MainPanel.Controls.Add(this.InputPanel);
			this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainPanel.Location = new System.Drawing.Point(0, 0);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(1137, 474);
			this.MainPanel.TabIndex = 3;
			// 
			// RawsViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(1137, 474);
			this.Controls.Add(this.MainPanel);
			this.ForeColor = System.Drawing.SystemColors.Control;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RawsViewerForm";
			this.Text = "Translation Script Maker";
			this.Load += new System.EventHandler(this.RawsViewerForm_Load);
			this.InputGroupBox.ResumeLayout(false);
			this.InputGroupBox.PerformLayout();
			this.PanelsWithSFXGroupBox.ResumeLayout(false);
			this.PanelsWithSFXGroupBox.PerformLayout();
			this.InputPanel.ResumeLayout(false);
			this.RawsViewerPanel.ResumeLayout(false);
			this.ViewersSplitContainer.Panel1.ResumeLayout(false);
			this.ViewersSplitContainer.Panel1.PerformLayout();
			this.ViewersSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ViewersSplitContainer)).EndInit();
			this.ViewersSplitContainer.ResumeLayout(false);
			this.RawsViewerGroupBox.ResumeLayout(false);
			this.ScriptViewerGroupBox.ResumeLayout(false);
			this.ScriptViewerContextMenuStrip.ResumeLayout(false);
			this.MainPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox InputGroupBox;
		private System.Windows.Forms.Panel InputPanel;
		private System.Windows.Forms.Panel RawsViewerPanel;
		private System.Windows.Forms.GroupBox RawsViewerGroupBox;
		private System.Windows.Forms.Button PreviousImageButton;
		private System.Windows.Forms.Button NextImageButton;
		private System.Windows.Forms.Label TotalPanelsLabel;
		private System.Windows.Forms.TextBox TotalPanelsTextBox;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.GroupBox PanelsWithSFXGroupBox;
		private System.Windows.Forms.CheckBox Panel1CheckBox;
		private System.Windows.Forms.CheckBox Panel12CheckBox;
		private System.Windows.Forms.CheckBox Panel11CheckBox;
		private System.Windows.Forms.CheckBox Panel10CheckBox;
		private System.Windows.Forms.CheckBox Panel9CheckBox;
		private System.Windows.Forms.CheckBox Panel8CheckBox;
		private System.Windows.Forms.CheckBox Panel7CheckBox;
		private System.Windows.Forms.CheckBox Panel6CheckBox;
		private System.Windows.Forms.CheckBox Panel5CheckBox;
		private System.Windows.Forms.CheckBox Panel4CheckBox;
		private System.Windows.Forms.CheckBox Panel3CheckBox;
		private System.Windows.Forms.CheckBox Panel2CheckBox;
		private System.Windows.Forms.CheckBox IsPageASpreadCheckBox;
        private System.Windows.Forms.GroupBox ScriptViewerGroupBox;
        private System.Windows.Forms.SplitContainer ViewersSplitContainer;
        private System.Windows.Forms.RichTextBox ScriptViewerRichTextBox;
		private Cyotek.Windows.Forms.ImageBox RawsImageBox;
		private System.Windows.Forms.ComboBox CurrentPageComboBox;
		private System.Windows.Forms.ContextMenuStrip ScriptViewerContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem InsertToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SFXToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem BubbleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem NotesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TranslatorsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TranslatorToReaderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TranslatorToProofreaderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TranslatorToTypesetterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ProofreadersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ProofreaderToReaderToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ProofreaderToTranslatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ProofreaderToTypesetterToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
	}
}