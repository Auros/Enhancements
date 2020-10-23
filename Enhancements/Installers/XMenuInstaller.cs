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

            Container.BindViewController<BasicClockView>();
            Container.BindViewController<NewReminderView>();
            Container.BindViewController<NotificationView>();

            Container.BindViewController<XInfoView>();
            Container.BindViewController<MiscSettingsInfoView>();
            Container.BindViewController<ClockSettingsInfoView>();
            Container.BindViewController<TimersSettingsInfoView>();
            Container.BindViewController<VolumeSettingsInfoView>();
            Container.BindViewController<ClockSettingsPosColView>();
            Container.BindViewController<ClockSettingsFormatView>();
            Container.BindViewController<ExtraTweaksSettingsView>();
            Container.BindViewController<BreaktimeSettingsInfoView>();
            Container.BindViewController<BreaktimeSettingsGlobalView>();
            Container.BindViewController<BreaktimeSettingsProfileView>();
            Container.BindViewController<XSettingsNavigationController>();
            Container.BindFlowCoordinator<XSettingsFlowCoordinator>();

            Container.BindInterfacesAndSelfTo<MenuButtonManager>().AsSingle();
        }
    }
}