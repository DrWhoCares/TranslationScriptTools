using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;
using Onova;
using Onova.Services;

namespace TranslationScriptMaker
{
	public partial class MainForm : Form
	{
		#region Regex Constants
		private static readonly Regex VOLUME_REGEX = new(@"Vol(ume)?.? *([0-9]+$)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex CHAPTER_REGEX = new(@"(Ch(apter)?.? *([0-9]+([.,][0-9]+)?$){1})|(^([0-9]+([.,][0-9]+)?$){1})", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex RAWS_REGEX = new(@"Raw(s)?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex IMAGE_REGEX = new(@".*\.(png|jpg|jpeg)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		#endregion

		#region Member Variables
		private static readonly TLSMConfig CONFIG = TLSMConfig.LoadConfig();

		private bool WasRawsLocationVerified { get; set; }
		private bool WasOutputLocationVerified { get; set; }
		private bool WasTranslatorNameVerified { get; set; }
		private bool WasSeriesLocationVerified { get; set; }
		#endregion
		
		public MainForm()
		{
			InitializeComponent();
			Text = "Translation Script Maker - v" + typeof(MainForm).Assembly.GetName().Version;
			CheckForProgramUpdates();
			InitializeWithConfigValues();
		}

		public sealed override string Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		#region Initialization
		private static async void CheckForProgramUpdates()
		{
			using UpdateManager updateManager = new(
				new GithubPackageResolver("DrWhoCares", "TranslationScriptTools", "TranslationScriptTools_*.zip"),
				new ZipPackageExtractor()
			);

			// Check for updates
			try
			{
				var check = await updateManager.CheckForUpdatesAsync();

				// If there are no updates, continue on silently
				if ( !check.CanUpdate )
				{
					return;
				}

				DialogResult result = MessageBox.Show("There is a new version (" + check.LastVersion + ") available for download. Would you like to download and install it?", "New Version Update", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

				if ( result != DialogResult.Yes )
				{
					return;
				}

				// Prepare the latest update
				await updateManager.PrepareUpdateAsync(check.LastVersion ?? throw new InvalidOperationException());

				// Launch updater and exit
				updateManager.LaunchUpdater(check.LastVersion);
				Application.Exit();
			}
			catch ( Exception e )
			{
				MessageBox.Show("Checking for updates threw an exception:\n\"" + e.Message + "\"\n\nYou may not be able to access api.github.com. You can safely continue using this offline.", "Error Checking For Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void InitializeWithConfigValues()
		{
			if ( !Directory.Exists(CONFIG.RawsLocation) )
			{
				CONFIG.RawsLocation = "";
			}

			if ( !Directory.Exists(CONFIG.ScriptOutputLocation) )
			{
				CONFIG.ScriptOutputLocation = "";
			}

			RawsLocationTextBox.Text = !string.IsNullOrWhiteSpace(CONFIG.RawsLocation) ? CONFIG.RawsLocation : "";
			OutputLocationTextBox.Text = !string.IsNullOrWhiteSpace(CONFIG.ScriptOutputLocation) ? CONFIG.ScriptOutputLocation : "";
			TranslatorNameTextBox.Text = !string.IsNullOrWhiteSpace(CONFIG.TranslatorName) ? CONFIG.TranslatorName : "";
			OutputAsTypesettererCheckBox.Checked = CONFIG.ShouldOutputAsTypesetterCompliant;

			switch ( CONFIG.ScriptOutputToChoice )
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
					CONFIG.ScriptOutputToChoice = OutputToChoice.ChapterFolder;
					OutputToChapterFolderRadioButton.Checked = true;
					break;
			}

			OutputLocationButton.Enabled = OutputToCustomLocationRadioButton.Checked;
			OutputLocationTextBox.Enabled = OutputToCustomLocationRadioButton.Checked;

			if ( !string.IsNullOrWhiteSpace(RawsLocationTextBox.Text) )
			{
				ParseRawsDirectoryForSeries(RawsLocationTextBox.Text);

				if ( !string.IsNullOrWhiteSpace(CONFIG.LastSelectedSeries) )
				{
					int selectedSeriesIndex = SeriesSelectionComboBox.Items.IndexOf(CONFIG.LastSelectedSeries);

					if ( selectedSeriesIndex > -1 )
					{
						SeriesSelectionComboBox.SelectedItem = SeriesSelectionComboBox.Items[selectedSeriesIndex];
					}
					else
					{
						CONFIG.LastSelectedSeries = ""; // Since it can no longer find the last selected series, reset the setting
					}
				}

				if ( SeriesSelectionComboBox.SelectedItem != null )
				{
					ParseSeriesDirectoryForChapters(RawsLocationTextBox.Text + "/" + SeriesSelectionComboBox.SelectedItem);

					if ( !string.IsNullOrWhiteSpace(CONFIG.LastSelectedChapter) )
					{
						int selectedChapterIndex = ChapterSelectionComboBox.Items.IndexOf(CONFIG.LastSelectedChapter);

						if ( selectedChapterIndex > -1 )
						{
							ChapterSelectionComboBox.SelectedItem = ChapterSelectionComboBox.Items[selectedChapterIndex];
						}
						else
						{
							CONFIG.LastSelectedChapter = ""; // Since it can no longer find the last selected chapter, reset the setting
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

			if ( !Directory.Exists(rawsDirectoryPath) )
			{
				WasRawsLocationVerified = false;
				MainFormErrorProvider.SetError(RawsLocationTextBox, "The Raws Location does not exist.");
				return;
			}

			SeriesSelectionComboBox.Items.Clear();
			SeriesSelectionComboBox.DropDownHeight = 106;

			DirectoryInfo[] seriesDirectories = new DirectoryInfo(rawsDirectoryPath).GetDirectories();

			foreach ( DirectoryInfo seriesDirInfo in seriesDirectories.OrderByAlphaNumeric(DirectoryOrderer.GetDirectoryName) )
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

				int selectedSeriesIndex = SeriesSelectionComboBox.Items.IndexOf(CONFIG.LastSelectedSeries);

				if ( selectedSeriesIndex > -1 )
				{
					SeriesSelectionComboBox.SelectedItem = SeriesSelectionComboBox.Items[selectedSeriesIndex];
				}
			}
		}

		private static bool DoesSeriesDirectoryContainChapters(string seriesDirectoryPath)
		{
			return new DirectoryInfo(seriesDirectoryPath)
				.GetDirectories("*", SearchOption.TopDirectoryOnly)
				.Any(subdirectory => CHAPTER_REGEX.IsMatch(subdirectory.Name) || IsDirectoryAVolume(subdirectory));
		}

		private static bool IsDirectoryAVolume(DirectoryInfo directoryInfo)
		{
			return VOLUME_REGEX.IsMatch(directoryInfo.Name) && directoryInfo
				.GetDirectories("*", SearchOption.TopDirectoryOnly)
				.Any(subDirectory => CHAPTER_REGEX.IsMatch(subDirectory.Name));
		}

		private void ParseSeriesDirectoryForChapters(string seriesDirectoryPath)
		{
			if ( string.IsNullOrWhiteSpace(seriesDirectoryPath) )
			{
				return;
			}

			ChapterSelectionComboBox.Items.Clear();
			ChapterSelectionComboBox.DropDownHeight = 106;

			DirectoryInfo seriesDirectoryInfo = new(seriesDirectoryPath);
			IEnumerable<DirectoryInfo> chapterDirectories = seriesDirectoryInfo.GetDirectories("*", SearchOption.AllDirectories).Where((directory) => CHAPTER_REGEX.IsMatch(directory.Name));

			foreach ( DirectoryInfo chapterDirInfo in chapterDirectories.OrderByAlphaNumeric(DirectoryOrderer.GetDirectoryName) )
			{
				if ( IsDirectoryAVolume(chapterDirInfo) || (!DoesChapterDirectoryContainRaws(chapterDirInfo.FullName) && !DoesChapterDirectoryContainRawsFolder(chapterDirInfo.FullName)) )
				{
					continue;
				}

				if ( chapterDirInfo.Parent != null && chapterDirInfo.Parent.Name == seriesDirectoryInfo.Name )
				{
					ChapterSelectionComboBox.Items.Add(chapterDirInfo.Name);
				}
				else
				{
					if ( chapterDirInfo.Parent != null && IsDirectoryAVolume(chapterDirInfo.Parent) )
					{
						ChapterSelectionComboBox.Items.Add(chapterDirInfo.Parent.Name + "/" + chapterDirInfo.Name);
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
			return new DirectoryInfo(chapterDirectoryPath).GetFiles("*", SearchOption.TopDirectoryOnly).Any(file => IMAGE_REGEX.IsMatch(file.Name));
		}

		private static bool DoesChapterDirectoryContainRawsFolder(string chapterDirectoryPath)
		{
			return new DirectoryInfo(chapterDirectoryPath)
				.GetDirectories("*", SearchOption.TopDirectoryOnly)
				.Any(subdirectory => RAWS_REGEX.IsMatch(subdirectory.Name)
					&& subdirectory.GetFiles("*", SearchOption.TopDirectoryOnly).Any(file => IMAGE_REGEX.IsMatch(file.Name)));
		}

		private void UpdateOutputLocation()
		{
			if ( CONFIG.ScriptOutputToChoice == OutputToChoice.CustomLocation || SeriesSelectionComboBox.SelectedItem == null || ChapterSelectionComboBox.SelectedItem == null )
			{
				return;
			}

			string selectedSeriesPath = RawsLocationTextBox.Text + "/" + SeriesSelectionComboBox.SelectedItem + "/";
			string selectedChapter = ChapterSelectionComboBox.SelectedItem.ToString();
			string rawsSuffix = "";

			string outputLocationFullPath = selectedSeriesPath + selectedChapter + "/";

			if ( CONFIG.ScriptOutputToChoice == OutputToChoice.WithRaws && DoesChapterDirectoryContainRawsFolder(outputLocationFullPath) )
			{
				var subdirectories = new DirectoryInfo(outputLocationFullPath).GetDirectories("*", SearchOption.TopDirectoryOnly).Where(subdirectory => RAWS_REGEX.IsMatch(subdirectory.Name));
				
				foreach ( DirectoryInfo dirInfo in subdirectories )
				{
					// Just take the first match that actually has files
					if ( dirInfo.GetFiles("*", SearchOption.TopDirectoryOnly).Any(file => IMAGE_REGEX.IsMatch(file.Name)) )
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
		#region RawsLocationTextBox
		private void RawsLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			using CommonOpenFileDialog rawsLocationDialog = new()
			{
				IsFolderPicker = true
			};

			if ( rawsLocationDialog.ShowDialog() != CommonFileDialogResult.Ok )
			{
				return;
			}

			RawsLocationTextBox.Text = rawsLocationDialog.FileName;
			CONFIG.RawsLocation = rawsLocationDialog.FileName;
			ParseRawsDirectoryForSeries(RawsLocationTextBox.Text);
		}

		private void RawsLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ParseRawsDirectoryForSeries(RawsLocationTextBox.Text);
		}
		#endregion

		#region OutputLocationTextBox
		private void OutputLocationButton_MouseClick(object sender, MouseEventArgs e)
		{
			using CommonOpenFileDialog outputLocationDialog = new()
			{
				IsFolderPicker = true
			};

			if ( outputLocationDialog.ShowDialog() != CommonFileDialogResult.Ok )
			{
				return;
			}

			OutputLocationTextBox.Text = outputLocationDialog.FileName;
			CONFIG.ScriptOutputLocation = outputLocationDialog.FileName;
			WasOutputLocationVerified = VerifyOutputLocation();
		}

		private void OutputLocationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = VerifyOutputLocation();
			WasOutputLocationVerified = e.Cancel;
		}
		#endregion

		#region TranslatorNameTextBox
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
			CONFIG.TranslatorName = TranslatorNameTextBox.Text;
			WasTranslatorNameVerified = true;
			MainFormErrorProvider.SetError(TranslatorNameTextBox, null);
		}
		#endregion

		#region SeriesSelectionComboBox
		private void SeriesSelectionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if ( SeriesSelectionComboBox.SelectedItem == null )
			{
				return;
			}

			CONFIG.LastSelectedSeries = SeriesSelectionComboBox.SelectedItem.ToString();

			if ( !string.IsNullOrWhiteSpace(RawsLocationTextBox.Text) )
			{
				ParseSeriesDirectoryForChapters(RawsLocationTextBox.Text + "/" + SeriesSelectionComboBox.SelectedItem);
				UpdateOutputLocation();
			}
		}
		#endregion

		#region ChapterSelectionComboBox
		private void ChapterSelectionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if ( ChapterSelectionComboBox.SelectedItem == null )
			{
				return;
			}

			CONFIG.LastSelectedChapter = ChapterSelectionComboBox.SelectedItem.ToString();
			UpdateOutputLocation();
		}
		#endregion

		#region OutputToRadioButtons
		private void OutputToChapterFolderRadioButton_MouseClick(object sender, MouseEventArgs e)
		{
			CONFIG.ScriptOutputToChoice = OutputToChoice.ChapterFolder;
			UpdateOutputLocation();
		}

		private void OutputWithRawsRadioButton_MouseClick(object sender, MouseEventArgs e)
		{
			CONFIG.ScriptOutputToChoice = OutputToChoice.WithRaws;
			UpdateOutputLocation();
		}

		private void OutputToCustomLocationRadioButton_MouseClick(object sender, MouseEventArgs e)
		{
			CONFIG.ScriptOutputToChoice = OutputToChoice.CustomLocation;
		}

		private void OutputToCustomLocationRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			OutputLocationButton.Enabled = OutputToCustomLocationRadioButton.Checked;
			OutputLocationTextBox.Enabled = OutputToCustomLocationRadioButton.Checked;

			if ( OutputToCustomLocationRadioButton.Checked )
			{
				OutputLocationTextBox.Text = "";
			}
		}
		#endregion

		#region Input Verification
		private void BeginScriptCreationButton_MouseClick(object sender, MouseEventArgs e)
		{
			ValidateChildren();

			if ( AreScriptCreationInputsValid() )
			{
				BeginScriptCreation();
			}
		}

		private bool AreScriptCreationInputsValid()
		{
			return WasRawsLocationVerified
				&& WasOutputLocationVerified
				&& WasTranslatorNameVerified
				&& WasSeriesLocationVerified;
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
				Directory.GetAccessControl(pathToCheck);
			}
			catch ( UnauthorizedAccessException )
			{
				MainFormErrorProvider.SetError(OutputLocationTextBox, "You do not have access (Read/Write permissions)\nto the selected Output Directory.");
				return false;
			}

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

			return true;
		}
		#endregion

		#region Start Translation Script Maker
		private void BeginScriptCreation()
		{
			SaveInputsToConfig();

			List<FileInfo> rawsFiles = GetRawsFiles(GetRawsFilesFullPath()).OrderByAlphaNumeric(DirectoryOrderer.GetFileName).ToList();

			if ( !rawsFiles.Any() )
			{
				MainFormErrorProvider.SetError(BeginScriptCreationButton, "Unable to parse any of the Raws Files. This should never happen.");
				return;
			}

			using RawsViewerForm rawsViewerForm = new(rawsFiles, OutputLocationTextBox.Text + GetOutputFilename(), CONFIG.ShouldOutputAsTypesetterCompliant);

			if ( !rawsViewerForm.IsDisposed )
			{
				Hide();
				rawsViewerForm.ShowDialog();
				Show();
			}

			ForceGarbageCollection();
		}

		private void SaveInputsToConfig()
		{
			CONFIG.RawsLocation = RawsLocationTextBox.Text;
			CONFIG.ScriptOutputLocation = OutputLocationTextBox.Text;
			CONFIG.TranslatorName = TranslatorNameTextBox.Text;
			CONFIG.LastSelectedSeries = SeriesSelectionComboBox.SelectedItem.ToString();
			CONFIG.LastSelectedChapter = ChapterSelectionComboBox.SelectedItem.ToString();
		}

		private static IEnumerable<FileInfo> GetRawsFiles(string rawsFilesFullPath)
		{
			return new DirectoryInfo(rawsFilesFullPath).EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
				.Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
					|| s.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase));
		}

		private string GetRawsFilesFullPath()
		{
			string rawsFilesFullPath = RawsLocationTextBox.Text + "/" + SeriesSelectionComboBox.SelectedItem + "/" + ChapterSelectionComboBox.SelectedItem + "/";
			return rawsFilesFullPath + GetRawsFolderName(rawsFilesFullPath);
		}

		private static string GetRawsFolderName(string chapterDirectoryFullPath)
		{
			if ( DoesChapterDirectoryContainRaws(chapterDirectoryFullPath) )
			{
				return "";
			}

			return new DirectoryInfo(chapterDirectoryFullPath)
				.GetDirectories("*", SearchOption.TopDirectoryOnly)
				.First(subdirectory => RAWS_REGEX.IsMatch(subdirectory.Name)
					&& subdirectory.GetFiles("*", SearchOption.TopDirectoryOnly)
						.Any(file => IMAGE_REGEX.IsMatch(file.Name))).Name;
		}

		private string GetOutputFilename()
		{
			return "Ch " + GetChapterNumber() + " - TL " + CONFIG.TranslatorName + ".txt";
		}

		private string GetChapterNumber()
		{
			Match result = CHAPTER_REGEX.Match(ChapterSelectionComboBox.SelectedItem.ToString());
			return string.IsNullOrWhiteSpace(result.Groups[5].Value) ? result.Groups[3].Value : result.Groups[5].Value;
		}

		private static void ForceGarbageCollection()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		#endregion

		#endregion

		private void OutputAsTypesettererCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.ShouldOutputAsTypesetterCompliant = OutputAsTypesettererCheckBox.Checked;
		}
	}

	internal static class DirectoryOrderer
	{
		public static string GetDirectoryName(DirectoryInfo dirInfo) => dirInfo.Name;

		public static string GetFileName(FileInfo fileInfo) => fileInfo.Name;

		public static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
		{
			IEnumerable<T> sourceAsListEnumerable = source.ToList();
			int max = sourceAsListEnumerable.SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => m.Value.Length)).DefaultIfEmpty().Max();
			return sourceAsListEnumerable.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
		}
	}
}
