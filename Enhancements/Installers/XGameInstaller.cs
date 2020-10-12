using Zenject;
using SiraUtil;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.Volume;
using Enhancements.Misc;

namespace Enhancements.Installers
{
    public class XGameInstaller : Installer
    {
        private MiscSettings _miscSettings;
        private ClockSettings _clockSettings;
        private TimerSettings _timerSettings;

        public XGameInstaller(MiscSettings miscSettings, ClockSettings clockSettings, TimerSettings timerSettings)
        {
            _miscSettings = miscSettings;
            _clockSettings = clockSettings;
            _timerSettings = timerSettings;
        }

        public override void InstallBindings()
        {
            if (_clockSettings.Enabled && _clockSettings.ShowInGame)
            {
                Container.Bind<BasicClockView>().FromNewComponentOnNewGameObject(nameof(BasicClockView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
                Container.Bind<NewReminderView>().FromNewComponentOnNewGameObject(nameof(NewReminderView)).AsSingle().OnInstantiated(Utilities.SetupViewController);
                Container.BindInterfacesTo<BasicClock>().AsSingle();
            }
            if (_timerSettings.Enabled && _timerSettings.NotifyInGame)
            {
                Container.Bind<NotificationView>().FromNewComponentOnNewGameObject(nameof(NotificationView)).AsSingle().OnInstantiated(Utilities.SetupViewController).NonLazy();
            }
            Container.BindInterfacesAndSelfTo<GameVolumeModifier>().AsSingle();
            if (_miscSettings.ButtonLockMenu || _miscSettings.ButtonLockRestart || _miscSettings.ButtonLockContinue)
            {
                Container.Bind<ButtonLock>().FromNewComponentOnRoot().AsSingle().NonLazy();
            }
        }
    }
}