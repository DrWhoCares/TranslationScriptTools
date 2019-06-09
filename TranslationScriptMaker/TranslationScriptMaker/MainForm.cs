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
		private const string CONFIG_FILENAME = "TSMConfig.txt";
		private const char CFG_DELIMITER = '=';
		private const string CFG_TRANSLATOR_NAME = "Translator=";
		private string OutputLocationFullPath { get; set; }
		private string TranslatorName { get; set; }
		private string ScriptLocationFullPath { get; set; }
		private string SelectedChapterNumber { get; set; }
		private IEnumerable<FileInfo> RawsFiles { get; set; }
		
		
		public MainForm()
		{
			InitializeComponent();
			ReadTSMConfigFile();

			ScriptCreationErrorLabel.Visible = false;
		}

		private static void ShowError(Label errorLabel, string errorMessage)
		{
			errorLabel.Text = errorMessage;
			errorLabel.Visible = true;
		}

		#region ConfigReading
		private void ReadTSMConfigFile()
		{
			if ( !File.Exists(CONFIG_FILENAME) )
			{
				StreamWriter configFile = File.CreateText(CONFIG_FILENAME);
				configFile.WriteLine(CFG_TRANSLATOR_NAME + Environment.UserName);
				configFile.Close();
				TranslatorNameTextBox.Text = Environment.UserName;
				TranslatorName = Environment.UserName;
				return;
			}

			string[] configEntries = File.ReadAllLines(CONFIG_FILENAME);

			foreach ( string configEntry in configEntries )
			{
				if ( configEntry.Contains(CFG_TRANSLATOR_NAME) )
				{
					string translatorName = GetConfigEntryValue(configEntry);
					TranslatorNameTextBox.Text = translatorName;
					TranslatorName = translatorName;
				}
			}

			if ( string.IsNullOrWhiteSpace(TranslatorName) )
			{
				StreamWriter configFile = File.AppendText(CONFIG_FILENAME);
				configFile.WriteLine(CFG_TRANSLATOR_NAME + Environment.UserName);
				configFile.Close();
				TranslatorNameTextBox.Text = Environment.UserName;
				TranslatorName = Environment.UserName;
			}
		}

		private static string GetConfigEntryValue(string configEntry)
		{
			return configEntry.Substring(configEntry.IndexOf(CFG_DELIMITER) + 1); // Offset by one to remove the delimiter
		}

		private void UpdateConfigEntryInFile(string configKeyString, string newValue)
		{
			string[] configEntries = File.ReadAllLines(CONFIG_FILENAME);

			for ( int configIndex = 0; configIndex < configEntries.Length; ++configIndex )
			{
				if ( configEntries[configIndex].Contains(configKeyString) )
				{
					configEntries[configIndex] = configEntries[configIndex].Substring(0, configEntries[configIndex].IndexOf(CFG_DELIMITER) + 1);
					configEntries[configIndex] += newValue;
				}
			}

			File.WriteAllLines(CONFIG_FILENAME, configEntries);
		}
		#endregion

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

		private void RawsLocationTextBox_TextChanged(object sender, System.EventArgs e)
		{
			BeginScriptCreationButton.Enabled = AreScriptCreationInputsValid();
		}

		private void OutputLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			CommonOpenFileDialog outputLocationDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};

			if ( outputLocationDialog.ShowDialog() == CommonFileDialogResult.Ok )
			{
				OutputLocationTextBox.Text = outputLocationDialog.FileName;
			}
		}

		private void OutputLocationTextBox_TextChanged(object sender, EventArgs e)
		{
			BeginScriptCreationButton.Enabled = AreScriptCreationInputsValid();
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

		private void TranslatorNameTextBox_TextChanged(object sender, EventArgs e)
		{
			BeginScriptCreationButton.Enabled = AreScriptCreationInputsValid();
		}

		private void ChapterNumberTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				if ( AreScriptCreationInputsValid() )
				{
					BeginScriptCreation();
				}
			}
		}

		private void ChapterNumberTextBox_TextChanged(object sender, EventArgs e)
		{
			BeginScriptCreationButton.Enabled = AreScriptCreationInputsValid();
		}

		private void BeginScriptCreationButton_MouseClick(object sender, MouseEventArgs e) => BeginScriptCreation();

		private bool AreScriptCreationInputsValid()
		{
			if ( !VerifyRawsLocation(RawsLocationTextBox) )
			{
				return false;
			}

			if ( !VerifyOutputLocation() )
			{
				return false;
			}

			if ( !VerifyTranslatorName() )
			{
				return false;
			}

			if ( !VerifyChapterNumber() )
			{
				return false;
			}

			ScriptCreationErrorLabel.Visible = false;

			return true;
		}

		private bool VerifyRawsLocation(TextBox rawsLocationTextBox)
		{
			// The Raws location must be in a valid directory with at least one image that is either .png or .jpg/.jpeg
			string pathToCheck = rawsLocationTextBox.Text;

			if ( !Directory.Exists(pathToCheck) )
			{
				ShowError(ScriptCreationErrorLabel, "The selected Raws Directory does not exist.");
				return false;
			}

			DirectoryInfo rawsDirectory = new DirectoryInfo(pathToCheck);

			var files = rawsDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
				.Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
					|| s.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase));

			if ( files.Count() == 0 )
			{
				ShowError(ScriptCreationErrorLabel, "The Raws folder does not contain any supported image types.\nSupported types: .png, .jpg, .jpeg");
				return false;
			}

			int index = pathToCheck.LastIndexOf('\\');
			string chapterFolderPath = pathToCheck.Substring(0, index);
			string chapterFolderName = chapterFolderPath.Substring(chapterFolderPath.LastIndexOf('\\') + 1);

			Match result = Regex.Match(chapterFolderName, @"[Ch]{2}[a-z _-]*([0-9]*([.,][0-9]*)?$)", RegexOptions.IgnoreCase);

			// Check to see if it matches the folder hierarchy that I use
			if ( result.Success )
			{
				OutputLocationTextBox.Text = chapterFolderPath;
				ChapterNumberTextBox.Text = result.Groups[1].Value;
			}
			else // Default to placing in the same directory as raws
			{
				OutputLocationTextBox.Text = pathToCheck;
			}

			RawsFiles = files;

			return true;
		}

		private bool VerifyChapterNumber()
		{
			// Chapter Number cannot be empty and these characters cannot be present: / \ | : * ? < >
			if ( string.IsNullOrWhiteSpace(ChapterNumberTextBox.Text) )
			{
				ShowError(ScriptCreationErrorLabel, "Chapter Number cannot be empty or just spaces,\nand must contain at least one character.");
				return false;
			}

			if ( ChapterNumberTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1 )
			{
				ShowError(ScriptCreationErrorLabel, "Chapter number cannot contain any of the following\ncharacters: / \\ | : * ? < >");
				return false;
			}

			SelectedChapterNumber = ChapterNumberTextBox.Text;

			return true;
		}

		private bool VerifyOutputLocation()
		{
			// Output Location must be a valid directory which can be accessed and written to
			string pathToCheck = OutputLocationTextBox.Text;

			if ( !Directory.Exists(pathToCheck) )
			{
				ShowError(ScriptCreationErrorLabel, "The selected Output Directory does not exist.");
				return false;
			}

			try
			{
				System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(pathToCheck);
			}
			catch ( UnauthorizedAccessException )
			{
				ShowError(ScriptCreationErrorLabel, "You do not have access (Read/Write permissions)\nto the selected Output Directory.");
				return false;
			}

			string chapterFolderName = pathToCheck.Substring(pathToCheck.LastIndexOf('\\') + 1);

			Match result = Regex.Match(chapterFolderName, @"[Ch]{2}[a-z _-]*([0-9]*([.,][0-9]*)?$)", RegexOptions.IgnoreCase);

			// Check to see if it matches the folder hierarchy that I use
			if ( result.Success )
			{
				ChapterNumberTextBox.Text = result.Groups[1].Value;
			}

			OutputLocationFullPath = pathToCheck;

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

			TranslatorName = TranslatorNameTextBox.Text;

			return true;
		}

		private void BeginScriptCreation()
		{
			UpdateConfigEntryInFile(CFG_TRANSLATOR_NAME, TranslatorName);

			RawsViewerForm rawsViewerForm = new RawsViewerForm(OutputLocationFullPath, ScriptLocationFullPath, SelectedChapterNumber, RawsFiles, TranslatorName, true);
			this.Hide();
			rawsViewerForm.ShowDialog();
			this.Show();
		}
        #endregion

        #region ScriptEditing
        private void ScriptLocationButton_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog scriptLocationDialog = new OpenFileDialog();

            if ( scriptLocationDialog.ShowDialog() == DialogResult.OK )
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

		private void ScriptEditingRawsLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			CommonOpenFileDialog rawsLocationDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};

			if ( rawsLocationDialog.ShowDialog() == CommonFileDialogResult.Ok )
			{
				ScriptEditingRawsLocationTextBox.Text = rawsLocationDialog.FileName;
			}
		}

		private void ScriptEditingRawsLocationTextBox_TextChanged(object sender, EventArgs e)
		{
			BeginScriptEditingButton.Enabled = AreScriptEditingInputsValid();
		}

		private void BeginScriptEditingButton_MouseClick(object sender, MouseEventArgs e) => BeginScriptEditing();

		private bool AreScriptEditingInputsValid()
		{
			if ( !VerifyScriptLocation() )
			{
				return false;
			}

			if ( !VerifyRawsLocation(ScriptEditingRawsLocationTextBox) )
			{
				return false;
			}

			ScriptEditingErrorLabel.Visible = false;

			return true;
		}

		private bool VerifyScriptLocation()
		{
			// The Script location must be a valid .txt file, with permissions, and optionally have the images next to it, or in a folder called Raws below it
			string pathToCheck = ScriptLocationTextBox.Text;

			if ( !File.Exists(pathToCheck) )
			{
				ShowError(ScriptCreationErrorLabel, "The selected Script Location does not exist.");
				return false;
			}

			try
			{
				System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(pathToCheck);
			}
			catch ( UnauthorizedAccessException )
			{
				ShowError(ScriptCreationErrorLabel, "You do not have access (Read/Write permissions)\nto the selected Script Location.");
				return false;
			}

			string chapterFolderPath = pathToCheck.Substring(0, pathToCheck.LastIndexOf('\\'));
			string chapterFolderName = chapterFolderPath.Substring(chapterFolderPath.LastIndexOf('\\') + 1);

			Match result = Regex.Match(chapterFolderName, @"[Ch]{2}[a-z _-]*([0-9]*([.,][0-9]*)?$)", RegexOptions.IgnoreCase);

			if ( result.Success )
			{
				string rawsFolderPath = chapterFolderPath + "\\Raws\\";

				// Check to see if it matches the folder hierarchy that I use
				if ( Directory.Exists(rawsFolderPath) )
				{
					ScriptEditingRawsLocationTextBox.Text = rawsFolderPath;
				}
				else // Otherwise, default assume that raws are next to script
				{
					ScriptEditingRawsLocationTextBox.Text = chapterFolderPath;
				}
			}

			ScriptLocationFullPath = pathToCheck;

			return true;
		}

		private void BeginScriptEditing()
        {
            RawsViewerForm rawsViewerForm = new RawsViewerForm(OutputLocationFullPath, ScriptLocationFullPath, SelectedChapterNumber, RawsFiles, TranslatorName, false);
            this.Hide();
            rawsViewerForm.ShowDialog();
            this.Show();
        }
		#endregion
	}
}
