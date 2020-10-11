using Zenject;
using Enhancements.Clock;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI.Clock
{
    [ViewDefinition("Enhancements.Views.Clock.clock-settings-info-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Clock\clock-settings-info-view.bsml")]
    public class ClockSettingsInfoView : BSMLAutomaticViewController
    {
        private ClockSettings _settings;
        
        [Inject]
        public void Construct(ClockSettings settings)
        {
            _settings = settings;
        }

        [UIValue("enabled")]
        protected bool Enabled
        {
            get => _settings.Enabled;
            set => _settings.Enabled = value;
        }
    }
}