using System;
using Zenject;
using System.Collections.Generic;

namespace Enhancements.Timers
{
    public class Notifier : IInitializable, IDisposable
    {
        private readonly TimerSettings _settings;
        private readonly ITimerController _timerController;
        private readonly GameScenesManager _gameScenesManager;
        public event Action<ITimeNotification> NotificationPing;
        private static readonly Queue<ITimeNotification> _queue = new Queue<ITimeNotification>();
        public bool IsViewing { private get; set; } = false;

        public Notifier(TimerSettings settings, ITimerController timerController, GameScenesManager gameScenesManager)
        {
            _settings = settings;
            _timerController = timerController;
            _gameScenesManager = gameScenesManager;
        }

        public ITimeNotification NextNotification()
        {
            // If there's no items, just return null now
            if (_queue.Count == 0) return null;

            ITimeNotification notif = _queue.Dequeue();
            return notif;
        }

        public void Initialize()
        {
            _timerController.NotificationPing += ReceivedNotification;
        }

        private void ReceivedNotification(ITimeNotification notification)
        {
            if (!_settings.Enabled)
            {
                return;
            }
            if ((!_settings.NotifyInGame && _gameScenesManager.currentScenesContainer.TryResolve<IDifficultyBeatmap>() != null) || IsViewing)
            {
                _queue.Enqueue(notification);
            }
            else
            {
                NotificationPing?.Invoke(notification);
            }
        }

        public void Dispose()
        {
            _timerController.NotificationPing -= ReceivedNotification;
        }
    }
}
