using Zenject;
using SiraUtil;
using Enhancements.Misc;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.Volume;
using Enhancements.Breaktime;
using Enhancements.UI.Breaktime;

namespace Enhancements.Installers
{
    public class XGameInstaller : Installer
    {
        private MiscSettings _miscSettings;
        private ClockSettings _clockSettings;
        private TimerSettings _timerSettings;
        private PlayerDataModel _playerDataModel;
        private BreaktimeSettings _breaktimeSettings;

        public XGameInstaller(MiscSettings miscSettings, ClockSettings clockSettings, TimerSettings timerSettings, PlayerDataModel playerDataModel, BreaktimeSettings breaktimeSettings)
        {
            _miscSettings = miscSettings;
            _clockSettings = clockSettings;
            _timerSettings = timerSettings;
            _playerDataModel = playerDataModel;
            _breaktimeSettings = breaktimeSettings;
        }

        public override void InstallBindings()
        {
            var textAndHuds = !_playerDataModel.playerData.playerSpecificSettings.noTextsAndHuds;
            if (_clockSettings.Enabled && _clockSettings.ShowInGame && textAndHuds)
            {
                Container.BindViewController<BasicClockView>();
                Container.BindViewController<NewReminderView>();
                Container.BindInterfacesTo<BasicClock>().AsSingle();
            }
            if (_timerSettings.Enabled && _timerSettings.NotifyInGame && textAndHuds)
            {
                Container.BindViewController<NotificationView>();
            }
            Container.BindInterfacesAndSelfTo<GameVolumeModifier>().AsSingle();
            if (_miscSettings.ButtonLockMenu || _miscSettings.ButtonLockRestart || _miscSettings.ButtonLockContinue)
            {
                Container.Bind<ButtonLock>().FromNewComponentOnRoot().AsSingle().NonLazy();
            }
            if (_breaktimeSettings.Enabled && textAndHuds)
            {
                Container.BindInterfacesAndSelfTo<BreaktimeManager>().AsSingle();
                Container.BindViewController<BreaktimeModule>(true);
            }
        }
    }
}