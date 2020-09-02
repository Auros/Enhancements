using System;
using Zenject;
using UnityEngine;

namespace Enhancements.Clock
{
    public class ClockController : IClockController, ITickable
    {
        public float CycleLength { get; set; } = 1f;
        public event Action<DateTime> DateUpdated;

        private float _currentCycleTime = 0f;

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public void Tick()
        {
            if (_currentCycleTime >= CycleLength)
            {
                _currentCycleTime = 0;
                Plugin.Log.Info("Time Updated!");
                DateUpdated?.Invoke(GetCurrentTime());
            }
            _currentCycleTime += Time.deltaTime;
        }
    }
}