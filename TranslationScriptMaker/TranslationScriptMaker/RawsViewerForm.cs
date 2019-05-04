using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Drawing;

namespace TranslationScriptMaker
{

	public partial class RawsViewerForm : Form
	{
		private const int MAX_PANELS = 12;
		private const string PAGE_HEADER_BEGIN = "----------# Page ";
		private const string PAGE_HEADER_END = " #----------{\n";
		private const string PANEL_HEADER_BEGIN = "\n---# Panel ";
		private const string PANEL_HEADER_END = " #---{\n";
		private const string PANEL_SFX_SECTION = "\n[SFX]{\n\n}";
		private const string PANEL_FOOTER = "\n---#####---}\n";
		private const string PAGE_FOOTER = "\n----------##########----------}\n\n";

		private static readonly IEnumerable<string> PAGE_SYNTAX_MARKERS = new HashSet<string>
		{
			PAGE_HEADER_BEGIN,
			"---# Panel ",
			"[SFX]{",
			"}",
			"---#####---}",
			"----------##########----------}"
		};

		class PageInformation
		{
			public bool isSpread { get; set; }
			public int pageNumber { get; set; }
			public int totalPanels { get; set; }
			public string filename { get; set; }
			public List<bool> panelsWithSFX { get; set; }
			public List<string> pageScriptContents { get; set; }
		}

		private string RawsLocationFullPath { get; }
		private string ScriptLocationFullPath { get; }
		private string SelectedChapterNumber { get; }
		private IEnumerable<FileInfo> RawsFiles { get; set; }
		private string TranslatorsName { get; }
		private bool IsCreatingScript { get; }

		private int CurrentPageIndex { get; set; }
		private int PreviousPageIndex { get; set; }
		private List<PageInformation> PageInformations { get; set; }

		public RawsViewerForm(string rawsLocationFullPath, string scriptLocationFullPath, string chapterNumber, IEnumerable<FileInfo> rawsFiles, string translatorsName, bool isCreatingScript)
		{
			InitializeComponent();
			RawsPictureBox.MouseWheel += RawsPictureBox_MouseWheel;

			RawsLocationFullPath = rawsLocationFullPath;
			ScriptLocationFullPath = scriptLocationFullPath;
			SelectedChapterNumber = chapterNumber;
			RawsFiles = rawsFiles;
			TranslatorsName = translatorsName;
			IsCreatingScript = isCreatingScript;
			PreviousPageIndex = 0;
			CurrentPageIndex = 0;

			ScriptViewerRichTextBox.ReadOnly = isCreatingScript;

			PageInformations = new List<PageInformation>();

			LoadImage();
			InitializePageInformations();
		}

		private void InitializePageInformations()
		{
			for ( int pageIndex = 0; pageIndex < RawsFiles.Count(); ++pageIndex )
			{
				PageInformations.Add(new PageInformation
				{
					isSpread = RawsFiles.ElementAt(pageIndex).Name.Contains('-'),
					pageNumber = pageIndex + 1,
					totalPanels = 0,
					filename = RawsFiles.ElementAt(pageIndex).Name,
					panelsWithSFX = new List<bool>(),
					pageScriptContents = new List<string>()
				});
			}

			if ( IsCreatingScript )
			{
				ResetPageScriptContent();
			}
			else
			{
				ParseScriptForPageInformations();
				TotalPanelsTextBox.Enabled = false;
				PanelsWithSFXGroupBox.Enabled = false;
				IsPageASpreadCheckBox.Enabled = false;
				LoadPageInformation();
			}
		}

		private void ParseScriptForPageInformations()
		{
			string[] fileContents = File.ReadAllLines(ScriptLocationFullPath);

			int currentPageIndex = -1;
			int currentPanelIndex = -1;
			List<string> pageContents = new List<string>();

			foreach ( string line in fileContents )
			{
				pageContents.Add(line + "\n");

				if ( line.Contains(PAGE_HEADER_BEGIN) )
				{
					++currentPageIndex;
				}
				else if ( line.Contains("Panel") )
				{
					++currentPanelIndex;
					++PageInformations.ElementAt(currentPageIndex).totalPanels;
					PageInformations.ElementAt(currentPageIndex).panelsWithSFX.Add(false);
				}
				else if ( line.Contains("SFX") )
				{
					PageInformations.ElementAt(currentPageIndex).panelsWithSFX[currentPanelIndex] = true;
				}
				else if ( line.Contains("----------##########----------}") )
				{
					PageInformations.ElementAt(currentPageIndex).pageScriptContents = new List<string>(pageContents);
					pageContents.Clear();
					currentPanelIndex = -1;
				}
			}
		}

		private void RawsPictureBox_MouseWheel(object sender, MouseEventArgs e)
		{
			// TODO: Handle zooming image
		}

		private void PreviousImageButton_MouseClick(object sender, MouseEventArgs e)
		{
			SwitchToPreviousPage();
		}

		private void NextImageButton_MouseClick(object sender, MouseEventArgs e)
		{
			SwitchToNextPage();
		}

		private void SwitchToPreviousPage()
		{
			if ( CurrentPageIndex == 0 )
			{
				return;
			}

			PreviousPageIndex = CurrentPageIndex;
			--CurrentPageIndex;

			if ( CurrentPageIndex == 0 )
			{
				PreviousImageButton.Enabled = false;
			}

			ChangePage();
		}

		private void SwitchToNextPage()
		{
			PreviousPageIndex = CurrentPageIndex;
			++CurrentPageIndex;

			if ( CurrentPageIndex >= RawsFiles.Count() )
			{
				if ( IsCreatingScript )
				{
					FinishScriptCreation();
				}
				else
				{
					FinishScriptEditing();
				}

				return;
			}
			else if ( CurrentPageIndex >= RawsFiles.Count() - 1 )
			{
				NextImageButton.Text = "Finish";
			}

			PreviousImageButton.Enabled = true;
			ChangePage();
		}

		private void ChangePage()
		{
			SavePageInformation();
			LoadImage();
			LoadPageInformation();
		}

		private void SavePageInformation()
		{
			PageInformation pageInfo = PageInformations.ElementAt(PreviousPageIndex);

			pageInfo.pageScriptContents = new List<string>(TextUtils.ParseScriptPageContents(ScriptViewerRichTextBox.Text));

			ScriptViewerRichTextBox.Text = "";

			bool didSucceed = int.TryParse(TotalPanelsTextBox.Text, out int totalPanelsInput);

			if ( !didSucceed )
			{
				IsPageASpreadCheckBox.Checked = false;
				return;
			}

			pageInfo.totalPanels = totalPanelsInput;

			foreach ( Control control in PanelsWithSFXGroupBox.Controls )
			{
				if ( control.GetType() == typeof(CheckBox) )
				{
					CheckBox checkBox = (CheckBox)control;

					pageInfo.panelsWithSFX.Add(checkBox.Checked);
				}
			}

			pageInfo.isSpread = IsPageASpreadCheckBox.Checked;
		}

		private void LoadImage()
		{
			RawsViewerGroupBox.Text = "Raws Viewer - Page: " + (CurrentPageIndex + 1).ToString() + " / " + RawsFiles.Count().ToString();
			RawsPictureBox.Image = Image.FromFile(RawsFiles.ElementAt(CurrentPageIndex).FullName);

			this.Size = RawsPictureBox.Image.Size;

			TotalPanelsTextBox.Text = "";
		}

		private void LoadPageInformation()
		{
			PageInformation pageInfo = PageInformations.ElementAt(CurrentPageIndex);

			IsPageASpreadCheckBox.Checked = pageInfo.isSpread;

			if ( !IsCreatingScript )
			{
				UpdateScriptViewerContents(pageInfo.pageScriptContents);
			}

			if ( pageInfo.panelsWithSFX.Count == 0 )
			{
				if ( IsCreatingScript )
				{
					ResetPageScriptContent(); // If switching from a blank page to a blank page 
				}

				return; // Page has yet to be processed for the first time
			}

			TotalPanelsTextBox.Text = pageInfo.totalPanels.ToString();

			int currentPanelIndex = 0;

			foreach ( Control control in PanelsWithSFXGroupBox.Controls )
			{
				if ( control.GetType() == typeof(CheckBox) )
				{
					CheckBox checkBox = (CheckBox)control;

					checkBox.Checked = pageInfo.panelsWithSFX.ElementAt(currentPanelIndex);

					++currentPanelIndex;

					if ( currentPanelIndex >= pageInfo.panelsWithSFX.Count )
					{
						break;
					}
				}
			}
		}

		private void FinishScriptCreation()
		{
			SavePageInformation();

			if ( !AreAllPagesFilledIn() )
			{
				--CurrentPageIndex;
				return;
			}

			string fileContents = string.Empty;

			int pageNumberOffset = 0;

			foreach ( PageInformation pageInfo in PageInformations )
			{
				int currentPageNumber = pageInfo.pageNumber + pageNumberOffset;
				fileContents += PAGE_HEADER_BEGIN + currentPageNumber.ToString() + (pageInfo.isSpread ? " - " + (currentPageNumber + 1).ToString() : "") + PAGE_HEADER_END;

				if ( pageInfo.isSpread )
				{
					++pageNumberOffset;
				}

				for ( int panelIndex = 0; panelIndex < pageInfo.totalPanels; ++panelIndex )
				{
					fileContents += PANEL_HEADER_BEGIN + (panelIndex + 1).ToString() + PANEL_HEADER_END;

					if ( pageInfo.panelsWithSFX.ElementAt(panelIndex) )
					{
						fileContents += PANEL_SFX_SECTION;
					}

					fileContents += PANEL_FOOTER;
				}

				fileContents += PAGE_FOOTER;
			}

			OutputScript(fileContents);
		}

		private void FinishScriptEditing()
		{
			SavePageInformation();

			string fileContents = string.Empty;

			foreach ( PageInformation pageInfo in PageInformations )
			{
				fileContents += string.Join("", pageInfo.pageScriptContents);
			}

			OutputScript(fileContents);
		}

		private void OutputScript(string fileContents)
		{
			string outputDirectoryPath = RawsLocationFullPath.Substring(0, RawsLocationFullPath.LastIndexOf("\\") + 1);
			string outputFilepath = IsCreatingScript ? outputDirectoryPath + "Ch " + SelectedChapterNumber + " - TL " + TranslatorsName + ".txt" : ScriptLocationFullPath;

			File.WriteAllLines(outputFilepath, fileContents.Split('\n'), System.Text.Encoding.UTF8);

			MessageBox.Show(this, "File output to:\n" + outputFilepath, "Script Successfully generated", MessageBoxButtons.OK,
				MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

			this.Close();
		}

		private bool AreAllPagesFilledIn()
		{
			foreach ( PageInformation pageInfo in PageInformations )
			{
				if ( pageInfo.totalPanels == 0 )
				{
					MessageBox.Show(this, "Page " + (pageInfo.pageNumber).ToString() + " does not have a value for total panels. All pages must have at least 1 panel to continue.",
						"Error generating script.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					return false;
				}
			}

			return true;
		}

		private void TotalPanelsTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( !char.IsControl(e.KeyChar) && !char.IsNumber(e.KeyChar) )
			{
				e.Handled = true;
			}
		}

		private void TotalPanelsTextBox_TextChanged(object sender, EventArgs e)
		{
			ResetPageScriptContent();

			if ( string.IsNullOrWhiteSpace(TotalPanelsTextBox.Text) )
			{
				ResetSFXCheckBoxes();
				return;
			}

			int currentIndex = 0;
			int totalPanels = int.Parse(TotalPanelsTextBox.Text);

			if ( totalPanels > MAX_PANELS )
			{
				totalPanels = MAX_PANELS;
				TotalPanelsTextBox.Text = MAX_PANELS.ToString();
			}

			foreach ( Control control in PanelsWithSFXGroupBox.Controls )
			{
				if ( control.GetType() == typeof(CheckBox) )
				{
					control.Visible = currentIndex < totalPanels;

					++currentIndex;
				}
			}

			AddPanelsToScriptPageContent(totalPanels);
		}

		private void ResetPageScriptContent()
		{
			PageInformation pageInfo = PageInformations.ElementAt(CurrentPageIndex);

			int pageNumberOffset = 0;

			for ( int pageInfoIndex = 0; pageInfoIndex < CurrentPageIndex; ++pageInfoIndex )
			{
				if ( PageInformations.ElementAt(pageInfoIndex).isSpread )
				{
					++pageNumberOffset;
				}
			}

			int currentPageNumber = pageInfo.pageNumber + pageNumberOffset;
			ScriptViewerRichTextBox.Text = PAGE_HEADER_BEGIN + currentPageNumber.ToString() + (pageInfo.isSpread ? " - " + (currentPageNumber + 1).ToString() : "") + PAGE_HEADER_END + PAGE_FOOTER;
		}

		private void ResetSFXCheckBoxes()
		{
			foreach ( Control control in PanelsWithSFXGroupBox.Controls )
			{
				if ( control.GetType() == typeof(CheckBox) )
				{
					control.Visible = false;
					CheckBox checkBox = (CheckBox)control;
					checkBox.Checked = false;
				}
			}
		}

		private void AddPanelsToScriptPageContent(int totalPanels)
		{
			List<string> pageContents = TextUtils.ParseScriptPageContents(ScriptViewerRichTextBox.Text);

			int index = 0;

			foreach ( string line in pageContents )
			{
				if ( line.Contains("Page") )
				{
					++index; // Offset by one more due to the newline in the middle
					break;
				}

				++index;
			}

			for ( int panelIndex = 0; panelIndex < totalPanels; ++panelIndex )
			{
				pageContents.Insert(index, PANEL_HEADER_BEGIN + (panelIndex + 1).ToString() + PANEL_HEADER_END + PANEL_FOOTER);
				++index;
			}

			UpdateScriptViewerContents(pageContents);
		}

		private void TotalPanelsTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if ( e.KeyCode == Keys.Left )
			{
				e.IsInputKey = true;
			}
			else if ( e.KeyCode == Keys.Right )
			{
				e.IsInputKey = true;
			}
		}

		private void TotalPanelsTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Left )
			{
				SwitchToPreviousPage();
			}
			else if ( e.KeyCode == Keys.Right )
			{
				SwitchToNextPage();
			}
		}

		private void UpdateScriptViewerContents(List<string> newContents)
		{
			ScriptViewerRichTextBox.Text = string.Join("", newContents);
		}

		private void PanelCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( !((CheckBox)sender).Visible )
			{
				return; // Switching pages
			}

			ResetPageScriptContent();
			AddPanelsToScriptPageContent(int.Parse(TotalPanelsTextBox.Text));

			List<string> pageContents = TextUtils.ParseScriptPageContents(ScriptViewerRichTextBox.Text);

			foreach ( Control control in PanelsWithSFXGroupBox.Controls )
			{
				if ( control.GetType() != typeof(CheckBox) )
				{
					continue;
				}

				CheckBox checkBox = (CheckBox)control;

				if ( !checkBox.Checked )
				{
					continue;
				}

				int index = 0;
				string panelToFind = "Panel " + int.Parse(checkBox.Text);

				foreach ( string line in pageContents )
				{
					if ( line.Contains(panelToFind) )
					{
						++index; // Offset by one more due to the newline in the middle

						pageContents.Insert(index, PANEL_SFX_SECTION);
						break;
					}

					++index;
				}
			}

			UpdateScriptViewerContents(pageContents);
		}

		private void ScriptViewerRichTextBox_MouseClick(object sender, MouseEventArgs e)
		{
			if ( IsCreatingScript )
			{
				return;
			}

			ValidateSelectionIsNotSyntax();
		}

		private void ScriptViewerRichTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Up || e.KeyCode == Keys.Down )
			{
				ValidateSelectionIsNotSyntax();
			}
		}

		private void ValidateSelectionIsNotSyntax()
		{
			int totalLines = ScriptViewerRichTextBox.Lines.Count();
			int currentLineIndex = ScriptViewerRichTextBox.GetLineFromCharIndex(ScriptViewerRichTextBox.GetFirstCharIndexOfCurrentLine());

			string currentLine = ScriptViewerRichTextBox.Lines[currentLineIndex];

			bool isCurrentLineUneditable = false;

			if ( currentLine == "" && currentLineIndex != 0 && currentLineIndex != totalLines - 1 && currentLineIndex != totalLines - 2 )
			{
				string previousLine = ScriptViewerRichTextBox.Lines[currentLineIndex - 1];
				string nextLine = ScriptViewerRichTextBox.Lines[currentLineIndex + 1];

				if ( previousLine.Contains(PAGE_HEADER_BEGIN) && nextLine.Contains("---# Panel ") )
				{
					isCurrentLineUneditable = true;
				}

				if ( previousLine.Contains("---#####---}") && nextLine.Contains("---# Panel ") )
				{
					isCurrentLineUneditable = true;
				}

				if ( previousLine.Contains("---#####---}") && nextLine.Contains("----------##########----------}") )
				{
					isCurrentLineUneditable = true;
				}
			}
			else if ( currentLine == "" && (currentLineIndex == totalLines - 1 || currentLineIndex == totalLines - 2) )
			{
				isCurrentLineUneditable = true;
			}
			else
			{
				isCurrentLineUneditable = DoesLineContainSyntaxMarkers(currentLine);
			}

			ScriptViewerRichTextBox.ReadOnly = isCurrentLineUneditable;
		}

		private bool DoesLineContainSyntaxMarkers(string line)
		{
			foreach ( string syntaxMarker in PAGE_SYNTAX_MARKERS )
			{
				if ( line.Contains(syntaxMarker) )
				{
					return true;
				}
			}

			return false;
		}
	}

	public static class TextUtils
	{

		public static IEnumerable<string> SplitToLines(string input)
		{
			if ( input == null )
			{
				yield break;
			}

			using ( StringReader reader = new StringReader(input) )
			{
				string line;

				while ( (line = reader.ReadLine()) != null )
				{
					yield return line + "\n";
				}
			}
		}

		public static List<string> ParseScriptPageContents(string input)
		{
			List<string> pageContents = new List<string>();

			foreach ( string line in TextUtils.SplitToLines(input) )
			{
				pageContents.Add(line);
			}

			return pageContents;
		}
	}
}
