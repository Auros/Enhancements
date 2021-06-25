using Zenject;
using SiraUtil;
using Enhancements.Misc;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.Volume;
using Enhancements.Breaktime;
using Enhancements.UI.Breaktime;
using System;

namespace Enhancements.Installers
{
    public class XGameInstaller : Installer
    {
        private readonly MiscSettings _miscSettings;
        private readonly ClockSettings _clockSettings;
        private readonly TimerSettings _timerSettings;
        private readonly PlayerDataModel _playerDataModel;
        private readonly BreaktimeSettings _breaktimeSettings;

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
                Container.Bind(typeof(BasicClockView), typeof(IInitializable)).To<BasicClockView>().FromNewComponentAsViewController().AsSingle();
                Container.Bind(typeof(NewReminderView), typeof(IInitializable)).To<NewReminderView>().FromNewComponentAsViewController().AsSingle();
                Container.BindInterfacesTo<BasicClock>().AsSingle();
            }
            if (_timerSettings.Enabled && _timerSettings.NotifyInGame && textAndHuds)
            {
                Container.Bind(typeof(NotificationView), typeof(IInitializable), typeof(IDisposable)).To<NotificationView>().FromNewComponentAsViewController().AsSingle();
            }
            Container.BindInterfacesAndSelfTo<GameVolumeModifier>().AsSingle();
            if (_miscSettings.ButtonLockMenu || _miscSettings.ButtonLockRestart || _miscSettings.ButtonLockContinue)
            {
                Container.BindInterfacesTo<ButtonLock>().FromNewComponentOnRoot().AsSingle();
            }
            if (_breaktimeSettings.Enabled && textAndHuds)
            {
                Container.BindInterfacesAndSelfTo<BreaktimeManager>().AsSingle();
                Container.Bind(typeof(BreaktimeModule), typeof(IInitializable)).To<BreaktimeModule>().FromNewComponentAsViewController().AsSingle();
            }
        }
    }
}