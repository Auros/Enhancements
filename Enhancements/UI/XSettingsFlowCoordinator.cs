using HMUI;
using Zenject;
using BeatSaberMarkupLanguage;

namespace Enhancements.UI
{
    public class XSettingsFlowCoordinator : FlowCoordinator
    {
        private XInfoView _infoView;
        private MainFlowCoordinator _mainFlowCoordinator;

        [Inject]
        public void Construct(XInfoView infoView, MainFlowCoordinator mainFlowCoordinator)
        {
            _infoView = infoView;
            _mainFlowCoordinator = mainFlowCoordinator;
        }

        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            if (firstActivation)
            {
                title = "Enhancements";
                showBackButton = true;
            }
            ProvideInitialViewControllers(_infoView);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            _mainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}