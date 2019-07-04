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

		private bool WasRawsLocationVerified { get; set; }
		private bool WasOutputLocationVerified { get; set; }
		private bool WasTranslatorNameVerified { get; set; }
		private bool WasChapterNameVerified { get; set; }
		private bool WasScriptLocationVerified { get; set; }
		private bool WasEditingRawsLocationVerified { get; set; }
		private string OutputLocationFullPath { get; set; }
		private string TranslatorName { get; set; }
		private string ScriptLocationFullPath { get; set; }
		private string SelectedChapterNumber { get; set; }
		private IEnumerable<FileInfo> RawsFiles { get; set; }
		
		
		public MainForm()
		{
			InitializeComponent();
			ReadTSMConfigFile();
		}

		#region ConfigReading
		private void ReadTSMConfigFile()
		{
			if ( !File.Exists(CONFIG_FILENAME) )
			{
				File.AppendAllText(CONFIG_FILENAME, CFG_TRANSLATOR_NAME + Environment.UserName);

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
				File.AppendAllText(CONFIG_FILENAME, CFG_TRANSLATOR_NAME + Environment.UserName);

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
			using ( CommonOpenFileDialog rawsLocationDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			} )
			{
				if ( rawsLocationDialog.ShowDialog() == CommonFileDialogResult.Ok )
				{
					RawsLocationTextBox.Text = rawsLocationDialog.FileName;
					WasRawsLocationVerified = VerifyRawsLocation(RawsLocationTextBox);
				}
			}
		}

		private void RawsLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !VerifyRawsLocation(RawsLocationTextBox);

			if ( e.Cancel )
			{
				WasRawsLocationVerified = false;
			}
		}

		private void RawsLocationTextBox_Validated(object sender, EventArgs e)
		{
			WasRawsLocationVerified = true;
			MainFormErrorProvider.SetError(RawsLocationTextBox, null);
		}

		private void OutputLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			using ( CommonOpenFileDialog outputLocationDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			} )
			{
				if ( outputLocationDialog.ShowDialog() == CommonFileDialogResult.Ok )
				{
					OutputLocationTextBox.Text = outputLocationDialog.FileName;
					WasOutputLocationVerified = VerifyOutputLocation();
				}
			}
		}

		private void OutputLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !VerifyOutputLocation();

			if ( e.Cancel )
			{
				WasOutputLocationVerified = false;
			}
		}

		private void OutputLocationTextBox_Validated(object sender, EventArgs e)
		{
			WasOutputLocationVerified = true;
			MainFormErrorProvider.SetError(OutputLocationTextBox, null);
		}

		private void TranslatorNameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !VerifyTranslatorName();

			if ( e.Cancel )
			{
				WasTranslatorNameVerified = false;
			}
		}

		private void TranslatorNameTextBox_Validated(object sender, EventArgs e)
		{
			WasTranslatorNameVerified = true;
			MainFormErrorProvider.SetError(TranslatorNameTextBox, null);
		}

		private void ChapterNumberTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !VerifyChapterNumber();

			if ( e.Cancel )
			{
				WasChapterNameVerified = false;
			}
		}

		private void ChapterNumberTextBox_Validated(object sender, EventArgs e)
		{
			WasChapterNameVerified = true;
			MainFormErrorProvider.SetError(ChapterNumberTextBox, null);
		}

		private void BeginScriptCreationButton_MouseClick(object sender, MouseEventArgs e)
		{
			this.ValidateChildren();

			if ( AreScriptCreationInputsValid() )
			{
				BeginScriptCreation();
			}
		}

		private bool AreScriptCreationInputsValid()
		{
			if ( !WasRawsLocationVerified )
			{
				return false;
			}

			if ( !WasOutputLocationVerified )
			{
				return false;
			}

			if ( !WasTranslatorNameVerified )
			{
				return false;
			}

			if ( !WasChapterNameVerified )
			{
				return false;
			}

			return true;
		}

		private bool VerifyRawsLocation(TextBox rawsLocationTextBox)
		{
			// The Raws location must be in a valid directory with at least one image that is either .png or .jpg/.jpeg
			string pathToCheck = rawsLocationTextBox.Text;

			if ( !Directory.Exists(pathToCheck) )
			{
				MainFormErrorProvider.SetError(rawsLocationTextBox, "The selected Raws Directory does not exist.");
				return false;
			}

			DirectoryInfo rawsDirectory = new DirectoryInfo(pathToCheck);

			var files = rawsDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
				.Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
					|| s.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase));

			if ( files.Count() == 0 )
			{
				MainFormErrorProvider.SetError(rawsLocationTextBox, "The Raws folder does not contain any supported image types.\nSupported types: .png, .jpg, .jpeg");
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
				MainFormErrorProvider.SetError(OutputLocationTextBox, null);
				MainFormErrorProvider.SetError(ChapterNumberTextBox, null);
			}
			else // Default to placing in the same directory as raws
			{
				OutputLocationTextBox.Text = pathToCheck;
				MainFormErrorProvider.SetError(OutputLocationTextBox, null);
			}

			RawsFiles = files;
			MainFormErrorProvider.SetError(rawsLocationTextBox, null);

			return true;
		}

		private bool VerifyOutputLocation()
		{
			// Output Location must be a valid directory which can be accessed and written to
			string pathToCheck = OutputLocationTextBox.Text;

			if ( !Directory.Exists(pathToCheck) )
			{
				MainFormErrorProvider.SetError(OutputLocationTextBox, "The selected Output Directory does not exist.");
				return false;
			}

			try
			{
				System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(pathToCheck);
			}
			catch ( UnauthorizedAccessException )
			{
				MainFormErrorProvider.SetError(OutputLocationTextBox, "You do not have access (Read/Write permissions)\nto the selected Output Directory.");
				return false;
			}

			string chapterFolderName = pathToCheck.Substring(pathToCheck.LastIndexOf('\\') + 1);

			Match result = Regex.Match(chapterFolderName, @"[Ch]{2}[a-z _-]*([0-9]*([.,][0-9]*)?$)", RegexOptions.IgnoreCase);

			// Check to see if it matches the folder hierarchy that I use
			if ( result.Success )
			{
				ChapterNumberTextBox.Text = result.Groups[1].Value;
				MainFormErrorProvider.SetError(ChapterNumberTextBox, null);
			}

			OutputLocationFullPath = pathToCheck;
			MainFormErrorProvider.SetError(OutputLocationTextBox, null);

			return true;
		}

		private bool VerifyTranslatorName()
		{
			// Name cannot be empty and these characters cannot be present: / \ | : * ? < >
			if ( string.IsNullOrWhiteSpace(TranslatorNameTextBox.Text) )
			{
				MainFormErrorProvider.SetError(TranslatorNameTextBox, "Translator name cannot be empty or just spaces,\nand must contain at least one letter.");
				return false;
			}

			if ( TranslatorNameTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1 )
			{
				MainFormErrorProvider.SetError(TranslatorNameTextBox, "Translator name cannot contain any of the following\ncharacters: / \\ | : * ? < >");
				return false;
			}

			TranslatorName = TranslatorNameTextBox.Text;

			return true;
		}

		private bool VerifyChapterNumber()
		{
			// Chapter Number cannot be empty and these characters cannot be present: / \ | : * ? < >
			if ( string.IsNullOrWhiteSpace(ChapterNumberTextBox.Text) )
			{
				MainFormErrorProvider.SetError(ChapterNumberTextBox, "Chapter Number cannot be empty or just spaces,\nand must contain at least one character.");
				return false;
			}

			if ( ChapterNumberTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1 )
			{
				MainFormErrorProvider.SetError(ChapterNumberTextBox, "Chapter number cannot contain any of the following\ncharacters: / \\ | : * ? < >");
				return false;
			}

			SelectedChapterNumber = ChapterNumberTextBox.Text;

			return true;
		}

		private void BeginScriptCreation()
		{
			UpdateConfigEntryInFile(CFG_TRANSLATOR_NAME, TranslatorName);

			using ( RawsViewerForm rawsViewerForm = new RawsViewerForm(OutputLocationFullPath, ScriptLocationFullPath, SelectedChapterNumber, RawsFiles, TranslatorName, true) )
			{
				this.Hide();
				rawsViewerForm.ShowDialog();
				this.Show();
			}
		}
        #endregion

        #region ScriptEditing
        private void ScriptLocationButton_MouseClick(object sender, MouseEventArgs e)
        {
			using ( OpenFileDialog scriptLocationDialog = new OpenFileDialog() )
			{

				if ( scriptLocationDialog.ShowDialog() == DialogResult.OK )
				{
					if ( !scriptLocationDialog.CheckPathExists )
					{
						MainFormErrorProvider.SetError(ScriptEditingRawsLocationTextBox, "The selected path does not exist.");
					}

					if ( !scriptLocationDialog.CheckFileExists )
					{
						MainFormErrorProvider.SetError(ScriptEditingRawsLocationTextBox, "The selected file does not exist.");
					}

					ScriptLocationTextBox.Text = scriptLocationDialog.FileName;
					WasScriptLocationVerified = VerifyScriptLocation();
				}
			}
        }

		private void ScriptLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !VerifyScriptLocation();

			if ( e.Cancel )
			{
				WasScriptLocationVerified = false;
			}
		}

		private void ScriptLocationTextBox_Validated(object sender, EventArgs e)
		{
			WasScriptLocationVerified = true;
			MainFormErrorProvider.SetError(ScriptLocationTextBox, null);
		}

		private void ScriptEditingRawsLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			using ( CommonOpenFileDialog rawsLocationDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			} )
			{
				if ( rawsLocationDialog.ShowDialog() == CommonFileDialogResult.Ok )
				{
					ScriptEditingRawsLocationTextBox.Text = rawsLocationDialog.FileName;
					WasEditingRawsLocationVerified = VerifyRawsLocation(ScriptEditingRawsLocationTextBox);
				}
			}
		}

		private void ScriptEditingRawsLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !VerifyRawsLocation(ScriptEditingRawsLocationTextBox);

			if ( e.Cancel )
			{
				WasEditingRawsLocationVerified = false;
			}
		}

		private void ScriptEditingRawsLocationTextBox_Validated(object sender, EventArgs e)
		{
			WasEditingRawsLocationVerified = true;
			MainFormErrorProvider.SetError(ScriptEditingRawsLocationTextBox, null);
		}

		private void BeginScriptEditingButton_MouseClick(object sender, MouseEventArgs e)
		{
			this.ValidateChildren();

			if ( AreScriptEditingInputsValid() )
			{
				BeginScriptEditing();
			}
		}

		private bool AreScriptEditingInputsValid()
		{
			if ( !WasScriptLocationVerified )
			{
				return false;
			}

			if ( !WasEditingRawsLocationVerified )
			{
				return false;
			}

			return true;
		}

		private bool VerifyScriptLocation()
		{
			// The Script location must be a valid .txt file, with permissions, and optionally have the images next to it, or in a folder called Raws below it
			string pathToCheck = ScriptLocationTextBox.Text;

			if ( !File.Exists(pathToCheck) )
			{
				MainFormErrorProvider.SetError(ScriptLocationTextBox, "The selected Script Location does not exist.");
				return false;
			}

			try
			{
				System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(pathToCheck);
			}
			catch ( UnauthorizedAccessException )
			{
				MainFormErrorProvider.SetError(ScriptLocationTextBox, "You do not have access (Read/Write permissions)\nto the selected Script Location.");
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

				MainFormErrorProvider.SetError(ScriptEditingRawsLocationTextBox, null);
			}

			ScriptLocationFullPath = pathToCheck;
			MainFormErrorProvider.SetError(ScriptLocationTextBox, null);

			return true;
		}

		private void BeginScriptEditing()
        {
			using ( RawsViewerForm rawsViewerForm = new RawsViewerForm(OutputLocationFullPath, ScriptLocationFullPath, SelectedChapterNumber, RawsFiles, TranslatorName, false) )
			{
				this.Hide();
				rawsViewerForm.ShowDialog();
				this.Show();
			}
        }
		#endregion
	}
}
