using HMUI;
using Zenject;
using BeatSaberMarkupLanguage;
using Enhancements.UI.Clock;

namespace Enhancements.UI
{
    public class XSettingsFlowCoordinator : FlowCoordinator
    {
        private int _lastId = 0;
        private XInfoView _infoView;
        private MainFlowCoordinator _mainFlowCoordinator;
        private XSettingNavigationView _settingsNavigationView;

        private ClockSettingsInfoView _clockSettingsInfoView;
        private ClockSettingsPosColView _clockSettingsPosColView;
        private ClockSettingsFormatView _clockSettingsFormatView;

        [Inject]
        public void Construct(XInfoView infoView, MainFlowCoordinator mainFlowCoordinator, XSettingNavigationView settingsNavigationView,
                              ClockSettingsInfoView clockSettingsInfoView, ClockSettingsPosColView clockSettingsPosColView, ClockSettingsFormatView clockSettingsFormatView)
        {
            _infoView = infoView;
            _mainFlowCoordinator = mainFlowCoordinator;
            _clockSettingsInfoView = clockSettingsInfoView;
            _settingsNavigationView = settingsNavigationView;
            _clockSettingsPosColView = clockSettingsPosColView;
            _clockSettingsFormatView = clockSettingsFormatView;
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
            _settingsNavigationView?.SelectFirstCell();
            _lastId = 0;
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

            base.BackButtonWasPressed(topViewController);
            _mainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}