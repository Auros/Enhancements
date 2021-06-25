using System;
using Zenject;
using System.Linq;
using System.Text;
using Enhancements.Breaktime;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components.Settings;

namespace Enhancements.UI.Breaktime
{
    [ViewDefinition("Enhancements.Views.Breaktime.breaktime-settings-global-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Breaktime\breaktime-settings-global-view.bsml")]
    public class BreaktimeSettingsGlobalView : BSMLAutomaticViewController
    {
        private BreaktimeSettings _settings;

        [UIParams]
        protected BSMLParserParams parserParams;

        [UIComponent("dropdown")]
        protected DropDownListSetting dropdown;

        [UIValue("enabled")]
        public bool Enabled
        {
            get => _settings.Enabled;
            set => _settings.Enabled = value;
        }

        [UIValue("selected-profile")]
        public string SelectedProfile
        {
            get => _settings.SelectedProfile;
            set => _settings.SelectedProfile = value;
        }

        [UIValue("randomize-mode")]
        public RandomizeMode RandomizeMode
        {
            get => _settings.RandomizeMode;
            set => _settings.RandomizeMode = value;
        }

        [UIValue("profiles")]
        public List<object> Profiles = new List<object>();

        [UIValue("randomize-modes")]
        public List<object> RandomizeModes => RandomizeOptions();

        [UIValue("minimum-break-time")]
        public float MinimumBreakTime
        {
            get => _settings.MinimumBreakTime;
            set => _settings.MinimumBreakTime = value;
        }

        [Inject]
        public void Construct(BreaktimeSettings settings)
        {
            _settings = settings;
            LoadProfiles();
        }

        public void LoadProfiles()
        {
            Profiles.Clear();
            Profiles.AddRange(_settings.Profiles.Select(x => (object)x.Name));

            dropdown?.UpdateChoices();
            parserParams?.EmitEvent("get");
        }

        private List<object> RandomizeOptions()
        {
            List<object> list = new List<object>();
            foreach (var item in Enum.GetValues(typeof(RandomizeMode)))
            {
                list.Add(item);
            }
            return list;
        }

        [UIAction("format-random")]
        protected string FormatRandom(RandomizeMode mode)
        {
            return Pascaler(mode.ToString());
        }

        public static string Pascaler(string random)
        {
            char[] chars = random.ToString().ToArray();
            StringBuilder builtFormat = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 0)
                {
                    builtFormat.Append(chars[i].ToString().ToUpper());
                    continue;
                }
                if (chars[i].ToString().ToUpper() == chars[i].ToString())
                {
                    builtFormat.Append(" ");
                }
                builtFormat.Append(chars[i]);
            }
            return builtFormat.ToString();
        }
    }
}