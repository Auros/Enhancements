using HMUI;
using Zenject;
using Enhancements.UI.Misc;
using Enhancements.UI.Clock;
using Enhancements.UI.Timers;
using Enhancements.UI.Volume;
using BeatSaberMarkupLanguage;
using Enhancements.UI.Breaktime;

namespace Enhancements.UI
{
    public class XSettingsFlowCoordinator : FlowCoordinator
    {
        private int _lastId = 0;
        private XInfoView _infoView;
        private MainFlowCoordinator _mainFlowCoordinator;
        private XSettingsNavigationController _settingsNavigationView;

        private ClockSettingsInfoView _clockSettingsInfoView;
        private ClockSettingsPosColView _clockSettingsPosColView;
        private ClockSettingsFormatView _clockSettingsFormatView;

        private TimersSettingsInfoView _timersSettingsInfoView;

        private BreaktimeSettingsInfoView _breaktimeSettingsInfoView;
        private BreaktimeSettingsGlobalView _breaktimeSettingsGlobalView;
        private BreaktimeSettingsProfileView _breaktimeSettingsProfileView;

        private VolumeSettingsInfoView _volumeSettingsInfoView;

        private MiscSettingsInfoView _miscSettingsInfoView;
        private ExtraTweaksSettingsView _extraTweaksSettingsView;

        [Inject]
        public void Construct(XInfoView infoView, MainFlowCoordinator mainFlowCoordinator, XSettingsNavigationController settingsNavigationView,
                              ClockSettingsInfoView clockSettingsInfoView, ClockSettingsPosColView clockSettingsPosColView, ClockSettingsFormatView clockSettingsFormatView,
                              TimersSettingsInfoView timersSettingsInfoView,
                              BreaktimeSettingsInfoView breaktimeSettingsInfoView, BreaktimeSettingsGlobalView breaktimeSettingsGlobalView, BreaktimeSettingsProfileView breaktimeSettingsProfileView,
                              VolumeSettingsInfoView volumeSettingsInfoView,
                              MiscSettingsInfoView miscSettingsInfoView, ExtraTweaksSettingsView extraTweaksSettingsView)
        {
            _infoView = infoView;
            _mainFlowCoordinator = mainFlowCoordinator;
            _settingsNavigationView = settingsNavigationView;

            _clockSettingsInfoView = clockSettingsInfoView;
            _clockSettingsPosColView = clockSettingsPosColView;
            _clockSettingsFormatView = clockSettingsFormatView;

            _timersSettingsInfoView = timersSettingsInfoView;

            _breaktimeSettingsInfoView = breaktimeSettingsInfoView;
            _breaktimeSettingsGlobalView = breaktimeSettingsGlobalView;
            _breaktimeSettingsProfileView = breaktimeSettingsProfileView;

            _volumeSettingsInfoView = volumeSettingsInfoView;

            _miscSettingsInfoView = miscSettingsInfoView;
            _extraTweaksSettingsView = extraTweaksSettingsView;
        }

        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            if (firstActivation)
            {
                title = "Enhancements";
                showBackButton = true;
            }
            ProvideInitialViewControllers(_infoView, bottomScreenViewController: _settingsNavigationView);
            _settingsNavigationView.DidSelectSettingOption += ShowSettingsPage;
            _breaktimeSettingsProfileView.ProfilesUpdated += ProfilesUpdated;
            _settingsNavigationView?.SelectFirstCell();
            _lastId = 0;
        }

        private void ProfilesUpdated()
        {
            _breaktimeSettingsGlobalView.LoadProfiles();
        }

        protected void ShowSettingsPage(string name, int id)
        {
            ViewController match = null;
            ViewController leftMatch = null;
            ViewController rightMatch = null;

            switch (name)
            {
                case "Changelog":
                    match = _infoView;
                    leftMatch = null;
                    rightMatch = null;
                    break;
                case "Clock":
                    match = _clockSettingsInfoView;
                    leftMatch = _clockSettingsPosColView;
                    rightMatch = _clockSettingsFormatView;
                    break;
                case "Timers":
                    match = _timersSettingsInfoView;
                    leftMatch = null;
                    rightMatch = null;
                    break;
                case "Breaktime":
                    match = _breaktimeSettingsInfoView;
                    leftMatch = _breaktimeSettingsGlobalView;
                    rightMatch = _breaktimeSettingsProfileView;
                    break;
                case "Volume":
                    match = _volumeSettingsInfoView;
                    leftMatch = null;
                    rightMatch = null;
                    break;
                case "Mini Settings and Optidra":
                    match = _miscSettingsInfoView;
                    leftMatch = _extraTweaksSettingsView;
                    rightMatch = null;
                    break;
            }
            if (match != null && match != topViewController)
            {
                ViewController.SlideAnimationDirection slide = _lastId > id ? ViewController.SlideAnimationDirection.Left : ViewController.SlideAnimationDirection.Right;
                ReplaceTopViewController(match, slideAnimationDirection: slide);
                SetLeftScreenViewController(leftMatch);
                SetRightScreenViewController(rightMatch);
                _lastId = id;
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _settingsNavigationView.DidSelectSettingOption -= ShowSettingsPage;
            _breaktimeSettingsProfileView.ProfilesUpdated -= ProfilesUpdated;
            base.BackButtonWasPressed(topViewController);
            _mainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}