using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Drawing;
using ScintillaNET;

namespace TranslationScriptMaker
{

	public partial class RawsViewerForm : Form
	{
		private const int MAX_PANELS = 12;
		private const string PAGE_HEADER_BEGIN = "----------# Page ";
		private const string PAGE_HEADER_END = " #----------{\n";
		private const string PANEL_HEADER_BEGIN = "\n---# Panel ";
		private const string PANEL_HEADER_END = " #---{\n";
		private const string PANEL_SFX_SECTION = "[SFX]{\n\n}\n";
		private const string PANEL_FOOTER = "\n---#####---}\n";
		private const string PAGE_FOOTER = "\n----------##########----------}\n\n";

		private static readonly Color COLOR_BACKGROUND = Color.FromArgb(30, 30, 30);
		private static readonly Color COLOR_FOREGROUND = Color.FromArgb(216, 216, 216);
		private static readonly Color COLOR_SELECTION = Color.FromArgb(38, 79, 120);
		private static readonly Color COLOR_CURRENT_LINE_BACKGROUND = Color.FromArgb(15, 15, 15);
		private static readonly Color COLOR_CURRENT_LINE_FOREGROUND = Color.FromArgb(220, 220, 220);
		private static readonly Color COLOR_FORMATTING_BLOCK = Color.FromArgb(0, 128, 192);
		private static readonly Color COLOR_HEADER_TITLE = Color.FromArgb(0, 128, 255);
		private static readonly Color COLOR_HEADER_VALUE = Color.FromArgb(255, 128, 0);
		private static readonly Color COLOR_BRACES = Color.FromArgb(128, 128, 255);
		private static readonly Color COLOR_IMPORTANT = Color.FromArgb(220, 20, 20);
		private static readonly Color COLOR_NOTE = Color.FromArgb(255, 128, 128);
		private static readonly Color COLOR_TL_NOTE = Color.FromArgb(0, 128, 0);
		private static readonly Color COLOR_SUBSECTION = Color.FromArgb(0, 128, 0);

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

		private IEnumerable<FileInfo> RawsFiles { get; }
		private string OutputLocationFullPath { get; }
		private bool IsCreatingScript { get; }
		private int CurrentPageIndex { get; set; }
		private int PreviousPageIndex { get; set; }
		private List<PageInformation> PageInformations { get; set; }

		private bool IsChangingPage { get; set; } // TEMP, WILL REMOVE AFTER REFACTORING HOW SFX AND SUCH ARE ADDED
		private bool HasUserPromptedZoomChanged { get; set; }
		private bool HasUserScrolledIn { get; set; }
		private bool _AreThereUnsavedChanges;
		private bool AreThereUnsavedChanges
		{
			get => _AreThereUnsavedChanges;
			set
			{
				if ( value == _AreThereUnsavedChanges )
				{
					return; // No need to modify the Title bar further
				}

				if ( value )
				{
					this.Text = "Translation Script Maker* - v" + typeof(MainForm).Assembly.GetName().Version;
				}
				else
				{
					this.Text = "Translation Script Maker - v" + typeof(MainForm).Assembly.GetName().Version;
				}

				_AreThereUnsavedChanges = value;
			}
		}

		public RawsViewerForm(IEnumerable<FileInfo> rawsFiles, string outputLocationFullPath)
		{
			InitializeComponent();

			this.WindowState = FormWindowState.Maximized;
			this.Text = "Translation Script Maker v" + typeof(MainForm).Assembly.GetName().Version;

			OutputLocationFullPath = outputLocationFullPath;
			RawsFiles = rawsFiles;
			IsCreatingScript = !File.Exists(outputLocationFullPath);
			PreviousPageIndex = 0;
			CurrentPageIndex = 0;
			AreThereUnsavedChanges = false;

			PageInformations = new List<PageInformation>();

			LoadImage();
			InitializePageInformations();

			this.StartPosition = FormStartPosition.Manual;
		}

		private void RawsViewerForm_Load(object sender, EventArgs e)
		{
			InitializeScintillaStyles();
			IsChangingPage = true;
			DisplaySFXGroupBoxes(PageInformations.ElementAt(CurrentPageIndex).totalPanels);
			LoadPageInformation();
			IsChangingPage = false;
		}

		private void InitializeScintillaStyles()
		{
			// Context Menu
			STTB.ContextMenuStrip = ScriptEditorContextMenuStrip;
			ScriptEditorContextMenuStrip.Renderer = new DarkContextMenuRenderer();

			// Default Style
			STTB.Styles[Style.Default].BackColor = COLOR_BACKGROUND;
			STTB.Styles[Style.Default].ForeColor = COLOR_FOREGROUND;
			STTB.Styles[Style.Default].Font = "Yu Gothic";
			STTB.Styles[Style.Default].SizeF = 9.75F;
			STTB.Zoom = 4;
			STTB.StyleClearAll();

			// Selection
			STTB.SetSelectionBackColor(true, COLOR_SELECTION);

			// Margins Style
			STTB.Styles[Style.LineNumber].BackColor = COLOR_BACKGROUND;
			STTB.Styles[Style.LineNumber].ForeColor = Color.DeepSkyBlue;
			STTB.Margins[0].Width = 48;

			STTB.Margins[1].Type = MarginType.Symbol;
			STTB.Margins[1].Mask = Marker.MaskFolders;
			STTB.Margins[1].BackColor = COLOR_BACKGROUND;
			STTB.Margins[1].Sensitive = true;
			STTB.Margins[1].Width = 16;
			STTB.SetFoldMarginColor(true, COLOR_BACKGROUND);
			STTB.SetFoldMarginHighlightColor(true, COLOR_BACKGROUND);

			// Set colors for all folding markers
			for ( int folderIndex = Marker.FolderEnd; folderIndex <= Marker.FolderOpen; ++folderIndex )
			{
				STTB.Markers[folderIndex].SetForeColor(SystemColors.Control);
				STTB.Markers[folderIndex].SetBackColor(SystemColors.ControlDark);
			}

			// Configure folding markers with respective symbols
			STTB.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
			STTB.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
			STTB.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
			STTB.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
			STTB.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
			STTB.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
			STTB.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;

			//// TEST
			//STTB.Lines[0].FoldLevelFlags = FoldLevelFlags.Header;
			//var foldLevel = STTB.Lines[0].FoldLevel;
			//STTB.Lines[1].FoldLevel = ++foldLevel;
			//STTB.Lines[2].FoldLevel = foldLevel;
			//STTB.Lines[3].FoldLevel = --foldLevel;

			//STTB.MarginClick += (s, mcea) =>
			//{
			//	// Toggle the fold when clicking
			//	var line = STTB.LineFromPosition(mcea.Position);
			//	STTB.Lines[line].ToggleFold();
			//};

			// Custom Styles
			STTB.Styles[TLSLexer.STYLE_FORMATTING_BLOCK].ForeColor = COLOR_FORMATTING_BLOCK;
			STTB.Styles[TLSLexer.STYLE_FORMATTING_BLOCK].Font = "Consolas";

			STTB.Styles[TLSLexer.STYLE_HEADER_TITLE].ForeColor = COLOR_HEADER_TITLE;
			STTB.Styles[TLSLexer.STYLE_HEADER_TITLE].Font = "Consolas";
			STTB.Styles[TLSLexer.STYLE_HEADER_TITLE].Bold = true;

			STTB.Styles[TLSLexer.STYLE_HEADER_VALUE].ForeColor = COLOR_HEADER_VALUE;
			STTB.Styles[TLSLexer.STYLE_HEADER_VALUE].Font = "Consolas";
			STTB.Styles[TLSLexer.STYLE_HEADER_VALUE].Bold = true;

			STTB.Styles[TLSLexer.STYLE_BRACES].ForeColor = COLOR_BRACES;
			STTB.Styles[TLSLexer.STYLE_BRACES].Font = "Consolas";
			STTB.Styles[TLSLexer.STYLE_BRACES].Bold = true;

			STTB.Styles[TLSLexer.STYLE_IMPORTANT].ForeColor = COLOR_IMPORTANT;
			STTB.Styles[TLSLexer.STYLE_IMPORTANT].Bold = true;

			STTB.Styles[TLSLexer.STYLE_NOTE].ForeColor = COLOR_NOTE;
			STTB.Styles[TLSLexer.STYLE_NOTE].Italic = true;

			STTB.Styles[TLSLexer.STYLE_TL_NOTE].ForeColor = COLOR_TL_NOTE;
			STTB.Styles[TLSLexer.STYLE_TL_NOTE].Font = "Consolas";
			STTB.Styles[TLSLexer.STYLE_TL_NOTE].Bold = true;

			STTB.Styles[TLSLexer.STYLE_SUBSECTION].ForeColor = COLOR_SUBSECTION;
			STTB.Styles[TLSLexer.STYLE_SUBSECTION].Font = "Consolas";
			STTB.Styles[TLSLexer.STYLE_SUBSECTION].Bold = true;
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

				CurrentPageComboBox.Items.Add(PageInformations.Last().filename + (IsCreatingScript ? "*" : ""));
			}

			CurrentPageComboBox.SelectedIndex = 0;

			if ( IsCreatingScript )
			{
				InitializePageScriptContents();
			}
			else
			{
				ParseScriptForPageInformations();
			}
		}

		private void InitializePageScriptContents()
		{
			int pageNumberOffset = 0;

			foreach ( PageInformation pageInfo in PageInformations )
			{
				pageInfo.totalPanels = 1;
				pageInfo.panelsWithSFX.Add(false);

				int currentPageNumber = pageInfo.pageNumber + pageNumberOffset;

				if ( pageInfo.isSpread )
				{
					++pageNumberOffset;
				}

				pageInfo.pageScriptContents = new List<string>(TextUtils.ParseScriptPageContents(
					PAGE_HEADER_BEGIN + currentPageNumber.ToString() + (pageInfo.isSpread ? " - " + (currentPageNumber + 1).ToString() : "") + PAGE_HEADER_END
					+ PANEL_HEADER_BEGIN + "1" + PANEL_HEADER_END + PANEL_FOOTER
					+ PAGE_FOOTER));
			}
		}

		private void ParseScriptForPageInformations()
		{
			string[] fileContents = File.ReadAllLines(OutputLocationFullPath);

			int currentPageIndex = -1;
			int currentPanelIndex = -1;
			List<string> pageContents = new List<string>();

			foreach ( string line in fileContents )
			{
				pageContents.Add(line + "\n");

				if ( line.Contains(PAGE_HEADER_BEGIN) )
				{
					++currentPageIndex;

					if ( line.Contains(" - ") )
					{
						PageInformations.ElementAt(currentPageIndex).isSpread = true;
					}
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

		private void PreviousImageButton_MouseClick(object sender, MouseEventArgs e)
		{
			SwitchToPreviousPage();
		}

		private void NextImageButton_MouseClick(object sender, MouseEventArgs e)
		{
			SwitchToNextPage();
		}

		private void CurrentPageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			IsChangingPage = true;
			string fileNeedsEditingMarker = string.Empty;

			if ( string.IsNullOrWhiteSpace(TotalPanelsTextBox.Text) || int.Parse(TotalPanelsTextBox.Text) == 0 )
			{
				fileNeedsEditingMarker = "*";
			}

			CurrentPageComboBox.Items[CurrentPageIndex] = PageInformations.ElementAt(CurrentPageIndex).filename + fileNeedsEditingMarker;

			PreviousPageIndex = CurrentPageIndex;
			SavePageInformation();
			CurrentPageIndex = CurrentPageComboBox.SelectedIndex;

			PreviousImageButton.Enabled = CurrentPageIndex != 0;

			if ( CurrentPageIndex >= RawsFiles.Count() - 1 )
			{
				NextImageButton.Text = "Finish";
			}
			else if ( NextImageButton.Text == "Finish" )
			{
				NextImageButton.Text = "Next";
			}

			LoadImage();
			LoadPageInformation();
			TotalPanelsTextBox.Select();

			SaveCurrentScript(false);
			IsChangingPage = false;
		}

		private void SwitchToPreviousPage()
		{
			if ( CurrentPageIndex == 0 )
			{
				return;
			}

			if ( IsCreatingScript )
			{
				string fileNeedsEditingMarker = string.Empty;

				if ( string.IsNullOrWhiteSpace(TotalPanelsTextBox.Text) || int.Parse(TotalPanelsTextBox.Text) == 0 )
				{
					fileNeedsEditingMarker = "*";
				}

				CurrentPageComboBox.Items[CurrentPageIndex] = PageInformations.ElementAt(CurrentPageIndex).filename + fileNeedsEditingMarker;
			}

			PreviousPageIndex = CurrentPageIndex;
			--CurrentPageIndex;

			if ( CurrentPageIndex == 0 )
			{
				PreviousImageButton.Enabled = false;
			}

			if ( NextImageButton.Text == "Finish" )
			{
				NextImageButton.Text = "Next";
			}

			ChangePage();
		}

		private void SwitchToNextPage()
		{
			if ( IsCreatingScript )
			{
				string fileNeedsEditingMarker = string.Empty;

				if ( string.IsNullOrWhiteSpace(TotalPanelsTextBox.Text) || int.Parse(TotalPanelsTextBox.Text) == 0 )
				{
					fileNeedsEditingMarker = "*";
				}

				CurrentPageComboBox.Items[CurrentPageIndex] = PageInformations.ElementAt(CurrentPageIndex).filename + fileNeedsEditingMarker;
			}

			PreviousPageIndex = CurrentPageIndex;
			++CurrentPageIndex;

			if ( CurrentPageIndex >= RawsFiles.Count() )
			{
				SaveCurrentScript(true);
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
			IsChangingPage = true;

			if ( STTB.ReadOnly )
			{
				STTB.ReadOnly = false;
			}

			SavePageInformation();
			LoadImage();
			LoadPageInformation();
			TotalPanelsTextBox.Select();
			CurrentPageComboBox.SelectedIndex = CurrentPageIndex;

			SaveCurrentScript(false);
			IsChangingPage = false;
		}

		private void SavePageInformation()
		{
			PageInformation pageInfo = PageInformations.ElementAt(PreviousPageIndex);

			pageInfo.pageScriptContents = new List<string>(TextUtils.ParseScriptPageContents(STTB.Text));
			
			STTB.Clear();

			bool didSucceed = int.TryParse(TotalPanelsTextBox.Text, out int totalPanelsInput);

			if ( !didSucceed )
			{
				IsPageASpreadCheckBox.Checked = false;
				return;
			}

			pageInfo.totalPanels = totalPanelsInput;
			pageInfo.panelsWithSFX.Clear();

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
			RawsImageBox.BeginUpdate();
			RawsViewerGroupBox.Text = "Raws Viewer - Page: " + (CurrentPageIndex + 1).ToString() + " / " + RawsFiles.Count().ToString();
			ScriptEditorGroupBox.Text = "Script Editor - Page : " + (CurrentPageIndex + 1).ToString() + " / " + RawsFiles.Count().ToString();
			RawsImageBox.Image = Image.FromFile(RawsFiles.ElementAt(CurrentPageIndex).FullName);
			RawsImageBox.ZoomToFit();
			RawsImageBox.EndUpdate();

			ResizeWindowToImage();
			TotalPanelsTextBox.Text = "";
		}

		private void ResizeWindowToImage()
		{
			Size currentScreenSize = Screen.FromControl(this).WorkingArea.Size;
			this.Width = RawsImageBox.Image.Width < currentScreenSize.Width ? RawsImageBox.Image.Width : currentScreenSize.Width;
			this.Height = RawsImageBox.Image.Height < currentScreenSize.Height ? RawsImageBox.Image.Height : currentScreenSize.Height;
			this.Left = 0;
			this.Top = 0;

			HasUserPromptedZoomChanged = true;
			SetSizeMode();
		}

		private void LoadPageInformation()
		{
			PageInformation pageInfo = PageInformations.ElementAt(CurrentPageIndex);

			IsPageASpreadCheckBox.Checked = pageInfo.isSpread;

			UpdateScriptEditorContents(pageInfo.pageScriptContents);

			TotalPanelsTextBox.Text = pageInfo.totalPanels.ToString();

			if ( pageInfo.panelsWithSFX.Count == 0 )
			{
				return;
			}

			DisplaySFXGroupBoxes(pageInfo.totalPanels);

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

		private void SaveCurrentScript(bool shouldClose)
		{
			SaveCurrentProgress(shouldClose);

			string fileContents = string.Empty;

			foreach ( PageInformation pageInfo in PageInformations )
			{
				fileContents += string.Join("", pageInfo.pageScriptContents);
			}

			OutputScript(fileContents, shouldClose);

			AreThereUnsavedChanges = false;
		}

		private void SaveCurrentProgress(bool shouldClose)
		{
			if ( shouldClose )
			{
				--CurrentPageIndex;
			}

			PageInformation pageInfo = PageInformations.ElementAt(CurrentPageIndex);

			pageInfo.pageScriptContents = new List<string>(TextUtils.ParseScriptPageContents(STTB.Text));

			if ( int.TryParse(TotalPanelsTextBox.Text, out int totalPanels) )
			{
				pageInfo.totalPanels = totalPanels;
			}
			else
			{
				pageInfo.totalPanels = 0;
			}

			pageInfo.panelsWithSFX.Clear();

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

		private void OutputScript(string fileContents, bool shouldClose)
		{
			string outputFilepath = OutputLocationFullPath;

			File.WriteAllLines(outputFilepath, fileContents.Split('\n'), System.Text.Encoding.UTF8);

			if ( shouldClose )
			{
				MessageBox.Show(this, "File output to:\n" + outputFilepath, "Script Successfully generated", MessageBoxButtons.OK,
					MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

				this.Close();
			}
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
			if ( string.IsNullOrWhiteSpace(TotalPanelsTextBox.Text) )
			{
				ResetSFXCheckBoxes();
				return;
			}

			int totalPanels = int.Parse(TotalPanelsTextBox.Text);

			if ( totalPanels == PageInformations.ElementAt(CurrentPageIndex).totalPanels )
			{
				return;
			}

			if ( totalPanels > MAX_PANELS )
			{
				totalPanels = MAX_PANELS;
				TotalPanelsTextBox.Text = MAX_PANELS.ToString();
			}

			PageInformations.ElementAt(CurrentPageIndex).totalPanels = totalPanels;

			DisplaySFXGroupBoxes(totalPanels);
			AddPanelsToScriptPageContent(totalPanels);
		}

		private void DisplaySFXGroupBoxes(int totalPanels)
		{
			int currentIndex = 0;

			foreach ( Control control in PanelsWithSFXGroupBox.Controls )
			{
				if ( control.GetType() == typeof(CheckBox) )
				{
					control.Visible = currentIndex < totalPanels;
					++currentIndex;
				}
			}
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
			List<string> pageContents = TextUtils.ParseScriptPageContents(STTB.Text);

			int index = 0;
			int totalExistingPanels = 0;

			foreach ( string line in pageContents )
			{
				if ( line.Contains("Panel") )
				{
					++totalExistingPanels;
				}

				if ( line.Contains("----------##########----------}\n") )
				{
					--index; // Offset by one before
					break;
				}

				++index;
			}

			if ( totalExistingPanels < totalPanels )
			{
				for ( int panelIndex = totalExistingPanels; panelIndex < totalPanels; ++panelIndex )
				{
					pageContents.Insert(index, PANEL_HEADER_BEGIN + (panelIndex + 1).ToString() + PANEL_HEADER_END + PANEL_FOOTER);
					++index;
				}
			}
			else
			{
				int linesBetweenPanels = 0;
				List<Tuple<int, int>> indicesToRemove = new List<Tuple<int, int>>();

				for ( int lineIndex = pageContents.Count() - 1; lineIndex > 0; --lineIndex )
				{
					if ( indicesToRemove.Count() == totalExistingPanels - totalPanels )
					{
						break;
					}

					string currentLine = pageContents.ElementAt(lineIndex);

					if ( currentLine.Contains("---#####---}") )
					{
						linesBetweenPanels = 3;
						continue;
					}

					if ( currentLine.Contains("Panel") )
					{
						indicesToRemove.Add(new Tuple<int, int>(lineIndex, linesBetweenPanels));
						continue;
					}

					++linesBetweenPanels;
				}

				foreach ( Tuple<int, int> indexAndCount in indicesToRemove )
				{
					pageContents.RemoveRange(indexAndCount.Item1, indexAndCount.Item2);
				}
			}

			UpdateScriptEditorContents(pageContents);
		}

		private void UpdateScriptEditorContents(List<string> newContents)
		{
			STTB.Text = string.Join("", newContents);
		}

		private void PanelCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;

			if ( !checkBox.Visible || IsChangingPage )
			{
				return; // Switching pages
			}

			List<string> pageContents = TextUtils.ParseScriptPageContents(STTB.Text);

			int indexOfPanelToFind = -1;
			int indexOfPanelToFindFooter = 0;
			int currentCountingIndex = 0;
			string panelToFind = "Panel " + int.Parse(checkBox.Text);

			foreach ( string line in pageContents )
			{
				if ( line.Contains(panelToFind) )
				{
					++currentCountingIndex; // Offset by one more due to the newline in the middle
					indexOfPanelToFind = currentCountingIndex;
					continue;
				}

				if ( indexOfPanelToFind != -1 && line.Contains("---#####---}") )
				{
					indexOfPanelToFindFooter = currentCountingIndex;
					break;
				}

				++currentCountingIndex;
			}

			if ( checkBox.Checked )
			{
				pageContents.Insert(indexOfPanelToFindFooter, PANEL_SFX_SECTION);
			}
			else
			{
				int indexToRemove = -1;
				int linesBetweenSFX = 0;

				for ( int lineIndex = indexOfPanelToFind; lineIndex < indexOfPanelToFindFooter; ++lineIndex )
				{
					string currentLine = pageContents.ElementAt(lineIndex);

					if ( currentLine.Contains("SFX") )
					{
						indexToRemove = lineIndex;
						linesBetweenSFX = 1;
						continue;
					}

					if ( currentLine.Equals("}\n") )
					{
						++linesBetweenSFX;
						break;
					}

					++linesBetweenSFX;
				}

				pageContents.RemoveRange(indexToRemove, linesBetweenSFX);
			}

			UpdateScriptEditorContents(pageContents);
		}

		private void ValidateSelectionIsNotSyntax()
		{
			int totalLines = STTB.Lines.Count();
			int currentLineIndex = STTB.CurrentLine;

			string currentLine = STTB.Lines[currentLineIndex].Text;

			bool isCurrentLineUneditable = false;

			if ( currentLineIndex == 0 || currentLineIndex >= totalLines - 3 )
			{
				isCurrentLineUneditable = true;
			}
			else if ( currentLine == "" && currentLineIndex != 0 && currentLineIndex != totalLines - 1 && currentLineIndex != totalLines - 2 )
			{
				string previousLine = STTB.Lines[currentLineIndex - 1].Text;
				string nextLine = STTB.Lines[currentLineIndex + 1].Text;

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

			STTB.ReadOnly = isCurrentLineUneditable;
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

		private void RawsImageBox_MouseWheel(object sender, MouseEventArgs e)
		{
			// This event is raised before ImageBox handles the event, so if we set the SizeMode to Normal here, then zooming will work
			RawsImageBox.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Normal;
			HasUserPromptedZoomChanged = true;
			HasUserScrolledIn = e.Delta > 0;
		}

		private void RawsImageBox_ZoomChanged(object sender, EventArgs e)
		{
			SetSizeMode();
		}

		private void SetSizeMode()
		{
			if ( RawsImageBox.Image == null || !HasUserPromptedZoomChanged )
			{
				return;
			}

			HasUserPromptedZoomChanged = false;

			double zoomFactor = RawsImageBox.ZoomFactor;
			Size imageSize = RawsImageBox.Image.Size;
			int scaledWidth = Convert.ToInt32(imageSize.Width * zoomFactor);
			int scaledHeight = Convert.ToInt32(imageSize.Height * zoomFactor);

			Size viewSize = RawsImageBox.GetInsideViewPort().Size;

			if ( scaledWidth < viewSize.Width && scaledHeight < viewSize.Height )
			{
				RawsImageBox.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Stretch;
			}

			if ( (scaledWidth < viewSize.Width && viewSize.Height < scaledHeight) && !HasUserScrolledIn )
			{
				// If the Image Width is larger than the Image Height, don't use stretched, use Fit
				RawsImageBox.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
			}
			else
			{
				RawsImageBox.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Normal;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ( keyData == (Keys.Control | Keys.S) )
			{
				SaveCurrentScript(false);
				return true;
			}

			bool shouldChangePage = FindFocusedControl(this.ActiveControl) != STTB;

			if ( keyData == Keys.Left && shouldChangePage )
			{
				SwitchToPreviousPage();
			}
			else if ( keyData == Keys.Right && shouldChangePage )
			{
				SwitchToNextPage();
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		private static Control FindFocusedControl(Control control)
		{
			IContainerControl container = control as IContainerControl;

			while ( container != null )
			{
				control = container.ActiveControl;
				container = control as IContainerControl;
			}

			return control;
		}

		private bool DoesControlContainControl(Control parent, Control searchFor)
		{
			if ( parent == searchFor )
			{
				return true;
			}

			foreach ( Control childControl in parent.Controls )
			{
				if ( DoesControlContainControl(childControl, searchFor) )
				{
					return true;
				}
			}

			return false;
		}

		private void CurrentPageComboBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			if ( e.Index < 0 )
			{
				return;
			}

			ComboBox comboBox = sender as ComboBox;

			using ( SolidBrush brush = new SolidBrush(e.ForeColor) )
			{
				Font font = e.Font;

				if ( comboBox.Items[e.Index].ToString().Last() == '*' )
				{
					font = new Font(font, FontStyle.Bold);
				}

				e.DrawBackground();
				e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), font, brush, e.Bounds);
				e.DrawFocusRectangle();
			}
		}

		private void IsPageASpreadCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;

			if ( IsChangingPage )
			{
				return; // Switching pages
			}

			PageInformations.ElementAt(CurrentPageIndex).isSpread = checkBox.Checked;

			int pageNumberOffset = 0;

			foreach ( PageInformation pageInfo in PageInformations )
			{
				int currentPageNumber = pageInfo.pageNumber + pageNumberOffset;

				if ( pageInfo.isSpread )
				{
					++pageNumberOffset;
				}

				int indexOfPageLine = pageInfo.pageScriptContents.First().Contains("Page") ? 0 : 1;

				int indexBeforePageNum = pageInfo.pageScriptContents.ElementAt(indexOfPageLine).IndexOf("e") + 2;
				int indexAfterPageNum = pageInfo.pageScriptContents.ElementAt(indexOfPageLine).LastIndexOf(' ');

				pageInfo.pageScriptContents[indexOfPageLine] = pageInfo.pageScriptContents.ElementAt(indexOfPageLine).Remove(indexBeforePageNum, indexAfterPageNum - indexBeforePageNum);

				if ( pageInfo.isSpread )
				{
					pageInfo.pageScriptContents[indexOfPageLine] = pageInfo.pageScriptContents.ElementAt(indexOfPageLine).Insert(indexBeforePageNum, currentPageNumber.ToString() + " - " + (currentPageNumber + 1).ToString());
				}
				else
				{
					pageInfo.pageScriptContents[indexOfPageLine] = pageInfo.pageScriptContents.ElementAt(indexOfPageLine).Insert(indexBeforePageNum, currentPageNumber.ToString());
				}
			}

			UpdateScriptEditorContents(PageInformations.ElementAt(CurrentPageIndex).pageScriptContents);
		}

		private void PaintGroupBoxBorderDarkTheme(object sender, PaintEventArgs e)
		{
			if ( !(sender is GroupBox groupBox) )
			{
				return;
			}

			using ( Brush textBrush = new SolidBrush(COLOR_FOREGROUND) )
			using ( Brush borderBrush = new SolidBrush(Color.DimGray) )
			using ( Pen borderPen = new Pen(borderBrush) )
			{
				SizeF strSize = e.Graphics.MeasureString(groupBox.Text, groupBox.Font);
				Rectangle rect = new Rectangle(groupBox.ClientRectangle.X,
										   groupBox.ClientRectangle.Y + (int)(strSize.Height / 2),
										   groupBox.ClientRectangle.Width - 1,
										   groupBox.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

				e.Graphics.Clear(groupBox.BackColor);

				// Draw Label
				e.Graphics.DrawString(groupBox.Text, groupBox.Font, textBrush, groupBox.Padding.Left, 0);

				// Draw Border
				//Left
				e.Graphics.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
				//Right
				e.Graphics.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
				//Bottom
				e.Graphics.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
				//Top1
				e.Graphics.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + groupBox.Padding.Left, rect.Y));
				//Top2
				e.Graphics.DrawLine(borderPen, new Point(rect.X + groupBox.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
			}
		}

		#region ToolStripMenu Handlers
		private void CutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.Cut();
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(STTB.SelectedText);
		}

		private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if ( Clipboard.ContainsText() )
			{
				STTB.InsertText(STTB.SelectionStart, Clipboard.GetText(TextDataFormat.UnicodeText).ToString());
			}
		}

		private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.DeleteRange(STTB.SelectionStart, STTB.SelectionEnd);
		}

		private void SFXToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, PANEL_SFX_SECTION);
		}

		private void BubbleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, "[B1]\n");
		}

		private void TranslatorToReaderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, "[T/N]: ");
		}

		private void TranslatorToProofreaderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, "(T/P: )");
		}

		private void TranslatorToTypesetterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, "(T/TS: )");
		}

		private void ProofreaderToReaderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, "[PR/N]: ");
		}

		private void ProofreaderToTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, "(P/T: )");
		}

		private void ProofreaderToTypesetterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			STTB.InsertText(STTB.SelectionStart, "(P/TS: )");
		}
		#endregion

		#region Scintilla Handling
		private void STTB_StyleNeeded(object sender, StyleNeededEventArgs e)
		{
			TLSLexer.StyleText(STTB);
		}

		private void STTB_MouseClick(object sender, MouseEventArgs e)
		{
			ValidateSelectionIsNotSyntax();
		}

		private void STTB_KeyUp(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Up || e.KeyCode == Keys.Down )
			{
				ValidateSelectionIsNotSyntax();
			}
		}

		private void STTB_TextChanged(object sender, EventArgs e)
		{
			if ( CurrentPageIndex >= PageInformations.Count() )
			{
				return;
			}

			if ( sender is Scintilla scriptEditor )
			{
				if ( scriptEditor.Text == string.Join("", PageInformations.ElementAt(CurrentPageIndex).pageScriptContents) )
				{
					AreThereUnsavedChanges = false;
					return; // The text is the same as the unsaved version
				}
			}

			if ( !AreThereUnsavedChanges )
			{
				AreThereUnsavedChanges = true; // After the fact, in the very unlikely case that the sender can't be casted
			}
		}

		private void STTB_MouseUp(object sender, MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Right && string.IsNullOrEmpty(STTB.SelectedText) )
			{
				int newStartPosition = STTB.CurrentPosition;

				if ( newStartPosition < STTB.SelectionStart || newStartPosition > STTB.SelectionStart + STTB.SelectedText.Length )
				{
					STTB.SelectionStart = newStartPosition;
					STTB.SelectionEnd = STTB.SelectionStart;
				}
			}
		}
		#endregion

		private class DarkContextMenuRenderer : ToolStripProfessionalRenderer
		{
			public DarkContextMenuRenderer() : base(new DarkContextMenuColors())
			{
			}

			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
				Color color = e.Item.Selected ? COLOR_CURRENT_LINE_BACKGROUND : COLOR_BACKGROUND;

				using ( SolidBrush brush = new SolidBrush(color) )
				{
					e.Graphics.FillRectangle(brush, rect);
				}
			}
		}

		private class DarkContextMenuColors : ProfessionalColorTable
		{
			public override Color MenuBorder => COLOR_CURRENT_LINE_BACKGROUND;
			public override Color ToolStripDropDownBackground => COLOR_BACKGROUND;
		}
	}

	internal static class TextUtils
	{

		internal static IEnumerable<string> SplitToLines(string input)
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

		internal static List<string> ParseScriptPageContents(string input)
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
