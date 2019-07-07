namespace TranslationScriptMaker
{
    partial class MainForm
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
            if (disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.RawsLocationLabel = new System.Windows.Forms.Label();
			this.RawsLocationTextBox = new System.Windows.Forms.TextBox();
			this.RawsLocationButton = new System.Windows.Forms.Button();
			this.TranslatorNameLabel = new System.Windows.Forms.Label();
			this.TranslatorNameTextBox = new System.Windows.Forms.TextBox();
			this.BeginScriptCreationButton = new System.Windows.Forms.Button();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.ScriptCreatorTabPage = new System.Windows.Forms.TabPage();
			this.OutputToGroupBox = new System.Windows.Forms.GroupBox();
			this.OutputToCustomLocationRadioButton = new System.Windows.Forms.RadioButton();
			this.OutputWithRawsRadioButton = new System.Windows.Forms.RadioButton();
			this.OutputToChapterFolderRadioButton = new System.Windows.Forms.RadioButton();
			this.ChapterSelectionComboBox = new System.Windows.Forms.ComboBox();
			this.SeriesSelectionComboBox = new System.Windows.Forms.ComboBox();
			this.ChapterSelectionLabel = new System.Windows.Forms.Label();
			this.SeriesSelectionLabel = new System.Windows.Forms.Label();
			this.OutputLocationLabel = new System.Windows.Forms.Label();
			this.OutputLocationTextBox = new System.Windows.Forms.TextBox();
			this.OutputLocationButton = new System.Windows.Forms.Button();
			this.ScriptEditorTabPage = new System.Windows.Forms.TabPage();
			this.ScriptEditingRawsLocationLabel = new System.Windows.Forms.Label();
			this.ScriptEditingRawsLocationTextBox = new System.Windows.Forms.TextBox();
			this.ScriptEditingRawsLocationButton = new System.Windows.Forms.Button();
			this.BeginScriptEditingButton = new System.Windows.Forms.Button();
			this.ScriptLocationLabel = new System.Windows.Forms.Label();
			this.ScriptLocationTextBox = new System.Windows.Forms.TextBox();
			this.ScriptLocationButton = new System.Windows.Forms.Button();
			this.MainFormErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.MainTabControl.SuspendLayout();
			this.ScriptCreatorTabPage.SuspendLayout();
			this.OutputToGroupBox.SuspendLayout();
			this.ScriptEditorTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainFormErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// RawsLocationLabel
			// 
			resources.ApplyResources(this.RawsLocationLabel, "RawsLocationLabel");
			this.RawsLocationLabel.Name = "RawsLocationLabel";
			// 
			// RawsLocationTextBox
			// 
			resources.ApplyResources(this.RawsLocationTextBox, "RawsLocationTextBox");
			this.RawsLocationTextBox.Name = "RawsLocationTextBox";
			this.RawsLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.RawsLocationTextBox_Validating);
			// 
			// RawsLocationButton
			// 
			resources.ApplyResources(this.RawsLocationButton, "RawsLocationButton");
			this.RawsLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.RawsLocationButton.Name = "RawsLocationButton";
			this.RawsLocationButton.UseVisualStyleBackColor = true;
			this.RawsLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RawsLocationButton_MouseClick);
			// 
			// TranslatorNameLabel
			// 
			resources.ApplyResources(this.TranslatorNameLabel, "TranslatorNameLabel");
			this.TranslatorNameLabel.Name = "TranslatorNameLabel";
			// 
			// TranslatorNameTextBox
			// 
			resources.ApplyResources(this.TranslatorNameTextBox, "TranslatorNameTextBox");
			this.TranslatorNameTextBox.Name = "TranslatorNameTextBox";
			this.TranslatorNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TranslatorNameTextBox_Validating);
			this.TranslatorNameTextBox.Validated += new System.EventHandler(this.TranslatorNameTextBox_Validated);
			// 
			// BeginScriptCreationButton
			// 
			this.BeginScriptCreationButton.BackColor = System.Drawing.SystemColors.Control;
			this.BeginScriptCreationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			resources.ApplyResources(this.BeginScriptCreationButton, "BeginScriptCreationButton");
			this.BeginScriptCreationButton.Name = "BeginScriptCreationButton";
			this.BeginScriptCreationButton.UseVisualStyleBackColor = false;
			this.BeginScriptCreationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BeginScriptCreationButton_MouseClick);
			// 
			// MainTabControl
			// 
			this.MainTabControl.Controls.Add(this.ScriptCreatorTabPage);
			this.MainTabControl.Controls.Add(this.ScriptEditorTabPage);
			resources.ApplyResources(this.MainTabControl, "MainTabControl");
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			// 
			// ScriptCreatorTabPage
			// 
			this.ScriptCreatorTabPage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ScriptCreatorTabPage.Controls.Add(this.OutputToGroupBox);
			this.ScriptCreatorTabPage.Controls.Add(this.ChapterSelectionComboBox);
			this.ScriptCreatorTabPage.Controls.Add(this.SeriesSelectionComboBox);
			this.ScriptCreatorTabPage.Controls.Add(this.ChapterSelectionLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.SeriesSelectionLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.OutputLocationLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.OutputLocationTextBox);
			this.ScriptCreatorTabPage.Controls.Add(this.OutputLocationButton);
			this.ScriptCreatorTabPage.Controls.Add(this.BeginScriptCreationButton);
			this.ScriptCreatorTabPage.Controls.Add(this.RawsLocationLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.RawsLocationTextBox);
			this.ScriptCreatorTabPage.Controls.Add(this.TranslatorNameTextBox);
			this.ScriptCreatorTabPage.Controls.Add(this.RawsLocationButton);
			this.ScriptCreatorTabPage.Controls.Add(this.TranslatorNameLabel);
			this.ScriptCreatorTabPage.ForeColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.ScriptCreatorTabPage, "ScriptCreatorTabPage");
			this.ScriptCreatorTabPage.Name = "ScriptCreatorTabPage";
			// 
			// OutputToGroupBox
			// 
			this.OutputToGroupBox.Controls.Add(this.OutputToCustomLocationRadioButton);
			this.OutputToGroupBox.Controls.Add(this.OutputWithRawsRadioButton);
			this.OutputToGroupBox.Controls.Add(this.OutputToChapterFolderRadioButton);
			this.OutputToGroupBox.ForeColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.OutputToGroupBox, "OutputToGroupBox");
			this.OutputToGroupBox.Name = "OutputToGroupBox";
			this.OutputToGroupBox.TabStop = false;
			// 
			// OutputToCustomLocationRadioButton
			// 
			resources.ApplyResources(this.OutputToCustomLocationRadioButton, "OutputToCustomLocationRadioButton");
			this.OutputToCustomLocationRadioButton.Name = "OutputToCustomLocationRadioButton";
			this.OutputToCustomLocationRadioButton.TabStop = true;
			this.OutputToCustomLocationRadioButton.UseVisualStyleBackColor = true;
			this.OutputToCustomLocationRadioButton.CheckedChanged += new System.EventHandler(this.OutputToCustomLocationRadioButton_CheckedChanged);
			this.OutputToCustomLocationRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputToCustomLocationRadioButton_MouseClick);
			// 
			// OutputWithRawsRadioButton
			// 
			resources.ApplyResources(this.OutputWithRawsRadioButton, "OutputWithRawsRadioButton");
			this.OutputWithRawsRadioButton.Name = "OutputWithRawsRadioButton";
			this.OutputWithRawsRadioButton.TabStop = true;
			this.OutputWithRawsRadioButton.UseVisualStyleBackColor = true;
			this.OutputWithRawsRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputWithRawsRadioButton_MouseClick);
			// 
			// OutputToChapterFolderRadioButton
			// 
			resources.ApplyResources(this.OutputToChapterFolderRadioButton, "OutputToChapterFolderRadioButton");
			this.OutputToChapterFolderRadioButton.Name = "OutputToChapterFolderRadioButton";
			this.OutputToChapterFolderRadioButton.TabStop = true;
			this.OutputToChapterFolderRadioButton.UseVisualStyleBackColor = true;
			this.OutputToChapterFolderRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputToChapterFolderRadioButton_MouseClick);
			// 
			// ChapterSelectionComboBox
			// 
			this.ChapterSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ChapterSelectionComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.ChapterSelectionComboBox, "ChapterSelectionComboBox");
			this.ChapterSelectionComboBox.Name = "ChapterSelectionComboBox";
			this.ChapterSelectionComboBox.SelectionChangeCommitted += new System.EventHandler(this.ChapterSelectionComboBox_SelectionChangeCommitted);
			// 
			// SeriesSelectionComboBox
			// 
			this.SeriesSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SeriesSelectionComboBox.FormattingEnabled = true;
			resources.ApplyResources(this.SeriesSelectionComboBox, "SeriesSelectionComboBox");
			this.SeriesSelectionComboBox.Name = "SeriesSelectionComboBox";
			this.SeriesSelectionComboBox.SelectionChangeCommitted += new System.EventHandler(this.SeriesSelectionComboBox_SelectionChangeCommitted);
			// 
			// ChapterSelectionLabel
			// 
			resources.ApplyResources(this.ChapterSelectionLabel, "ChapterSelectionLabel");
			this.ChapterSelectionLabel.Name = "ChapterSelectionLabel";
			// 
			// SeriesSelectionLabel
			// 
			resources.ApplyResources(this.SeriesSelectionLabel, "SeriesSelectionLabel");
			this.SeriesSelectionLabel.Name = "SeriesSelectionLabel";
			// 
			// OutputLocationLabel
			// 
			resources.ApplyResources(this.OutputLocationLabel, "OutputLocationLabel");
			this.OutputLocationLabel.Name = "OutputLocationLabel";
			// 
			// OutputLocationTextBox
			// 
			resources.ApplyResources(this.OutputLocationTextBox, "OutputLocationTextBox");
			this.OutputLocationTextBox.Name = "OutputLocationTextBox";
			this.OutputLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OutputLocationTextBox_Validating);
			// 
			// OutputLocationButton
			// 
			resources.ApplyResources(this.OutputLocationButton, "OutputLocationButton");
			this.OutputLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.OutputLocationButton.Name = "OutputLocationButton";
			this.OutputLocationButton.UseVisualStyleBackColor = true;
			this.OutputLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputLocationButton_MouseClick);
			// 
			// ScriptEditorTabPage
			// 
			this.ScriptEditorTabPage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ScriptEditorTabPage.Controls.Add(this.ScriptEditingRawsLocationLabel);
			this.ScriptEditorTabPage.Controls.Add(this.ScriptEditingRawsLocationTextBox);
			this.ScriptEditorTabPage.Controls.Add(this.ScriptEditingRawsLocationButton);
			this.ScriptEditorTabPage.Controls.Add(this.BeginScriptEditingButton);
			this.ScriptEditorTabPage.Controls.Add(this.ScriptLocationLabel);
			this.ScriptEditorTabPage.Controls.Add(this.ScriptLocationTextBox);
			this.ScriptEditorTabPage.Controls.Add(this.ScriptLocationButton);
			this.ScriptEditorTabPage.ForeColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.ScriptEditorTabPage, "ScriptEditorTabPage");
			this.ScriptEditorTabPage.Name = "ScriptEditorTabPage";
			// 
			// ScriptEditingRawsLocationLabel
			// 
			resources.ApplyResources(this.ScriptEditingRawsLocationLabel, "ScriptEditingRawsLocationLabel");
			this.ScriptEditingRawsLocationLabel.Name = "ScriptEditingRawsLocationLabel";
			// 
			// ScriptEditingRawsLocationTextBox
			// 
			resources.ApplyResources(this.ScriptEditingRawsLocationTextBox, "ScriptEditingRawsLocationTextBox");
			this.ScriptEditingRawsLocationTextBox.Name = "ScriptEditingRawsLocationTextBox";
			this.ScriptEditingRawsLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ScriptEditingRawsLocationTextBox_Validating);
			this.ScriptEditingRawsLocationTextBox.Validated += new System.EventHandler(this.ScriptEditingRawsLocationTextBox_Validated);
			// 
			// ScriptEditingRawsLocationButton
			// 
			resources.ApplyResources(this.ScriptEditingRawsLocationButton, "ScriptEditingRawsLocationButton");
			this.ScriptEditingRawsLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ScriptEditingRawsLocationButton.Name = "ScriptEditingRawsLocationButton";
			this.ScriptEditingRawsLocationButton.UseVisualStyleBackColor = true;
			this.ScriptEditingRawsLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScriptEditingRawsLocationButton_MouseClick);
			// 
			// BeginScriptEditingButton
			// 
			this.BeginScriptEditingButton.BackColor = System.Drawing.SystemColors.Control;
			this.BeginScriptEditingButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			resources.ApplyResources(this.BeginScriptEditingButton, "BeginScriptEditingButton");
			this.BeginScriptEditingButton.Name = "BeginScriptEditingButton";
			this.BeginScriptEditingButton.UseVisualStyleBackColor = false;
			this.BeginScriptEditingButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BeginScriptEditingButton_MouseClick);
			// 
			// ScriptLocationLabel
			// 
			resources.ApplyResources(this.ScriptLocationLabel, "ScriptLocationLabel");
			this.ScriptLocationLabel.Name = "ScriptLocationLabel";
			// 
			// ScriptLocationTextBox
			// 
			resources.ApplyResources(this.ScriptLocationTextBox, "ScriptLocationTextBox");
			this.ScriptLocationTextBox.Name = "ScriptLocationTextBox";
			this.ScriptLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ScriptLocationTextBox_Validating);
			this.ScriptLocationTextBox.Validated += new System.EventHandler(this.ScriptLocationTextBox_Validated);
			// 
			// ScriptLocationButton
			// 
			resources.ApplyResources(this.ScriptLocationButton, "ScriptLocationButton");
			this.ScriptLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ScriptLocationButton.Name = "ScriptLocationButton";
			this.ScriptLocationButton.UseVisualStyleBackColor = true;
			this.ScriptLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScriptLocationButton_MouseClick);
			// 
			// MainFormErrorProvider
			// 
			this.MainFormErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.MainFormErrorProvider.ContainerControl = this;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(this.MainTabControl);
			this.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.MainTabControl.ResumeLayout(false);
			this.ScriptCreatorTabPage.ResumeLayout(false);
			this.ScriptCreatorTabPage.PerformLayout();
			this.OutputToGroupBox.ResumeLayout(false);
			this.OutputToGroupBox.PerformLayout();
			this.ScriptEditorTabPage.ResumeLayout(false);
			this.ScriptEditorTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainFormErrorProvider)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label RawsLocationLabel;
        private System.Windows.Forms.TextBox RawsLocationTextBox;
        private System.Windows.Forms.Button RawsLocationButton;
        private System.Windows.Forms.Label TranslatorNameLabel;
        private System.Windows.Forms.TextBox TranslatorNameTextBox;
		private System.Windows.Forms.Button BeginScriptCreationButton;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ScriptCreatorTabPage;
        private System.Windows.Forms.TabPage ScriptEditorTabPage;
        private System.Windows.Forms.Button BeginScriptEditingButton;
        private System.Windows.Forms.Label ScriptLocationLabel;
        private System.Windows.Forms.TextBox ScriptLocationTextBox;
        private System.Windows.Forms.Button ScriptLocationButton;
		private System.Windows.Forms.Label OutputLocationLabel;
		private System.Windows.Forms.TextBox OutputLocationTextBox;
		private System.Windows.Forms.Button OutputLocationButton;
		private System.Windows.Forms.Label SeriesSelectionLabel;
		private System.Windows.Forms.Label ScriptEditingRawsLocationLabel;
		private System.Windows.Forms.TextBox ScriptEditingRawsLocationTextBox;
		private System.Windows.Forms.Button ScriptEditingRawsLocationButton;
		private System.Windows.Forms.ErrorProvider MainFormErrorProvider;
		private System.Windows.Forms.Label ChapterSelectionLabel;
		private System.Windows.Forms.ComboBox ChapterSelectionComboBox;
		private System.Windows.Forms.ComboBox SeriesSelectionComboBox;
		private System.Windows.Forms.GroupBox OutputToGroupBox;
		private System.Windows.Forms.RadioButton OutputToCustomLocationRadioButton;
		private System.Windows.Forms.RadioButton OutputWithRawsRadioButton;
		private System.Windows.Forms.RadioButton OutputToChapterFolderRadioButton;
	}
}

