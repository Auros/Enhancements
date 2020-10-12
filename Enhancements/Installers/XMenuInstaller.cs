using Zenject;
using SiraUtil;
using UnityEngine;
using Enhancements.UI;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.UI.Misc;
using Enhancements.UI.Clock;
using Enhancements.UI.Timers;
using Installer = Zenject.Installer;
using Enhancements.UI.Volume;
using Enhancements.Volume;

namespace Enhancements.Installers
{
    public class XMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Application.targetFrameRate = 90;

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
            Container.Bind<XSettingsNavigationController>().FromNewComponentOnNewGameObject(nameof(XSettingsNavigationController)).AsSingle().OnInstantiated(Utilities.SetupViewController);

            Container.Bind<XSettingsFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(XSettingsFlowCoordinator)).AsSingle();

            Container.BindInterfacesAndSelfTo<MenuButtonManager>().AsSingle();
        }

        private static int GetSetting(MonoInstaller monoInstaller)
        {
            Plugin.Log.Info("HELLO");
            return 25;
        }
    }
}