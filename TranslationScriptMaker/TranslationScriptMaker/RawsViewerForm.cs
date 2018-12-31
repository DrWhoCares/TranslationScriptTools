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

		class PageInformation
		{
			public int pageNumber { get; set; }
			public int totalPanels { get; set; }
			public string filename { get; set; }
			public List<bool> panelsWithSFX { get; set; }
		}

		private string RawsLocationFullPath { get; }
		private string SelectedChapterNumber { get; }
		private IEnumerable<FileInfo> RawsFiles { get; set; }
		private string TranslatorsName { get; }

		private int CurrentPageIndex { get; set; }
		private int PreviousPageIndex { get; set; }
		private List<PageInformation> PageInformations { get; set; }

		public RawsViewerForm(string rawsLocationFullPath, string chapterNumber, IEnumerable<FileInfo> rawsFiles, string translatorsName)
		{
			InitializeComponent();

			RawsLocationFullPath = rawsLocationFullPath;
			SelectedChapterNumber = chapterNumber;
			RawsFiles = rawsFiles;
			TranslatorsName = translatorsName;

			PageInformations = new List<PageInformation>();

			InitializePageInformations();

			PreviousPageIndex = 0;
			CurrentPageIndex = 0;
			LoadImage();
		}

		private void InitializePageInformations()
		{
			for ( int pageIndex = 0; pageIndex < RawsFiles.Count(); ++pageIndex )
			{
				PageInformations.Add(new PageInformation
				{
					pageNumber = pageIndex + 1,
					totalPanels = 0,
					filename = RawsFiles.ElementAt(pageIndex).Name,
					panelsWithSFX = new List<bool>()
				});
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
				FinishScriptCreation();
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

			bool didSucceed = int.TryParse(TotalPanelsTextBox.Text, out int totalPanelsInput);

			if ( !didSucceed )
			{
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

			if ( pageInfo.panelsWithSFX.Count == 0 )
			{
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

			foreach ( PageInformation pageInfo in PageInformations )
			{
				fileContents += PAGE_HEADER_BEGIN + pageInfo.pageNumber.ToString() + PAGE_HEADER_END;

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

			string outputDirectoryPath = RawsLocationFullPath.Substring(0, RawsLocationFullPath.LastIndexOf("\\") + 1);
			string outputFilepath = outputDirectoryPath + "Ch " + SelectedChapterNumber + " - TL " + TranslatorsName + ".txt";

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
	}
}
