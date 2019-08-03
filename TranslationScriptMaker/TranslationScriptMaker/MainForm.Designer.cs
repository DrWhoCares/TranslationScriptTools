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
			this.MainFormErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.OutputToGroupBox = new System.Windows.Forms.GroupBox();
			this.OutputWithRawsRadioButton = new System.Windows.Forms.RadioButton();
			this.OutputToChapterFolderRadioButton = new System.Windows.Forms.RadioButton();
			this.OutputToCustomLocationRadioButton = new System.Windows.Forms.RadioButton();
			this.ChapterSelectionComboBox = new System.Windows.Forms.ComboBox();
			this.SeriesSelectionComboBox = new System.Windows.Forms.ComboBox();
			this.ChapterSelectionLabel = new System.Windows.Forms.Label();
			this.SeriesSelectionLabel = new System.Windows.Forms.Label();
			this.OutputLocationLabel = new System.Windows.Forms.Label();
			this.OutputLocationTextBox = new System.Windows.Forms.TextBox();
			this.RawsLocationTextBox = new System.Windows.Forms.TextBox();
			this.TranslatorNameTextBox = new System.Windows.Forms.TextBox();
			this.OutputLocationButton = new System.Windows.Forms.Button();
			this.BeginScriptCreationButton = new System.Windows.Forms.Button();
			this.RawsLocationLabel = new System.Windows.Forms.Label();
			this.RawsLocationButton = new System.Windows.Forms.Button();
			this.TranslatorNameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.MainFormErrorProvider)).BeginInit();
			this.OutputToGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainFormErrorProvider
			// 
			this.MainFormErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.MainFormErrorProvider.ContainerControl = this;
			// 
			// OutputToGroupBox
			// 
			this.OutputToGroupBox.Controls.Add(this.OutputWithRawsRadioButton);
			this.OutputToGroupBox.Controls.Add(this.OutputToChapterFolderRadioButton);
			this.OutputToGroupBox.Controls.Add(this.OutputToCustomLocationRadioButton);
			this.OutputToGroupBox.ForeColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.OutputToGroupBox, "OutputToGroupBox");
			this.OutputToGroupBox.Name = "OutputToGroupBox";
			this.OutputToGroupBox.TabStop = false;
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
			// OutputToCustomLocationRadioButton
			// 
			resources.ApplyResources(this.OutputToCustomLocationRadioButton, "OutputToCustomLocationRadioButton");
			this.OutputToCustomLocationRadioButton.Name = "OutputToCustomLocationRadioButton";
			this.OutputToCustomLocationRadioButton.TabStop = true;
			this.OutputToCustomLocationRadioButton.UseVisualStyleBackColor = true;
			this.OutputToCustomLocationRadioButton.CheckedChanged += new System.EventHandler(this.OutputToCustomLocationRadioButton_CheckedChanged);
			this.OutputToCustomLocationRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputToCustomLocationRadioButton_MouseClick);
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
			// RawsLocationTextBox
			// 
			resources.ApplyResources(this.RawsLocationTextBox, "RawsLocationTextBox");
			this.RawsLocationTextBox.Name = "RawsLocationTextBox";
			this.RawsLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.RawsLocationTextBox_Validating);
			// 
			// TranslatorNameTextBox
			// 
			resources.ApplyResources(this.TranslatorNameTextBox, "TranslatorNameTextBox");
			this.TranslatorNameTextBox.Name = "TranslatorNameTextBox";
			this.TranslatorNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TranslatorNameTextBox_Validating);
			this.TranslatorNameTextBox.Validated += new System.EventHandler(this.TranslatorNameTextBox_Validated);
			// 
			// OutputLocationButton
			// 
			resources.ApplyResources(this.OutputLocationButton, "OutputLocationButton");
			this.OutputLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.OutputLocationButton.Name = "OutputLocationButton";
			this.OutputLocationButton.UseVisualStyleBackColor = true;
			this.OutputLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputLocationButton_MouseClick);
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
			// RawsLocationLabel
			// 
			resources.ApplyResources(this.RawsLocationLabel, "RawsLocationLabel");
			this.RawsLocationLabel.Name = "RawsLocationLabel";
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
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(this.ChapterSelectionComboBox);
			this.Controls.Add(this.RawsLocationTextBox);
			this.Controls.Add(this.SeriesSelectionComboBox);
			this.Controls.Add(this.TranslatorNameLabel);
			this.Controls.Add(this.ChapterSelectionLabel);
			this.Controls.Add(this.RawsLocationButton);
			this.Controls.Add(this.SeriesSelectionLabel);
			this.Controls.Add(this.RawsLocationLabel);
			this.Controls.Add(this.BeginScriptCreationButton);
			this.Controls.Add(this.OutputLocationTextBox);
			this.Controls.Add(this.OutputLocationButton);
			this.Controls.Add(this.TranslatorNameTextBox);
			this.Controls.Add(this.OutputToGroupBox);
			this.Controls.Add(this.OutputLocationLabel);
			this.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.MainFormErrorProvider)).EndInit();
			this.OutputToGroupBox.ResumeLayout(false);
			this.OutputToGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
		private System.Windows.Forms.ErrorProvider MainFormErrorProvider;
		private System.Windows.Forms.GroupBox OutputToGroupBox;
		private System.Windows.Forms.ComboBox ChapterSelectionComboBox;
		private System.Windows.Forms.ComboBox SeriesSelectionComboBox;
		private System.Windows.Forms.Label ChapterSelectionLabel;
		private System.Windows.Forms.Label SeriesSelectionLabel;
		private System.Windows.Forms.Label OutputLocationLabel;
		private System.Windows.Forms.TextBox OutputLocationTextBox;
		private System.Windows.Forms.TextBox RawsLocationTextBox;
		private System.Windows.Forms.TextBox TranslatorNameTextBox;
		private System.Windows.Forms.Button OutputLocationButton;
		private System.Windows.Forms.Button BeginScriptCreationButton;
		private System.Windows.Forms.Label RawsLocationLabel;
		private System.Windows.Forms.Button RawsLocationButton;
		private System.Windows.Forms.Label TranslatorNameLabel;
		private System.Windows.Forms.RadioButton OutputToChapterFolderRadioButton;
		private System.Windows.Forms.RadioButton OutputWithRawsRadioButton;
		private System.Windows.Forms.RadioButton OutputToCustomLocationRadioButton;
	}
}

