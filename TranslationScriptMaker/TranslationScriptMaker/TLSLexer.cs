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

		private static readonly Regex HeaderRegex = new(@"(-+# )(Page |Panel )([0-9]+?)(( - )([0-9]+?))?( #-+)({)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex FooterRegex = new(@"(-+#+-+)(})", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex ImportantInfoRegex = new(@"`(.*?)`", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex NoteRegex = new(@"\([A-Z]+/[A-Z]+:(.*?)\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex TLNoteRegex = new(@"\[[A-Z]/N\]:", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex SubsectionRegex = new(@"(\[(.*?)\])({)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private struct StyleMatch
		{
			internal int style;
			internal Match match;
		}

		private struct StyleMatchCollection
		{
			internal int style;
			internal MatchCollection matches;
		}

		internal static void StyleText(Scintilla scintilla)
		{
			if ( scintilla.Text.Length == 0 )
			{
				return;
			}

			scintilla.StartStyling(0);

			foreach ( Line currentLine in scintilla.Lines )
			{
				int totalCharactersRemaining = currentLine.Length;

				if ( currentLine.Text.Length == 0 )
				{
					continue;
				}

				switch (currentLine.Text[0])
				{
					case '\r':
					case '\n':
						scintilla.SetStyling(totalCharactersRemaining, STYLE_FORMATTING_BLOCK);
						continue;
					case '{':
					case '}':
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

				List<StyleMatchCollection> regexMatches = new List<StyleMatchCollection>
				{
					new() { style = STYLE_IMPORTANT, matches = ImportantInfoRegex.Matches(currentLine.Text) },
					new() { style = STYLE_NOTE, matches = NoteRegex.Matches(currentLine.Text) },
					new() { style = STYLE_TL_NOTE, matches = TLNoteRegex.Matches(currentLine.Text) },
					new() { style = STYLE_SUBSECTION, matches = SubsectionRegex.Matches(currentLine.Text) }
				};

				regexMatches.RemoveAll(RemoveAllZeroMatches);

				if ( !regexMatches.Any() )
				{
					scintilla.SetStyling(totalCharactersRemaining, STYLE_DEFAULT);
					continue;
				}

				List<StyleMatch> sortedMatches = new List<StyleMatch>();

				foreach ( StyleMatchCollection result in regexMatches )
				{
					foreach ( Match match in result.matches )
					{
						sortedMatches.Add(new StyleMatch {
							style = result.style,
							match = match
						});
					}
				}

				sortedMatches = sortedMatches.OrderBy(m => m.match.Index).ToList();

				int previousMatchEndingIndex = 0;

				foreach ( StyleMatch styleMatch in sortedMatches )
				{
					int lengthBetweenMatches = styleMatch.match.Index - previousMatchEndingIndex;

					if ( lengthBetweenMatches > 0 )
					{
						scintilla.SetStyling(lengthBetweenMatches, STYLE_DEFAULT);
						totalCharactersRemaining -= lengthBetweenMatches;
					}

					if ( styleMatch.style == STYLE_SUBSECTION )
					{
						scintilla.SetStyling(styleMatch.match.Groups[1].Length, styleMatch.style);
						scintilla.SetStyling(styleMatch.match.Groups[3].Length, STYLE_BRACES);
					}
					else
					{
						scintilla.SetStyling(styleMatch.match.Length, styleMatch.style);
					}

					totalCharactersRemaining -= styleMatch.match.Length;
					previousMatchEndingIndex = styleMatch.match.Index + styleMatch.match.Length;
				}

				scintilla.SetStyling(totalCharactersRemaining, STYLE_DEFAULT);
			}
		}

		private static bool RemoveAllZeroMatches(StyleMatchCollection styleMatch)
		{
			bool wereAllMatchesFailures = true;

			foreach ( Match match in styleMatch.matches )
			{
				if ( match.Success )
				{
					wereAllMatchesFailures = false;
				}
			}

			return wereAllMatchesFailures;
		}
	}
}
