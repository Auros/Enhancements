using System;
using Zenject;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Enhancements.Timers
{
    public class TimerController : ITimerController, ITickable
    {
        private float _cycleTime = 0f;
        private readonly float _cycleRate = 1f;
        public event Action<ITimeNotification> NotificationPing;
        private readonly ISet<ITimeNotification> _notifications = new HashSet<ITimeNotification>();
        private readonly Queue<ITimeNotification> _unregistrationQueue = new Queue<ITimeNotification>();

        public void RegisterNotification(ITimeNotification notification)
        {
            _notifications.Add(notification);
        }

        public void Tick()
        {
            if (_cycleTime >= _cycleRate)
            {
                CheckForNotifications();
                _cycleTime = 0;
            }
            _cycleTime += Time.deltaTime;
        }

        public void CheckForNotifications()
        {
            for (int i = 0; i < _notifications.Count; i++)
            {
                var notification = _notifications.ElementAt(i);
                if (notification.TimeReached())
                {
                    NotificationPing?.Invoke(notification);
                    if (notification.GetUnregisterAfterComplete())
                    {
                        _unregistrationQueue.Enqueue(notification);
                    }
                }
            }
            // Separate notification queue to not modify the notification list before it finishes iterating.
            for (int i = 0; i < _unregistrationQueue.Count; i++)
            {
                UnregisterNotification(_unregistrationQueue.Dequeue());
            }
            _unregistrationQueue.Clear();
        }

        public void UnregisterNotification(ITimeNotification notification)
        {
            _notifications.Remove(notification);
        }
    }
}