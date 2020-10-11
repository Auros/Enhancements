using System;

namespace Enhancements.Timers
{
    public class GenericNotification : ITimeNotification
    {
        public GenericNotification(string text, DateTime time)
        {
            Text = text;
            Time = time;
        }

        public string Text { get; }

        public DateTime Time { get; }

        public bool GetUnregisterAfterComplete()
        {
            return true;
        }

        public bool TimeReached()
        {
            return DateTime.Now > Time;
        }
    }
}