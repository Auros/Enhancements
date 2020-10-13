using System;

namespace Enhancements.Timers
{
    public interface ITimeNotification
    {
        string Text { get; }
        DateTime Time { get; }

        bool GetUnregisterAfterComplete();
        bool TimeReached();
    }
}