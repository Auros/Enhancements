using SiraUtil;
using Enhancements.UI;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.Volume;
using Enhancements.UI.Misc;
using Enhancements.UI.Clock;
using Enhancements.UI.Timers;
using Enhancements.UI.Volume;
using Enhancements.UI.Breaktime;
using Installer = Zenject.Installer;
using Zenject;
using System;

namespace Enhancements.Installers
{
    public class XMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BasicClock>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuVolumeManager>().AsSingle();

            Container.Bind<BasicClockView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<NewReminderView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind(typeof(NotificationView), typeof(IInitializable), typeof(IDisposable)).To<NotificationView>().FromNewComponentAsViewController().AsSingle();

            Container.Bind<XInfoView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<MiscSettingsInfoView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<ClockSettingsInfoView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<TimersSettingsInfoView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<VolumeSettingsInfoView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<ClockSettingsPosColView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<ClockSettingsFormatView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<ExtraTweaksSettingsView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<BreaktimeSettingsInfoView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<BreaktimeSettingsGlobalView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<BreaktimeSettingsProfileView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<XSettingsNavigationController>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<XSettingsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();

            Container.BindInterfacesAndSelfTo<MenuButtonManager>().AsSingle();
        }
    }
}