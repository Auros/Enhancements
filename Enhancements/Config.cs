using System;
using UnityEngine;
using IPA.Config.Data;
using IPA.Config.Stores;
using Enhancements.Misc;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.Volume;
using IPA.Config.Stores.Attributes;
using Enhancements.Breaktime;

namespace Enhancements
{
    public class Config
    {
        [NonNullable]
        public virtual ClockSettings Clock { get; set; } = new ClockSettings();
        [NonNullable]
        public virtual TimerSettings Timer { get; set; } = new TimerSettings();
        [NonNullable]
        public virtual BreaktimeSettings Breaktime { get; set; } = new BreaktimeSettings();
        [NonNullable]
        public virtual VolumeSettings Volume { get; set; } = new VolumeSettings();
        [NonNullable]
        public virtual MiscSettings Misc { get; set; } = new MiscSettings();
        [NonNullable]
        public virtual OptidraSettings Optidra { get; set; } = new OptidraSettings();

        public virtual void Changed()
        {
            Clock.MarkDirty();
        }
    }
}