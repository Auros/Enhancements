using System;

namespace Enhancements.Timers
{
    public interface ITimerController
    {
        event Action<ITImeNotification> NotificationPing;
        void RegisterNotification(ITImeNotification notification);
        void UnregisterNotification(ITImeNotification notification);
    }
}