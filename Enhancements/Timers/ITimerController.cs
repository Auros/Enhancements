using System;

namespace Enhancements.Timers
{
    public interface ITimerController
    {
        event Action<ITimeNotification> NotificationPing;
        void RegisterNotification(ITimeNotification notification);
        void UnregisterNotification(ITimeNotification notification);
    }
}