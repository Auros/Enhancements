using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using Enhancements.Misc;
using Zenject;

namespace Enhancements.UI.Misc
{
    [ViewDefinition("Enhancements.Views.Misc.extra-tweaks-settings-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Misc\extra-tweaks-settings-view.bsml")]
    public class ExtraTweaksSettingsView : BSMLAutomaticViewController
    {
        private MiscSettings _settings;

        [UIValue("bpm-fix")]
        protected bool BPMFix
        {
            get => _settings.BPMFixEnabled;
            set => _settings.BPMFixEnabled = value;
        }

        [UIValue("bl-menu")]
        protected bool ButtonLockMenu
        {
            get => _settings.ButtonLockMenu;
            set => _settings.ButtonLockMenu = value;
        }

        [UIValue("bl-restart")]
        protected bool ButtonLockRestart
        {
            get => _settings.ButtonLockRestart;
            set => _settings.ButtonLockRestart = value;
        }

        [UIValue("bl-continue")]
        protected bool ButtonLockContinue
        {
            get => _settings.ButtonLockContinue;
            set => _settings.ButtonLockContinue = value;
        }

        [Inject]
        public void Construct(MiscSettings settings)
        {
            _settings = settings;
        }
    }
}