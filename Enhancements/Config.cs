using Enhancements.Breaktime;
using Enhancements.Clock;
using Enhancements.Misc;
using Enhancements.Timers;
using Enhancements.Volume;
using IPA.Config.Stores.Attributes;
using SiraUtil.Converters;
using Version = Hive.Versioning.Version;

namespace Enhancements
{
    public class Config
    {
        internal static Config Value;

        [NonNullable, UseConverter(typeof(VersionConverter))]
        public virtual Version Version { get; set; } = new Version("0.0.0");
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