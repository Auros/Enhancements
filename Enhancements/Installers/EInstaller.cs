using Zenject;
using Enhancements.Clock;
using Installer = Zenject.Installer;

namespace Enhancements.Installers
{
    public class EInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IClockController), typeof(ITickable), typeof(ClockController)).To<ClockController>().AsSingle();
            Container.Bind(typeof(IInitializable), typeof(ELoader)).To<ELoader>().AsSingle();
        }
    }
}