using System;
using Zenject;
using Enhancements.Clock;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI.Clock
{
    [HotReload(RelativePathToLayout = @"..\..\Views\Clock\clock-settings-format-view.bsml")]
    public class ClockSettingsFormatView : BSMLAutomaticViewController
    {
        [UIValue("font-options")]
        public List<object> fontOptions = new List<object>();

        [UIValue("format-options")]
        public List<object> formatOptions = new List<object>();

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

        private ClockSettings _settings;

        [Inject]
        public void Construct(XLoader loader, ClockSettings settings)
        {
            _settings = settings;

            fontOptions.Add("Default");
            fontOptions.AddRange(loader.GetFontNames());
            formatOptions.AddRange(settings.Formats);
        }

        [UIAction("formatter-formatter")]
        protected string FormatterFormatter(string format)
        {
            return DateTime.Now.ToString(format);
        }
    }
}