using SiraUtil.Zenject;
using Enhancements.Clock;
using BeatSaberMarkupLanguage;
using Installer = Zenject.Installer;

namespace Enhancements.Installers
{
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