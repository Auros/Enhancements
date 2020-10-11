using Zenject;
using SiraUtil;
using Enhancements.Clock;
using Enhancements.Timers;

namespace Enhancements.Installers
{
    public class XGameInstaller : Installer
    {
        private ClockSettings _clockSettings;
        private TimerSettings _timerSettings;

        public XGameInstaller(ClockSettings clockSettings, TimerSettings timerSettings)
        {
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
        }
    }
}