using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using Enhancements.Timers;
using UnityEngine.UI;
using Zenject;

namespace Enhancements.UI.Timers
{
    [ViewDefinition("Enhancements.Views.Timers.timers-settings-info-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Timers\timers-settings-info-view.bsml")]
    public class TimersSettingsInfoView : BSMLAutomaticViewController
    {
        private TimerSettings _settings;

        [UIComponent("example")]
        protected Image exampleImage;

        [UIValue("enabled")]
        protected bool Enabled
        {
            get => _settings.Enabled;
            set => _settings.Enabled = value;
        }

        [UIValue("notify")]
        protected bool Notify
        {
            get => _settings.NotifyInGame;
            set => _settings.NotifyInGame = value;
        }

        [Inject]
        public void Construct(TimerSettings settings)
        {
            _settings = settings;
        }

        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            base.DidActivate(firstActivation, type);
            if (firstActivation)
            {
                exampleImage.SetImage("Enhancements.Resources.timers-example.gif");
            }
        }
    }
}
