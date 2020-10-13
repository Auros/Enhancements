using SemVer;
using Zenject;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.Breaktime;

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
            Container.BindInterfacesAndSelfTo<BreaktimeLoader>().AsSingle();
            Container.BindInstance(_config.Misc).AsSingle();
            Container.BindInstance(_config.Clock).AsSingle();
            Container.BindInstance(_config.Timer).AsSingle();
            Container.BindInstance(_config.Volume).AsSingle();
            Container.BindInstance(_config.Optidra).AsSingle();
            Container.BindInstance(_config.Breaktime).AsSingle();
            Container.Bind(typeof(ITickable), typeof(ITimerController)).To<TimerController>().AsSingle();
            Container.Bind(typeof(IInitializable), typeof(System.IDisposable), typeof(Notifier)).To<Notifier>().AsSingle();
            Container.Bind(typeof(IClockController), typeof(ITickable), typeof(ClockController)).To<ClockController>().AsSingle();
            Container.Bind<XLoader>().AsSingle().Lazy();
            Container.Bind<Version>().WithId("Enhancements.Version").FromInstance(_version).AsCached();
        }
    }
}