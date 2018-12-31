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
			this.ErrorLabel = new System.Windows.Forms.Label();
			this.BeginScriptCreationButton = new System.Windows.Forms.Button();
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
			// ErrorLabel
			// 
			resources.ApplyResources(this.ErrorLabel, "ErrorLabel");
			this.ErrorLabel.Name = "ErrorLabel";
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
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(this.BeginScriptCreationButton);
			this.Controls.Add(this.ErrorLabel);
			this.Controls.Add(this.TranslatorNameTextBox);
			this.Controls.Add(this.TranslatorNameLabel);
			this.Controls.Add(this.RawsLocationButton);
			this.Controls.Add(this.RawsLocationTextBox);
			this.Controls.Add(this.RawsLocationLabel);
			this.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RawsLocationLabel;
        private System.Windows.Forms.TextBox RawsLocationTextBox;
        private System.Windows.Forms.Button RawsLocationButton;
        private System.Windows.Forms.Label TranslatorNameLabel;
        private System.Windows.Forms.TextBox TranslatorNameTextBox;
		private System.Windows.Forms.Label ErrorLabel;
		private System.Windows.Forms.Button BeginScriptCreationButton;
	}
}

