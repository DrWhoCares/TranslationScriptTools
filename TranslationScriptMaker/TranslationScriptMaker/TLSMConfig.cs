﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace TranslationScriptMaker
{
	internal class TLSMConfig
	{
		private const string CONFIG_FILENAME = "TLSMConfig.json";

		#region Functions
		public static TLSMConfig LoadConfig()
		{
			if ( !File.Exists(CONFIG_FILENAME) )
			{
				return new TLSMConfig();
			}

			var resultConfig = JsonConvert.DeserializeObject<TLSMConfig>(File.ReadAllText(CONFIG_FILENAME));
			resultConfig.Defaults();

			return resultConfig;
		}

		public TLSMConfig()
		{
			Defaults();
		}

		private void Defaults()
		{
			rawsLocation ??= "";
			scriptOutputLocation ??= "";
			translatorName = string.IsNullOrWhiteSpace(translatorName) ? Environment.UserName : translatorName;
			lastSelectedSeries ??= "";
			lastSelectedChapter ??= "";
			scriptOutputToChoice = scriptOutputToChoice != OutputToChoice.Invalid ? scriptOutputToChoice : OutputToChoice.ChapterFolder;
		}

		private void SaveConfig()
		{
			File.WriteAllText(CONFIG_FILENAME, JsonConvert.SerializeObject(this));
		}
		#endregion

		#region Member Variables
		private string rawsLocation;
		public string RawsLocation
		{
			get => rawsLocation;
			set
			{
				rawsLocation = value;
				SaveConfig();
			}
		}

		private string scriptOutputLocation;
		public string ScriptOutputLocation
		{
			get => scriptOutputLocation;
			set
			{
				scriptOutputLocation = value;
				SaveConfig();
			}
		}

		private string translatorName;
		public string TranslatorName
		{
			get => translatorName;
			set
			{
				translatorName = value;
				SaveConfig();
			}
		}

		private string lastSelectedSeries;
		public string LastSelectedSeries
		{
			get => lastSelectedSeries;
			set
			{
				lastSelectedSeries = value;
				SaveConfig();
			}
		}

		private string lastSelectedChapter;
		public string LastSelectedChapter
		{
			get => lastSelectedChapter;
			set
			{
				lastSelectedChapter = value;
				SaveConfig();
			}
		}

		private OutputToChoice scriptOutputToChoice;

		public OutputToChoice ScriptOutputToChoice
		{
			get => scriptOutputToChoice;
			set
			{
				scriptOutputToChoice = value;
				SaveConfig();
			}
		}

		private bool _shouldOutputAsTypesetterCompliant;

		public bool ShouldOutputAsTypesetterCompliant
		{
			get => _shouldOutputAsTypesetterCompliant;
			set
			{
				_shouldOutputAsTypesetterCompliant = value;
				SaveConfig();
			}
		}

		#endregion
	}

	internal enum OutputToChoice
	{
		Invalid = 0,
		ChapterFolder,
		WithRaws,
		CustomLocation
	}
}
