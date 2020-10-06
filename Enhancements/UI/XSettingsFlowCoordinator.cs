using HMUI;
using Zenject;
using BeatSaberMarkupLanguage;

namespace Enhancements.UI
{
    public class XSettingsFlowCoordinator : FlowCoordinator
    {
        private XInfoView _infoView;
        private MainFlowCoordinator _mainFlowCoordinator;
        private XSettingNavigationView _settingsNavigationView;

        [Inject]
        public void Construct(XInfoView infoView, MainFlowCoordinator mainFlowCoordinator, XSettingNavigationView settingsNavigationView)
        {
            _infoView = infoView;
            _mainFlowCoordinator = mainFlowCoordinator;
            _settingsNavigationView = settingsNavigationView;
        }

        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            if (firstActivation)
            {
                title = "Enhancements";
                showBackButton = true;
            }
            ProvideInitialViewControllers(_infoView, bottomScreenViewController: _settingsNavigationView);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            _mainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}