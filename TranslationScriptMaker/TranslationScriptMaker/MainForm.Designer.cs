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
			this.OutputAsTypesettererCheckBox = new System.Windows.Forms.CheckBox();
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
			this.OutputToGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.OutputToGroupBox.Location = new System.Drawing.Point(438, 103);
			this.OutputToGroupBox.Name = "OutputToGroupBox";
			this.OutputToGroupBox.Size = new System.Drawing.Size(139, 75);
			this.OutputToGroupBox.TabIndex = 18;
			this.OutputToGroupBox.TabStop = false;
			this.OutputToGroupBox.Text = "OutputTo";
			// 
			// OutputWithRawsRadioButton
			// 
			this.OutputWithRawsRadioButton.AutoSize = true;
			this.OutputWithRawsRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.OutputWithRawsRadioButton.Location = new System.Drawing.Point(6, 34);
			this.OutputWithRawsRadioButton.Name = "OutputWithRawsRadioButton";
			this.OutputWithRawsRadioButton.Size = new System.Drawing.Size(77, 17);
			this.OutputWithRawsRadioButton.TabIndex = 1;
			this.OutputWithRawsRadioButton.TabStop = true;
			this.OutputWithRawsRadioButton.Text = "With Raws";
			this.OutputWithRawsRadioButton.UseVisualStyleBackColor = true;
			this.OutputWithRawsRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputWithRawsRadioButton_MouseClick);
			// 
			// OutputToChapterFolderRadioButton
			// 
			this.OutputToChapterFolderRadioButton.AutoSize = true;
			this.OutputToChapterFolderRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.OutputToChapterFolderRadioButton.Location = new System.Drawing.Point(6, 16);
			this.OutputToChapterFolderRadioButton.Name = "OutputToChapterFolderRadioButton";
			this.OutputToChapterFolderRadioButton.Size = new System.Drawing.Size(94, 17);
			this.OutputToChapterFolderRadioButton.TabIndex = 0;
			this.OutputToChapterFolderRadioButton.TabStop = true;
			this.OutputToChapterFolderRadioButton.Text = "Chapter Folder";
			this.OutputToChapterFolderRadioButton.UseVisualStyleBackColor = true;
			this.OutputToChapterFolderRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputToChapterFolderRadioButton_MouseClick);
			// 
			// OutputToCustomLocationRadioButton
			// 
			this.OutputToCustomLocationRadioButton.AutoSize = true;
			this.OutputToCustomLocationRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.OutputToCustomLocationRadioButton.Location = new System.Drawing.Point(6, 52);
			this.OutputToCustomLocationRadioButton.Name = "OutputToCustomLocationRadioButton";
			this.OutputToCustomLocationRadioButton.Size = new System.Drawing.Size(104, 17);
			this.OutputToCustomLocationRadioButton.TabIndex = 2;
			this.OutputToCustomLocationRadioButton.TabStop = true;
			this.OutputToCustomLocationRadioButton.Text = "Custom Location";
			this.OutputToCustomLocationRadioButton.UseVisualStyleBackColor = true;
			this.OutputToCustomLocationRadioButton.CheckedChanged += new System.EventHandler(this.OutputToCustomLocationRadioButton_CheckedChanged);
			this.OutputToCustomLocationRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputToCustomLocationRadioButton_MouseClick);
			// 
			// ChapterSelectionComboBox
			// 
			this.ChapterSelectionComboBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ChapterSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ChapterSelectionComboBox.FormattingEnabled = true;
			this.ChapterSelectionComboBox.Location = new System.Drawing.Point(92, 157);
			this.ChapterSelectionComboBox.Name = "ChapterSelectionComboBox";
			this.ChapterSelectionComboBox.Size = new System.Drawing.Size(340, 21);
			this.ChapterSelectionComboBox.TabIndex = 14;
			this.ChapterSelectionComboBox.SelectionChangeCommitted += new System.EventHandler(this.ChapterSelectionComboBox_SelectionChangeCommitted);
			// 
			// SeriesSelectionComboBox
			// 
			this.SeriesSelectionComboBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.SeriesSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SeriesSelectionComboBox.FormattingEnabled = true;
			this.SeriesSelectionComboBox.Location = new System.Drawing.Point(79, 131);
			this.SeriesSelectionComboBox.Name = "SeriesSelectionComboBox";
			this.SeriesSelectionComboBox.Size = new System.Drawing.Size(353, 21);
			this.SeriesSelectionComboBox.TabIndex = 13;
			this.SeriesSelectionComboBox.SelectionChangeCommitted += new System.EventHandler(this.SeriesSelectionComboBox_SelectionChangeCommitted);
			// 
			// ChapterSelectionLabel
			// 
			this.ChapterSelectionLabel.AutoSize = true;
			this.ChapterSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.ChapterSelectionLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ChapterSelectionLabel.Location = new System.Drawing.Point(8, 158);
			this.ChapterSelectionLabel.Name = "ChapterSelectionLabel";
			this.ChapterSelectionLabel.Size = new System.Drawing.Size(78, 20);
			this.ChapterSelectionLabel.TabIndex = 12;
			this.ChapterSelectionLabel.Text = "Chapter:";
			// 
			// SeriesSelectionLabel
			// 
			this.SeriesSelectionLabel.AutoSize = true;
			this.SeriesSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.SeriesSelectionLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.SeriesSelectionLabel.Location = new System.Drawing.Point(8, 131);
			this.SeriesSelectionLabel.Name = "SeriesSelectionLabel";
			this.SeriesSelectionLabel.Size = new System.Drawing.Size(65, 20);
			this.SeriesSelectionLabel.TabIndex = 11;
			this.SeriesSelectionLabel.Text = "Series:";
			// 
			// OutputLocationLabel
			// 
			this.OutputLocationLabel.AutoSize = true;
			this.OutputLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.OutputLocationLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.OutputLocationLabel.Location = new System.Drawing.Point(8, 53);
			this.OutputLocationLabel.Name = "OutputLocationLabel";
			this.OutputLocationLabel.Size = new System.Drawing.Size(143, 20);
			this.OutputLocationLabel.TabIndex = 9;
			this.OutputLocationLabel.Text = "Output Location:";
			// 
			// OutputLocationTextBox
			// 
			this.OutputLocationTextBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.OutputLocationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OutputLocationTextBox.Location = new System.Drawing.Point(51, 77);
			this.OutputLocationTextBox.Name = "OutputLocationTextBox";
			this.OutputLocationTextBox.Size = new System.Drawing.Size(526, 20);
			this.OutputLocationTextBox.TabIndex = 8;
			this.OutputLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OutputLocationTextBox_Validating);
			// 
			// RawsLocationTextBox
			// 
			this.RawsLocationTextBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.RawsLocationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RawsLocationTextBox.Location = new System.Drawing.Point(51, 28);
			this.RawsLocationTextBox.Name = "RawsLocationTextBox";
			this.RawsLocationTextBox.Size = new System.Drawing.Size(526, 20);
			this.RawsLocationTextBox.TabIndex = 1;
			this.RawsLocationTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.RawsLocationTextBox_Validating);
			// 
			// TranslatorNameTextBox
			// 
			this.TranslatorNameTextBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.TranslatorNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TranslatorNameTextBox.Location = new System.Drawing.Point(160, 104);
			this.TranslatorNameTextBox.Name = "TranslatorNameTextBox";
			this.TranslatorNameTextBox.Size = new System.Drawing.Size(272, 20);
			this.TranslatorNameTextBox.TabIndex = 2;
			this.TranslatorNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TranslatorNameTextBox_Validating);
			this.TranslatorNameTextBox.Validated += new System.EventHandler(this.TranslatorNameTextBox_Validated);
			// 
			// OutputLocationButton
			// 
			this.OutputLocationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.OutputLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.OutputLocationButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.OutputLocationButton.Location = new System.Drawing.Point(12, 75);
			this.OutputLocationButton.Name = "OutputLocationButton";
			this.OutputLocationButton.Size = new System.Drawing.Size(33, 23);
			this.OutputLocationButton.TabIndex = 7;
			this.OutputLocationButton.Text = "...";
			this.OutputLocationButton.UseVisualStyleBackColor = true;
			this.OutputLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputLocationButton_MouseClick);
			// 
			// BeginScriptCreationButton
			// 
			this.BeginScriptCreationButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BeginScriptCreationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.BeginScriptCreationButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.BeginScriptCreationButton.Location = new System.Drawing.Point(12, 185);
			this.BeginScriptCreationButton.Name = "BeginScriptCreationButton";
			this.BeginScriptCreationButton.Size = new System.Drawing.Size(75, 23);
			this.BeginScriptCreationButton.TabIndex = 6;
			this.BeginScriptCreationButton.Text = "Continue";
			this.BeginScriptCreationButton.UseVisualStyleBackColor = false;
			this.BeginScriptCreationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BeginScriptCreationButton_MouseClick);
			// 
			// RawsLocationLabel
			// 
			this.RawsLocationLabel.AutoSize = true;
			this.RawsLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.RawsLocationLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.RawsLocationLabel.Location = new System.Drawing.Point(8, 4);
			this.RawsLocationLabel.Name = "RawsLocationLabel";
			this.RawsLocationLabel.Size = new System.Drawing.Size(132, 20);
			this.RawsLocationLabel.TabIndex = 0;
			this.RawsLocationLabel.Text = "Raws Location:";
			// 
			// RawsLocationButton
			// 
			this.RawsLocationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.RawsLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.RawsLocationButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.RawsLocationButton.Location = new System.Drawing.Point(12, 26);
			this.RawsLocationButton.Name = "RawsLocationButton";
			this.RawsLocationButton.Size = new System.Drawing.Size(33, 23);
			this.RawsLocationButton.TabIndex = 0;
			this.RawsLocationButton.Text = "...";
			this.RawsLocationButton.UseVisualStyleBackColor = true;
			this.RawsLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RawsLocationButton_MouseClick);
			// 
			// TranslatorNameLabel
			// 
			this.TranslatorNameLabel.AutoSize = true;
			this.TranslatorNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.TranslatorNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.TranslatorNameLabel.Location = new System.Drawing.Point(8, 104);
			this.TranslatorNameLabel.Name = "TranslatorNameLabel";
			this.TranslatorNameLabel.Size = new System.Drawing.Size(146, 20);
			this.TranslatorNameLabel.TabIndex = 3;
			this.TranslatorNameLabel.Text = "Translator Name:";
			// 
			// OutputAsTypesettererCheckBox
			// 
			this.OutputAsTypesettererCheckBox.AutoSize = true;
			this.OutputAsTypesettererCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.OutputAsTypesettererCheckBox.Location = new System.Drawing.Point(93, 189);
			this.OutputAsTypesettererCheckBox.Name = "OutputAsTypesettererCheckBox";
			this.OutputAsTypesettererCheckBox.Size = new System.Drawing.Size(183, 17);
			this.OutputAsTypesettererCheckBox.TabIndex = 19;
			this.OutputAsTypesettererCheckBox.Text = "Output as Typesetterer Compliant";
			this.OutputAsTypesettererCheckBox.UseVisualStyleBackColor = true;
			this.OutputAsTypesettererCheckBox.CheckedChanged += new System.EventHandler(this.OutputAsTypesettererCheckBox_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.ClientSize = new System.Drawing.Size(601, 219);
			this.Controls.Add(this.OutputAsTypesettererCheckBox);
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
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Translation Script Maker";
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
		private System.Windows.Forms.CheckBox OutputAsTypesettererCheckBox;
	}
}

