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
using BeatSaberMarkupLanguage;

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

            Container.BindViewController<BasicClockView>(BeatSaberUI.CreateViewController<BasicClockView>());
            Container.BindViewController<NewReminderView>(BeatSaberUI.CreateViewController<NewReminderView>());
            Container.BindViewController<NotificationView>(BeatSaberUI.CreateViewController<NotificationView>());

            Container.BindViewController<XInfoView>(BeatSaberUI.CreateViewController<XInfoView>());
            Container.BindViewController<MiscSettingsInfoView>(BeatSaberUI.CreateViewController<MiscSettingsInfoView>());
            Container.BindViewController<ClockSettingsInfoView>(BeatSaberUI.CreateViewController<ClockSettingsInfoView>());
            Container.BindViewController<TimersSettingsInfoView>(BeatSaberUI.CreateViewController<TimersSettingsInfoView>());
            Container.BindViewController<VolumeSettingsInfoView>(BeatSaberUI.CreateViewController<VolumeSettingsInfoView>());
            Container.BindViewController<ClockSettingsPosColView>(BeatSaberUI.CreateViewController<ClockSettingsPosColView>());
            Container.BindViewController<ClockSettingsFormatView>(BeatSaberUI.CreateViewController<ClockSettingsFormatView>());
            Container.BindViewController<ExtraTweaksSettingsView>(BeatSaberUI.CreateViewController<ExtraTweaksSettingsView>());
            Container.BindViewController<BreaktimeSettingsInfoView>(BeatSaberUI.CreateViewController<BreaktimeSettingsInfoView>());
            Container.BindViewController<BreaktimeSettingsGlobalView>(BeatSaberUI.CreateViewController<BreaktimeSettingsGlobalView>());
            Container.BindViewController<BreaktimeSettingsProfileView>(BeatSaberUI.CreateViewController<BreaktimeSettingsProfileView>());
            Container.BindViewController<XSettingsNavigationController>(BeatSaberUI.CreateViewController<XSettingsNavigationController>());
            Container.BindFlowCoordinator<XSettingsFlowCoordinator>(BeatSaberUI.CreateFlowCoordinator<XSettingsFlowCoordinator>());

            //Container.Bind<XSettingsFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(XSettingsFlowCoordinator)).AsSingle();

            Container.BindInterfacesAndSelfTo<MenuButtonManager>().AsSingle();
        }
    }
}