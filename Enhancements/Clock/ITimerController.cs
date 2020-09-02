using System;

namespace Enhancements.Clock
{
    public interface ITimerController
    {
        event Action<ITImeNotification> NotificationPing;
        void RegisterNotification(ITImeNotification notification);
        void UnregisterNotification(ITImeNotification notification);
    }
}