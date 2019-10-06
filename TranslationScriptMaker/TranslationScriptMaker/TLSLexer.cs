using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ScintillaNET;

namespace TranslationScriptMaker
{
	internal static class TLSLexer
	{
		internal const int STYLE_DEFAULT = 0;
		internal const int STYLE_FORMATTING_BLOCK = 1;
		internal const int STYLE_HEADER_TITLE = 2;
		internal const int STYLE_HEADER_VALUE = 3;
		internal const int STYLE_BRACES = 4;
		internal const int STYLE_IMPORTANT = 5;
		internal const int STYLE_NOTE = 6;
		internal const int STYLE_TL_NOTE = 7;
		internal const int STYLE_SUBSECTION = 8;

		private static readonly Regex HeaderRegex = new Regex(@"(-+# )(Page |Panel )([0-9]+?)(( - )([0-9]+?))?( #-+)({)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex FooterRegex = new Regex(@"(-+#+-+)(})", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex ImportantInfoRegex = new Regex(@"`(.*?)`", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex NoteRegex = new Regex(@"\(T/P:(.*?)\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex TLNoteRegex = new Regex(@"\[T/N\]:", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex SubsectionRegex = new Regex(@"(\[(.*?)\])({)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private struct StyleMatch
		{
			internal int style;
			internal Match match;
		}

		internal static void StyleText(Scintilla scintilla)
		{
			if ( scintilla.Text.Length == 0 )
			{
				return;
			}

			scintilla.StartStyling(0);

			for ( int lineIndex = 0; lineIndex < scintilla.Lines.Count; ++lineIndex )
			{
				Line currentLine = scintilla.Lines[lineIndex];
				int totalCharactersRemaining = currentLine.Length;

				if ( currentLine.Text.Length == 0 )
				{
					continue;
				}

				if ( currentLine.Text[0] == '\r' || currentLine.Text[0] == '\n' )
				{
					scintilla.SetStyling(totalCharactersRemaining, STYLE_FORMATTING_BLOCK);
					continue;
				}

				if ( currentLine.Text[0] == '{' || currentLine.Text[0] == '}' )
				{
					scintilla.SetStyling(totalCharactersRemaining, STYLE_BRACES);
					continue;
				}

				Match formattingMatch = HeaderRegex.Match(currentLine.Text);

				if ( formattingMatch.Success )
				{
					scintilla.SetStyling(formattingMatch.Groups[1].Length, STYLE_FORMATTING_BLOCK);
					scintilla.SetStyling(formattingMatch.Groups[2].Length, STYLE_HEADER_TITLE);
					scintilla.SetStyling(formattingMatch.Groups[3].Length, STYLE_HEADER_VALUE);

					if ( formattingMatch.Groups[4].Length > 0 )
					{
						scintilla.SetStyling(formattingMatch.Groups[5].Length, STYLE_FORMATTING_BLOCK);
						scintilla.SetStyling(formattingMatch.Groups[6].Length, STYLE_HEADER_VALUE);
					}

					scintilla.SetStyling(formattingMatch.Groups[7].Length, STYLE_FORMATTING_BLOCK);
					scintilla.SetStyling(formattingMatch.Groups[8].Length, STYLE_BRACES);

					totalCharactersRemaining -= formattingMatch.Length;
					scintilla.SetStyling(totalCharactersRemaining, STYLE_BRACES);
					continue;
				}

				formattingMatch = FooterRegex.Match(currentLine.Text);

				if ( formattingMatch.Success )
				{
					scintilla.SetStyling(formattingMatch.Groups[1].Length, STYLE_FORMATTING_BLOCK);
					scintilla.SetStyling(formattingMatch.Groups[2].Length, STYLE_BRACES);

					totalCharactersRemaining -= formattingMatch.Length;
					scintilla.SetStyling(totalCharactersRemaining, STYLE_BRACES);
					continue;
				}

				List<StyleMatch> regexMatches = new List<StyleMatch>
				{
					new StyleMatch { style = STYLE_IMPORTANT, match = ImportantInfoRegex.Match(currentLine.Text) },
					new StyleMatch { style = STYLE_NOTE, match = NoteRegex.Match(currentLine.Text) },
					new StyleMatch { style = STYLE_TL_NOTE, match = TLNoteRegex.Match(currentLine.Text) },
					new StyleMatch { style = STYLE_SUBSECTION, match = SubsectionRegex.Match(currentLine.Text) }
				};

				regexMatches.RemoveAll(m => m.match.Success == false);

				if ( regexMatches.Count > 0 )
				{
					regexMatches = regexMatches.OrderBy(m => m.match.Index).ToList();

					int previousMatchEndingIndex = 0;

					foreach ( StyleMatch result in regexMatches )
					{
						int lengthBetweenMatches = result.match.Index - previousMatchEndingIndex;

						if ( lengthBetweenMatches > 0 )
						{
							scintilla.SetStyling(lengthBetweenMatches, STYLE_DEFAULT);
							totalCharactersRemaining -= lengthBetweenMatches;
						}

						if ( result.style == STYLE_SUBSECTION )
						{
							scintilla.SetStyling(result.match.Groups[1].Length, result.style);
							scintilla.SetStyling(result.match.Groups[3].Length, STYLE_BRACES);
						}
						else
						{
							scintilla.SetStyling(result.match.Length, result.style);
						}

						totalCharactersRemaining -= result.match.Length;
						previousMatchEndingIndex = result.match.Index + result.match.Length;
					}
				}

				scintilla.SetStyling(totalCharactersRemaining, STYLE_DEFAULT);
			}
		}
	}
}
