using Zenject;
using Enhancements.Clock;

namespace Enhancements.Installers
{
    public class EInstaller : Installer<Config, EInstaller>
    {
        private readonly Config _config;

        public EInstaller(Config config) => _config = config;

        public override void InstallBindings()
        {
            Container.BindInstance(_config.Clock).AsSingle();
            Container.Bind(typeof(IClockController), typeof(ITickable), typeof(ClockController)).To<ClockController>().AsSingle();
            Container.Bind<ELoader>().AsSingle().Lazy();
        }
    }
}