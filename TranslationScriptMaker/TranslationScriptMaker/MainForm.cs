using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace TranslationScriptMaker
{
	public partial class MainForm : Form
	{
		private string RawsLocationFullPath { get; set; }
        private string ScriptLocationFullPath { get; set; }
		private string SelectedChapterNumber { get; set; }
		private IEnumerable<FileInfo> RawsFiles { get; set; }
		private string TranslatorsName { get; set; }

		public MainForm()
		{
			InitializeComponent();
		}

        private void ShowError(Label errorLabel, string errorMessage)
        {
            errorLabel.Text = errorMessage;
            errorLabel.Visible = true;
        }

        #region ScriptCreation
        private void RawsLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			CommonOpenFileDialog rawsLocationDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};

			if ( rawsLocationDialog.ShowDialog() == CommonFileDialogResult.Ok )
			{
				RawsLocationTextBox.Text = rawsLocationDialog.FileName;
			}
		}

		private bool VerifyRawsLocation()
		{
			// Must be in a folder marked "Chapter X" (where X is any numerical value) in a subfolder called "Raws" and must contain at least one image in style of XXX.{ext}
			string pathToCheck = RawsLocationTextBox.Text;
			int index = pathToCheck.LastIndexOf('\\');

			if ( pathToCheck.Substring(index + 1) != "Raws" )
			{
				ShowError(ScriptCreationErrorLabel, "Final folder must be named 'Raws'.");
				return false;
			}

			string chapterFolderPath = pathToCheck.Substring(0, index);
			string chapterFolderName = chapterFolderPath.Substring(chapterFolderPath.LastIndexOf('\\') + 1);

			Match result = Regex.Match(chapterFolderName, @"Chapter [0-9]{1,4}([.,][0-9]{1,2})?$", RegexOptions.IgnoreCase);

			if ( !result.Success )
			{
				ShowError(ScriptCreationErrorLabel, "Folder containing Raws must be named 'Chapter X'\nwhere X is any number/decimal to 2 digits.");
				return false;
			}

			if ( !Directory.Exists(RawsLocationTextBox.Text) )
			{
				ShowError(ScriptCreationErrorLabel, "The selected directory does not exist.");
				return false;
			}

			DirectoryInfo rawsDirectory = new DirectoryInfo(RawsLocationTextBox.Text);

			var files = rawsDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
				.Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
					|| s.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase));

			if ( files.Count() == 0 )
			{
				ShowError(ScriptCreationErrorLabel, "The Raws folder does not contain any supported image types.\nSupported types: .png, .jpg, .jpeg");
				return false;
			}

			RawsLocationFullPath = RawsLocationTextBox.Text;
			SelectedChapterNumber = chapterFolderName.Substring(chapterFolderName.LastIndexOf(' ') + 1);
			RawsFiles = files;

			ScriptCreationErrorLabel.Visible = false;

			return true;
		}

		private bool VerifyTranslatorName()
		{
			// Name cannot be empty and these characters cannot be present: / \ | : * ? < >
			if ( string.IsNullOrWhiteSpace(TranslatorNameTextBox.Text) )
			{
				ShowError(ScriptCreationErrorLabel, "Translator name cannot be empty or just spaces,\nand must contain at least one letter.");
				return false;
			}

			if ( TranslatorNameTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1 )
			{
				ShowError(ScriptCreationErrorLabel, "Translator name cannot contain any of the following\ncharacters: / \\ | : * ? < >");
				return false;
			}

			TranslatorsName = TranslatorNameTextBox.Text;

			return true;
		}

		private bool AreScriptCreationInputsValid()
		{
			if ( !VerifyRawsLocation() )
			{
				return false;
			}

			if ( !VerifyTranslatorName() )
			{
				return false;
			}

			return true;
		}

		private void RawsLocationTextBox_TextChanged(object sender, System.EventArgs e)
		{
			BeginScriptCreationButton.Enabled = AreScriptCreationInputsValid();
		}

		private void TranslatorNameTextBox_TextChanged(object sender, System.EventArgs e)
		{
			BeginScriptCreationButton.Enabled = AreScriptCreationInputsValid();
		}

		private void BeginScriptCreationButton_MouseClick(object sender, MouseEventArgs e)
		{
			BeginScriptCreation();
		}

		private void TranslatorNameTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				if ( AreScriptCreationInputsValid() )
				{
					BeginScriptCreation();
				}
			}
		}

		private void BeginScriptCreation()
		{
			RawsViewerForm rawsViewerForm = new RawsViewerForm(RawsLocationFullPath, ScriptLocationFullPath, SelectedChapterNumber, RawsFiles, TranslatorsName, true);
			this.Hide();
			rawsViewerForm.ShowDialog();
			this.Show();
		}
        #endregion

        #region ScriptEditing
        private void ScriptLocationButton_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog scriptLocationDialog = new OpenFileDialog();

            if (scriptLocationDialog.ShowDialog() == DialogResult.OK)
            {
                if ( !scriptLocationDialog.CheckPathExists )
                {
                    ShowError(ScriptEditingErrorLabel, "The selected path does not exist.");
                }

                if ( !scriptLocationDialog.CheckFileExists )
                {
                    ShowError(ScriptEditingErrorLabel, "The selected file does not exist.");
                }

                ScriptLocationTextBox.Text = scriptLocationDialog.FileName;
            }
        }

        private void ScriptLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            BeginScriptEditingButton.Enabled = AreScriptEditingInputsValid();
        }

        private bool AreScriptEditingInputsValid()
        {
            return VerifyScriptLocation();
        }

        private bool VerifyScriptLocation()
        {
            // Must be in a folder marked "Chapter X" (where X is any numerical value) in a subfolder called "Raws" and must contain at least one image in style of XXX.{ext}
            string pathToCheck = ScriptLocationTextBox.Text;
            int index = pathToCheck.LastIndexOf('\\');

            pathToCheck = pathToCheck.Substring(0, index + 1) + "Raws";

            string chapterFolderPath = pathToCheck.Substring(0, index);
            string chapterFolderName = chapterFolderPath.Substring(chapterFolderPath.LastIndexOf('\\') + 1);

            Match result = Regex.Match(chapterFolderName, @"Chapter [0-9]{1,4}([.,][0-9]{1,2})?$", RegexOptions.IgnoreCase);

            if (!result.Success)
            {
                ShowError(ScriptEditingErrorLabel, "Folder containing Script must be named 'Chapter X'\nwhere X is any number/decimal to 2 digits.");
                return false;
            }

            if (!Directory.Exists(pathToCheck))
            {
                ShowError(ScriptEditingErrorLabel, "The directory containing the script does not have a folder called 'Raws' in it.");
                return false;
            }

            DirectoryInfo rawsDirectory = new DirectoryInfo(pathToCheck);

            var files = rawsDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase));

            if (files.Count() == 0)
            {
                ShowError(ScriptEditingErrorLabel, "The Raws folder does not contain any images.");
                return false;
            }

            RawsLocationFullPath = pathToCheck;
            ScriptLocationFullPath = ScriptLocationTextBox.Text;
            SelectedChapterNumber = chapterFolderName.Substring(chapterFolderName.LastIndexOf(' ') + 1);
            RawsFiles = files;

            ScriptEditingErrorLabel.Visible = false;

            return true;
        }

        private void BeginScriptEditingButton_MouseClick(object sender, MouseEventArgs e) => BeginScriptEditing();

        private void BeginScriptEditing()
        {
            RawsViewerForm rawsViewerForm = new RawsViewerForm(RawsLocationFullPath, ScriptLocationFullPath, SelectedChapterNumber, RawsFiles, TranslatorsName, false);
            this.Hide();
            rawsViewerForm.ShowDialog();
            this.Show();
        }
        #endregion
    }
}
