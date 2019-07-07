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
		private static readonly Regex VolumeRegex = new Regex(@"Vol(ume)?.? *([0-9]+$)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex ChapterRegex = new Regex(@"Ch(apter)?.? *([0-9]+([.,][0-9]+)?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex RawsRegex = new Regex(@"Raw(s)?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex ImageRegex = new Regex(@".*\.(png|jpg|jpeg)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private static TLSMConfig Config = TLSMConfig.LoadConfig();

		private bool WasRawsLocationVerified { get; set; }
		private bool WasOutputLocationVerified { get; set; }
		private bool WasTranslatorNameVerified { get; set; }
		private bool WasSeriesLocationVerified { get; set; }


		private bool WasChapterLocationVerified { get; set; }
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
			InitializeWithConfigValues();
		}

		#region Initialization
		private void InitializeWithConfigValues()
		{
			RawsLocationTextBox.Text = !string.IsNullOrWhiteSpace(Config.RawsLocation) ? Config.RawsLocation : "";
			OutputLocationTextBox.Text = !string.IsNullOrWhiteSpace(Config.ScriptOutputLocation) ? Config.ScriptOutputLocation : "";
			TranslatorNameTextBox.Text = !string.IsNullOrWhiteSpace(Config.TranslatorName) ? Config.TranslatorName : "";

			switch ( Config.ScriptOutputToChoice )
			{
				case OutputToChoice.ChapterFolder:
					OutputToChapterFolderRadioButton.Checked = true;
					break;
				case OutputToChoice.WithRaws:
					OutputWithRawsRadioButton.Checked = true;
					break;
				case OutputToChoice.CustomLocation:
					OutputToCustomLocationRadioButton.Checked = true;
					break;

				default:
					Config.ScriptOutputToChoice = OutputToChoice.ChapterFolder;
					OutputToChapterFolderRadioButton.Checked = true;
					break;
			}

			OutputLocationButton.Enabled = OutputToCustomLocationRadioButton.Checked;
			OutputLocationTextBox.Enabled = OutputToCustomLocationRadioButton.Checked;

			if ( !string.IsNullOrWhiteSpace(RawsLocationTextBox.Text) )
			{
				ParseRawsDirectoryForSeries(RawsLocationTextBox.Text);

				if ( !string.IsNullOrWhiteSpace(Config.LastSelectedSeries) )
				{
					int selectedSeriesIndex = SeriesSelectionComboBox.Items.IndexOf(Config.LastSelectedSeries);

					if ( selectedSeriesIndex > -1 )
					{
						SeriesSelectionComboBox.SelectedItem = SeriesSelectionComboBox.Items[selectedSeriesIndex];
					}
					else
					{
						Config.LastSelectedSeries = ""; // Since it can no longer find the last selected series, reset the setting
					}
				}

				if ( SeriesSelectionComboBox.SelectedItem != null )
				{
					ParseSeriesDirectoryForChapters(RawsLocationTextBox.Text + "\\" + SeriesSelectionComboBox.SelectedItem.ToString());

					if ( !string.IsNullOrWhiteSpace(Config.LastSelectedChapter) )
					{
						int selectedChapterIndex = ChapterSelectionComboBox.Items.IndexOf(Config.LastSelectedChapter);

						if ( selectedChapterIndex > -1 )
						{
							ChapterSelectionComboBox.SelectedItem = ChapterSelectionComboBox.Items[selectedChapterIndex];
						}
						else
						{
							Config.LastSelectedChapter = ""; // Since it can no longer find the last selected chapter, reset the setting
						}
					}
				}
			}

			UpdateOutputLocation();
		}

		private void ParseRawsDirectoryForSeries(string rawsDirectoryPath)
		{
			if ( string.IsNullOrWhiteSpace(rawsDirectoryPath) )
			{
				return;
			}

			SeriesSelectionComboBox.Items.Clear();
			SeriesSelectionComboBox.DropDownHeight = 106;

			DirectoryInfo[] seriesDirectories = new DirectoryInfo(rawsDirectoryPath).GetDirectories();

			foreach ( DirectoryInfo seriesDirInfo in DirectoryOrderer.OrderByAlphaNumeric(seriesDirectories, DirectoryOrderer.GetDirectoryName) )
			{
				if ( DoesSeriesDirectoryContainChapters(seriesDirInfo.FullName) )
				{
					SeriesSelectionComboBox.Items.Add(seriesDirInfo.Name);
				}
			}

			if ( SeriesSelectionComboBox.Items.Count == 0 )
			{
				WasRawsLocationVerified = false;
				MainFormErrorProvider.SetError(RawsLocationTextBox, "The Raws Location doesn't contain any Chapter folders in any of its subfolders.");
			}
			else
			{
				WasRawsLocationVerified = true;
				MainFormErrorProvider.SetError(RawsLocationTextBox, null);
			}
		}

		private static bool DoesSeriesDirectoryContainChapters(string seriesDirectoryPath)
		{
			return new DirectoryInfo(seriesDirectoryPath).GetDirectories("*", SearchOption.TopDirectoryOnly)
				.Where((subdirectory) => ChapterRegex.IsMatch(subdirectory.Name) || IsSeriesSubdirectoryAVolume(subdirectory)).Count() > 0;
		}

		private static bool IsSeriesSubdirectoryAVolume(DirectoryInfo subdirectoryInfo)
		{
			return VolumeRegex.IsMatch(subdirectoryInfo.Name) && subdirectoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly)
				.Where((subsubDirectory) => ChapterRegex.IsMatch(subsubDirectory.Name)).Count() > 0;
		}

		private void ParseSeriesDirectoryForChapters(string seriesDirectoryPath)
		{
			if ( string.IsNullOrWhiteSpace(seriesDirectoryPath) )
			{
				return;
			}

			ChapterSelectionComboBox.Items.Clear();
			ChapterSelectionComboBox.DropDownHeight = 106;

			DirectoryInfo seriesDirectoryInfo = new DirectoryInfo(seriesDirectoryPath);
			IEnumerable<DirectoryInfo> chapterDirectories = seriesDirectoryInfo.GetDirectories("*", SearchOption.AllDirectories).Where((directory) => ChapterRegex.IsMatch(directory.Name));

			foreach ( DirectoryInfo chapterDirInfo in DirectoryOrderer.OrderByAlphaNumeric(chapterDirectories, DirectoryOrderer.GetDirectoryName) )
			{
				if ( DoesChapterDirectoryContainRaws(chapterDirInfo.FullName) || DoesChapterDirectoryContainRawsFolder(chapterDirInfo.FullName) )
				{
					if ( chapterDirInfo.Parent.Name == seriesDirectoryInfo.Name )
					{
						ChapterSelectionComboBox.Items.Add(chapterDirInfo.Name);
					}
					else
					{
						ChapterSelectionComboBox.Items.Add(chapterDirInfo.Parent.Name + "\\" + chapterDirInfo.Name);
					}
				}
			}

			if ( ChapterSelectionComboBox.Items.Count == 0 )
			{
				WasSeriesLocationVerified = false;
				MainFormErrorProvider.SetError(SeriesSelectionComboBox, "The Raws Location doesn't contain any Chapter folders in any of its subfolders.");
			}
			else
			{
				WasSeriesLocationVerified = true;
				MainFormErrorProvider.SetError(SeriesSelectionComboBox, null);
			}
		}

		private static bool DoesChapterDirectoryContainRaws(string chapterDirectoryPath)
		{
			return new DirectoryInfo(chapterDirectoryPath).GetFiles("*", SearchOption.TopDirectoryOnly).Where((file) => ImageRegex.IsMatch(file.Name)).Count() > 0;
		}

		private static bool DoesChapterDirectoryContainRawsFolder(string chapterDirectoryPath)
		{
			return new DirectoryInfo(chapterDirectoryPath).GetDirectories("*", SearchOption.TopDirectoryOnly)
				.Where((subdirectory) => RawsRegex.IsMatch(subdirectory.Name)
					&& subdirectory.GetFiles("*", SearchOption.TopDirectoryOnly)
						.Where((file) => ImageRegex.IsMatch(file.Name)).Count() > 0).Count() > 0;
		}

		private void UpdateOutputLocation()
		{
			if ( Config.ScriptOutputToChoice == OutputToChoice.CustomLocation || SeriesSelectionComboBox.SelectedItem == null || ChapterSelectionComboBox.SelectedItem == null )
			{
				return;
			}

			string selectedSeriesPath = RawsLocationTextBox.Text + "\\" + SeriesSelectionComboBox.SelectedItem.ToString() + "\\";
			string selectedChapter = ChapterSelectionComboBox.SelectedItem.ToString();
			string rawsSuffix = string.Empty;

			string outputLocationFullPath = selectedSeriesPath + selectedChapter;

			if ( Config.ScriptOutputToChoice == OutputToChoice.WithRaws && DoesChapterDirectoryContainRawsFolder(outputLocationFullPath) )
			{
				var subdirectories = new DirectoryInfo(outputLocationFullPath).GetDirectories("*", SearchOption.TopDirectoryOnly).Where((subdirectory) => RawsRegex.IsMatch(subdirectory.Name));
				
				foreach ( DirectoryInfo dirInfo in subdirectories )
				{
					// Just take the first match that actually has files
					if ( dirInfo.GetFiles("*", SearchOption.TopDirectoryOnly).Where((file) => ImageRegex.IsMatch(file.Name)).Count() > 0 )
					{
						rawsSuffix = dirInfo.Name;
						break;
					}
				}
			}

			OutputLocationTextBox.Text = outputLocationFullPath + rawsSuffix;
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
					Config.RawsLocation = rawsLocationDialog.FileName;
					ParseRawsDirectoryForSeries(RawsLocationTextBox.Text);
				}
			}
		}

		private void RawsLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ParseRawsDirectoryForSeries(RawsLocationTextBox.Text);
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
					Config.ScriptOutputLocation = outputLocationDialog.FileName;
					WasOutputLocationVerified = VerifyOutputLocation();
				}
			}
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

		private void SeriesSelectionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if ( SeriesSelectionComboBox.SelectedItem == null )
			{
				return;
			}

			Config.LastSelectedSeries = SeriesSelectionComboBox.SelectedItem.ToString();

			if ( !string.IsNullOrWhiteSpace(RawsLocationTextBox.Text) )
			{
				ParseSeriesDirectoryForChapters(RawsLocationTextBox.Text + "\\" + SeriesSelectionComboBox.SelectedItem.ToString());
				UpdateOutputLocation();
			}
		}

		private void ChapterSelectionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if ( ChapterSelectionComboBox.SelectedItem == null )
			{
				return;
			}

			Config.LastSelectedChapter = ChapterSelectionComboBox.SelectedItem.ToString();
			UpdateOutputLocation();
		}

		private void OutputToChapterFolderRadioButton_MouseClick(object sender, MouseEventArgs e)
		{
			Config.ScriptOutputToChoice = OutputToChoice.ChapterFolder;
			UpdateOutputLocation();
		}

		private void OutputWithRawsRadioButton_MouseClick(object sender, MouseEventArgs e)
		{
			Config.ScriptOutputToChoice = OutputToChoice.WithRaws;
			UpdateOutputLocation();
		}

		private void OutputToCustomLocationRadioButton_MouseClick(object sender, MouseEventArgs e)
		{
			Config.ScriptOutputToChoice = OutputToChoice.CustomLocation;
		}

		private void OutputToCustomLocationRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			OutputLocationButton.Enabled = OutputToCustomLocationRadioButton.Checked;
			OutputLocationTextBox.Enabled = OutputToCustomLocationRadioButton.Checked;

			if ( OutputToCustomLocationRadioButton.Checked )
			{
				OutputLocationTextBox.Text = string.Empty;
			}
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

			if ( !WasSeriesLocationVerified )
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
				//ChapterNumberTextBox.Text = result.Groups[1].Value;
				MainFormErrorProvider.SetError(OutputLocationTextBox, null);
				//MainFormErrorProvider.SetError(ChapterNumberTextBox, null);
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

		private void BeginScriptCreation()
		{
			Config.SaveConfig();

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

		private void OutputLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = VerifyOutputLocation();
			WasOutputLocationVerified = e.Cancel;
		}
	}

	internal static class DirectoryOrderer
	{
		public static string GetDirectoryName(DirectoryInfo dirInfo) => dirInfo.Name;

		public static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
		{
			int max = source.SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => m.Value.Length)).DefaultIfEmpty().Max();
			return source.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
		}
	}
}
