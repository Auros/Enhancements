using SemVer;
using Zenject;
using Enhancements.Clock;

namespace Enhancements.Installers
{
    public class XInstaller : Installer<Config, Version, XInstaller>
    {
        private readonly Config _config;
        private readonly Version _version;

        public XInstaller(Config config, Version version)
        {
            _config = config;
            _version = version;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_config.Clock).AsSingle();
            Container.Bind(typeof(IClockController), typeof(ITickable), typeof(ClockController)).To<ClockController>().AsSingle();
            Container.Bind<XLoader>().AsSingle().Lazy();
            Container.Bind<Version>().WithId("Enhancements.Version").FromInstance(_version).AsCached();
        }
    }
}