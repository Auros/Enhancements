using SiraUtil.Zenject;
using Enhancements.Clock;
using BeatSaberMarkupLanguage;
using Installer = Zenject.Installer;

namespace Enhancements.Installers
{
    [RequiresInstaller(typeof(EInstaller.InitData))]
    public class EMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            var clockView = BeatSaberUI.CreateViewController<BasicClockView>();
            Container.ForceBindComponent<BasicClockView>(clockView);

            Container.BindInterfacesTo<BasicClock>().AsSingle();
            
        }
    }
}