using SiraUtil;
using UnityEngine;
using Enhancements.Clock;
using Installer = Zenject.Installer;

namespace Enhancements.Installers
{
    public class XMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<BasicClockView>().FromNewComponentOnNewGameObject(nameof(BasicClockView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.BindInterfacesTo<BasicClock>().AsSingle();

            Application.targetFrameRate = 90;
        }
    }
}