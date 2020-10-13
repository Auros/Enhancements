using System;
using Zenject;
using System.Linq;
using Enhancements.Clock;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components.Settings;

namespace Enhancements.UI.Clock
{
    [ViewDefinition("Enhancements.Views.Clock.clock-settings-format-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Clock\clock-settings-format-view.bsml")]
    public class ClockSettingsFormatView : BSMLAutomaticViewController
    {
        [UIValue("font-options")]
        public List<object> fontOptions = new List<object>();

        [UIValue("format-options")]
        public List<object> formatOptions = new List<object>();

        [UIValue("show-ingame")]
        protected bool ShowInGame
        {
            get => _settings.ShowInGame;
            set => _settings.ShowInGame = value;
        }

        [UIValue("size")]
        protected int Size
        {
            get => (int)_settings.Size;
            set => _settings.Size = value;
        }

        [UIValue("font")]
        protected string Font
        {
            get => _settings.Font;
            set => _settings.Font = value;
        }

        [UIValue("format")]
        protected string Format
        {
            get => _settings.Format;
            set => _settings.Format = value;
        }

        [UIValue("locale")]
        protected string Locale
        {
            get => string.IsNullOrEmpty(_settings.Culture) ? "Default Locale" : _settings.Culture;
            set
            {
                if (value != "Default Locale")
                {
                    _settings.Culture = value;
                }
                else
                {
                    _settings.Culture = "";
                }
            }
        }

        [UIAction("add-format")]
        protected void AddFormat(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _settings.Formats.Add(name);
                ReloadFormats();
            }
        }

        [UIAction("remove-format")]
        protected void RemoveFormat()
        {
            _settings.Formats.Remove(_settings.Format);
            _settings.Format = _settings.Formats.FirstOrDefault() ?? "";
            ReloadFormats();
        }

        [UIParams]
        protected BSMLParserParams parserParams;

        [UIComponent("dropdown")]
        protected DropDownListSetting dropdown;

        private ClockSettings _settings;

        [Inject]
        public void Construct(XLoader loader, ClockSettings settings)
        {
            _settings = settings;

            fontOptions.Add("Default");
            fontOptions.AddRange(loader.GetFontNames());
            ReloadFormats();
        }

        protected void ReloadFormats()
        {
            formatOptions.Clear();
            formatOptions.AddRange(_settings.Formats);
            dropdown?.tableView.ReloadData();
        }

        protected override void DidDeactivate(DeactivationType deactivationType)
        {
            base.DidDeactivate(deactivationType);
            parserParams.EmitEvent("hide-keyboard");
        }

        [UIAction("formatter-formatter")]
        protected string FormatterFormatter(string format)
        {
            return DateTime.Now.ToString(format);
        }
    }
}