using SiraUtil;
using UnityEngine;
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

namespace Enhancements.Installers
{
    public class XMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
#if DEBUG
            Application.targetFrameRate = 90;
#endif
            Container.BindInterfacesTo<BasicClock>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuVolumeManager>().AsSingle();
            Container.Bind<BasicClockView>().FromNewComponentOnNewGameObject(nameof(BasicClockView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<NewReminderView>().FromNewComponentOnNewGameObject(nameof(NewReminderView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<NotificationView>().FromNewComponentOnNewGameObject(nameof(NotificationView)).AsSingle().OnInstantiated(Utilities.SetupViewController).NonLazy();

            Container.Bind<XInfoView>().FromNewComponentOnNewGameObject(nameof(XInfoView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            //Container.Bind<OptidraSettingsView>().FromNewComponentOnNewGameObject(nameof(OptidraSettingsView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<MiscSettingsInfoView>().FromNewComponentOnNewGameObject(nameof(MiscSettingsInfoView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<ClockSettingsInfoView>().FromNewComponentOnNewGameObject(nameof(ClockSettingsInfoView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<TimersSettingsInfoView>().FromNewComponentOnNewGameObject(nameof(TimersSettingsInfoView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<VolumeSettingsInfoView>().FromNewComponentOnNewGameObject(nameof(VolumeSettingsInfoView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<ClockSettingsPosColView>().FromNewComponentOnNewGameObject(nameof(ClockSettingsPosColView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<ClockSettingsFormatView>().FromNewComponentOnNewGameObject(nameof(ClockSettingsFormatView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<ExtraTweaksSettingsView>().FromNewComponentOnNewGameObject(nameof(ExtraTweaksSettingsView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<BreaktimeSettingsInfoView>().FromNewComponentOnNewGameObject(nameof(BreaktimeSettingsInfoView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<BreaktimeSettingsGlobalView>().FromNewComponentOnNewGameObject(nameof(BreaktimeSettingsGlobalView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<BreaktimeSettingsProfileView>().FromNewComponentOnNewGameObject(nameof(BreaktimeSettingsProfileView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
            Container.Bind<XSettingsNavigationController>().FromNewComponentOnNewGameObject(nameof(XSettingsNavigationController)).AsSingle().OnInstantiated(Utilities.SetupViewController);

            Container.Bind<XSettingsFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(XSettingsFlowCoordinator)).AsSingle();

            Container.BindInterfacesAndSelfTo<MenuButtonManager>().AsSingle();
        }
    }
}