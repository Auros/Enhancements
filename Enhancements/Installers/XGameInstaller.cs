using Zenject;
using SiraUtil;
using Enhancements.Clock;

namespace Enhancements.Installers
{
    public class XGameInstaller : Installer
    {
        private ClockSettings _clockSettings;

        public XGameInstaller(ClockSettings clockSettings)
        {
            _clockSettings = clockSettings;
        }

        public override void InstallBindings()
        {
            if (_clockSettings.Enabled && _clockSettings.ShowInGame)
            {
                Container.Bind<BasicClockView>().FromNewComponentOnNewGameObject(nameof(BasicClockView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
                Container.BindInterfacesTo<BasicClock>().AsSingle();
            }
        }
    }
}