using Zenject;
using Enhancements.Clock;

namespace Enhancements.Installers
{
    public class XInstaller : Installer<Config, XInstaller>
    {
        private readonly Config _config;

        public XInstaller(Config config) => _config = config;

        public override void InstallBindings()
        {
            Container.BindInstance(_config.Clock).AsSingle();
            Container.Bind(typeof(IClockController), typeof(ITickable), typeof(ClockController)).To<ClockController>().AsSingle();
            Container.Bind<XLoader>().AsSingle().Lazy();
        }
    }
}