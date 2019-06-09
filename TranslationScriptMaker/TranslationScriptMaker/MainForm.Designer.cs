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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.RawsLocationLabel = new System.Windows.Forms.Label();
			this.RawsLocationTextBox = new System.Windows.Forms.TextBox();
			this.RawsLocationButton = new System.Windows.Forms.Button();
			this.TranslatorNameLabel = new System.Windows.Forms.Label();
			this.TranslatorNameTextBox = new System.Windows.Forms.TextBox();
			this.ScriptCreationErrorLabel = new System.Windows.Forms.Label();
			this.BeginScriptCreationButton = new System.Windows.Forms.Button();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.ScriptCreatorTabPage = new System.Windows.Forms.TabPage();
			this.ChapterNumberTextBox = new System.Windows.Forms.TextBox();
			this.ChapterNumberLabel = new System.Windows.Forms.Label();
			this.OutputLocationLabel = new System.Windows.Forms.Label();
			this.OutputLocationTextBox = new System.Windows.Forms.TextBox();
			this.OutputLocationButton = new System.Windows.Forms.Button();
			this.ScriptEditorTabPage = new System.Windows.Forms.TabPage();
			this.BeginScriptEditingButton = new System.Windows.Forms.Button();
			this.ScriptLocationLabel = new System.Windows.Forms.Label();
			this.ScriptEditingErrorLabel = new System.Windows.Forms.Label();
			this.ScriptLocationTextBox = new System.Windows.Forms.TextBox();
			this.ScriptLocationButton = new System.Windows.Forms.Button();
			this.ScriptEditingRawsLocationButton = new System.Windows.Forms.Button();
			this.ScriptEditingRawsLocationTextBox = new System.Windows.Forms.TextBox();
			this.ScriptEditingRawsLocationLabel = new System.Windows.Forms.Label();
			this.MainTabControl.SuspendLayout();
			this.ScriptCreatorTabPage.SuspendLayout();
			this.ScriptEditorTabPage.SuspendLayout();
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
			this.RawsLocationTextBox.TextChanged += new System.EventHandler(this.RawsLocationTextBox_TextChanged);
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
			this.TranslatorNameTextBox.TextChanged += new System.EventHandler(this.TranslatorNameTextBox_TextChanged);
			this.TranslatorNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TranslatorNameTextBox_KeyDown);
			// 
			// ScriptCreationErrorLabel
			// 
			resources.ApplyResources(this.ScriptCreationErrorLabel, "ScriptCreationErrorLabel");
			this.ScriptCreationErrorLabel.Name = "ScriptCreationErrorLabel";
			// 
			// BeginScriptCreationButton
			// 
			this.BeginScriptCreationButton.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.BeginScriptCreationButton, "BeginScriptCreationButton");
			this.BeginScriptCreationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
			this.ScriptCreatorTabPage.Controls.Add(this.ChapterNumberTextBox);
			this.ScriptCreatorTabPage.Controls.Add(this.ChapterNumberLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.OutputLocationLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.OutputLocationTextBox);
			this.ScriptCreatorTabPage.Controls.Add(this.OutputLocationButton);
			this.ScriptCreatorTabPage.Controls.Add(this.BeginScriptCreationButton);
			this.ScriptCreatorTabPage.Controls.Add(this.RawsLocationLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.ScriptCreationErrorLabel);
			this.ScriptCreatorTabPage.Controls.Add(this.RawsLocationTextBox);
			this.ScriptCreatorTabPage.Controls.Add(this.TranslatorNameTextBox);
			this.ScriptCreatorTabPage.Controls.Add(this.RawsLocationButton);
			this.ScriptCreatorTabPage.Controls.Add(this.TranslatorNameLabel);
			this.ScriptCreatorTabPage.ForeColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.ScriptCreatorTabPage, "ScriptCreatorTabPage");
			this.ScriptCreatorTabPage.Name = "ScriptCreatorTabPage";
			// 
			// ChapterNumberTextBox
			// 
			resources.ApplyResources(this.ChapterNumberTextBox, "ChapterNumberTextBox");
			this.ChapterNumberTextBox.Name = "ChapterNumberTextBox";
			this.ChapterNumberTextBox.TextChanged += new System.EventHandler(this.ChapterNumberTextBox_TextChanged);
			this.ChapterNumberTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChapterNumberTextBox_KeyDown);
			// 
			// ChapterNumberLabel
			// 
			resources.ApplyResources(this.ChapterNumberLabel, "ChapterNumberLabel");
			this.ChapterNumberLabel.Name = "ChapterNumberLabel";
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
			this.OutputLocationTextBox.TextChanged += new System.EventHandler(this.OutputLocationTextBox_TextChanged);
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
			this.ScriptEditorTabPage.Controls.Add(this.ScriptEditingErrorLabel);
			this.ScriptEditorTabPage.Controls.Add(this.ScriptLocationTextBox);
			this.ScriptEditorTabPage.Controls.Add(this.ScriptLocationButton);
			this.ScriptEditorTabPage.ForeColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.ScriptEditorTabPage, "ScriptEditorTabPage");
			this.ScriptEditorTabPage.Name = "ScriptEditorTabPage";
			// 
			// BeginScriptEditingButton
			// 
			this.BeginScriptEditingButton.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.BeginScriptEditingButton, "BeginScriptEditingButton");
			this.BeginScriptEditingButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.BeginScriptEditingButton.Name = "BeginScriptEditingButton";
			this.BeginScriptEditingButton.UseVisualStyleBackColor = false;
			this.BeginScriptEditingButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BeginScriptEditingButton_MouseClick);
			// 
			// ScriptLocationLabel
			// 
			resources.ApplyResources(this.ScriptLocationLabel, "ScriptLocationLabel");
			this.ScriptLocationLabel.Name = "ScriptLocationLabel";
			// 
			// ScriptEditingErrorLabel
			// 
			resources.ApplyResources(this.ScriptEditingErrorLabel, "ScriptEditingErrorLabel");
			this.ScriptEditingErrorLabel.Name = "ScriptEditingErrorLabel";
			// 
			// ScriptLocationTextBox
			// 
			resources.ApplyResources(this.ScriptLocationTextBox, "ScriptLocationTextBox");
			this.ScriptLocationTextBox.Name = "ScriptLocationTextBox";
			this.ScriptLocationTextBox.TextChanged += new System.EventHandler(this.ScriptLocationTextBox_TextChanged);
			// 
			// ScriptLocationButton
			// 
			resources.ApplyResources(this.ScriptLocationButton, "ScriptLocationButton");
			this.ScriptLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ScriptLocationButton.Name = "ScriptLocationButton";
			this.ScriptLocationButton.UseVisualStyleBackColor = true;
			this.ScriptLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScriptLocationButton_MouseClick);
			// 
			// ScriptEditingRawsLocationButton
			// 
			resources.ApplyResources(this.ScriptEditingRawsLocationButton, "ScriptEditingRawsLocationButton");
			this.ScriptEditingRawsLocationButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ScriptEditingRawsLocationButton.Name = "ScriptEditingRawsLocationButton";
			this.ScriptEditingRawsLocationButton.UseVisualStyleBackColor = true;
			this.ScriptEditingRawsLocationButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScriptEditingRawsLocationButton_MouseClick);
			// 
			// ScriptEditingRawsLocationTextBox
			// 
			resources.ApplyResources(this.ScriptEditingRawsLocationTextBox, "ScriptEditingRawsLocationTextBox");
			this.ScriptEditingRawsLocationTextBox.Name = "ScriptEditingRawsLocationTextBox";
			this.ScriptEditingRawsLocationTextBox.TextChanged += new System.EventHandler(this.ScriptEditingRawsLocationTextBox_TextChanged);
			// 
			// ScriptEditingRawsLocationLabel
			// 
			resources.ApplyResources(this.ScriptEditingRawsLocationLabel, "ScriptEditingRawsLocationLabel");
			this.ScriptEditingRawsLocationLabel.Name = "ScriptEditingRawsLocationLabel";
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
			this.ScriptEditorTabPage.ResumeLayout(false);
			this.ScriptEditorTabPage.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label RawsLocationLabel;
        private System.Windows.Forms.TextBox RawsLocationTextBox;
        private System.Windows.Forms.Button RawsLocationButton;
        private System.Windows.Forms.Label TranslatorNameLabel;
        private System.Windows.Forms.TextBox TranslatorNameTextBox;
		private System.Windows.Forms.Label ScriptCreationErrorLabel;
		private System.Windows.Forms.Button BeginScriptCreationButton;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ScriptCreatorTabPage;
        private System.Windows.Forms.TabPage ScriptEditorTabPage;
        private System.Windows.Forms.Button BeginScriptEditingButton;
        private System.Windows.Forms.Label ScriptLocationLabel;
        private System.Windows.Forms.Label ScriptEditingErrorLabel;
        private System.Windows.Forms.TextBox ScriptLocationTextBox;
        private System.Windows.Forms.Button ScriptLocationButton;
		private System.Windows.Forms.Label OutputLocationLabel;
		private System.Windows.Forms.TextBox OutputLocationTextBox;
		private System.Windows.Forms.Button OutputLocationButton;
		private System.Windows.Forms.TextBox ChapterNumberTextBox;
		private System.Windows.Forms.Label ChapterNumberLabel;
		private System.Windows.Forms.Label ScriptEditingRawsLocationLabel;
		private System.Windows.Forms.TextBox ScriptEditingRawsLocationTextBox;
		private System.Windows.Forms.Button ScriptEditingRawsLocationButton;
	}
}

