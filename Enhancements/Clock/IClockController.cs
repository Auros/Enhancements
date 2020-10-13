using System;

namespace Enhancements.Clock
{
    public interface IClockController
    {
        event Action<DateTime> DateUpdated;
        DateTime GetCurrentTime();
    }
}