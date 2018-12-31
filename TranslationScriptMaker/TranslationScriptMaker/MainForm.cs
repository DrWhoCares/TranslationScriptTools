using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace TranslationScriptMaker
{
	public partial class MainForm : Form
	{
		private string RawsLocationFullPath { get; set; }
		private string SelectedChapterNumber { get; set; }
		private IEnumerable<FileInfo> RawsFiles { get; set; }
		private string TranslatorsName { get; set; }

		public MainForm()
		{
			InitializeComponent();
		}

		private void RawsLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			FolderBrowserDialog rawsLocationDialog = new FolderBrowserDialog();

			if ( rawsLocationDialog.ShowDialog() == DialogResult.OK )
			{
				RawsLocationTextBox.Text = rawsLocationDialog.SelectedPath;
			}
		}

		private void ShowError(string errorMessage)
		{
			ErrorLabel.Text = errorMessage;
			ErrorLabel.Visible = true;
		}

		private bool VerifyRawsLocation()
		{
			// Must be in a folder marked "Chapter X" (where X is any numerical value) in a subfolder called "Raws" and must contain at least one image in style of XXX.{ext}
			string pathToCheck = RawsLocationTextBox.Text;
			int index = pathToCheck.LastIndexOf('\\');

			if ( pathToCheck.Substring(index + 1) != "Raws" )
			{
				ShowError("Final folder must be named 'Raws'.");
				return false;
			}

			string chapterFolderPath = pathToCheck.Substring(0, index);
			string chapterFolderName = chapterFolderPath.Substring(chapterFolderPath.LastIndexOf('\\') + 1);

			Match result = Regex.Match(chapterFolderName, @"Chapter [0-9]{1,4}([.,][0-9]{1,2})?$", RegexOptions.IgnoreCase);

			if ( !result.Success )
			{
				ShowError("Folder containing Raws must be named 'Chapter X'\nwhere X is any number/decimal to 2 digits.");
				return false;
			}

			if ( !Directory.Exists(RawsLocationTextBox.Text) )
			{
				ShowError("The selected directory does not exist.");
				return false;
			}

			DirectoryInfo rawsDirectory = new DirectoryInfo(RawsLocationTextBox.Text);

			var files = rawsDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
				.Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase));

			if ( files.Count() == 0 )
			{
				ShowError("The Raws folder does not contain any images.");
				return false;
			}

			RawsLocationFullPath = RawsLocationTextBox.Text;
			SelectedChapterNumber = chapterFolderName.Substring(chapterFolderName.LastIndexOf(' ') + 1);
			RawsFiles = files;

			ErrorLabel.Visible = false;

			return true;
		}

		private bool VerifyTranslatorName()
		{
			// Name cannot be empty and these characters cannot be present: / \ | : * ? < >
			if ( string.IsNullOrWhiteSpace(TranslatorNameTextBox.Text) )
			{
				ShowError("Translator name cannot be empty or just spaces,\nand must contain at least one letter.");
				return false;
			}

			if ( TranslatorNameTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1 )
			{
				ShowError("Translator name cannot contain any of the following\ncharacters: / \\ | : * ? < >");
				return false;
			}

			TranslatorsName = TranslatorNameTextBox.Text;

			return true;
		}

		private bool AreInputsValid()
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
			BeginScriptCreationButton.Enabled = AreInputsValid();
		}

		private void TranslatorNameTextBox_TextChanged(object sender, System.EventArgs e)
		{
			BeginScriptCreationButton.Enabled = AreInputsValid();
		}

		private void BeginScriptCreationButton_MouseClick(object sender, MouseEventArgs e)
		{
			BeginScriptCreation();
		}

		private void TranslatorNameTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				if ( AreInputsValid() )
				{
					BeginScriptCreation();
				}
			}
		}

		private void BeginScriptCreation()
		{
			RawsViewerForm rawsViewerForm = new RawsViewerForm(RawsLocationFullPath, SelectedChapterNumber, RawsFiles, TranslatorsName);
			this.Hide();
			rawsViewerForm.ShowDialog();
			this.Show();
		}
	}
}
