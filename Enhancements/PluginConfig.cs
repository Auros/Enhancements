using Enhancements.Clock;
using Enhancements.Settings;
using Enhancements.Breaktime;
using Enhancements.MiniTweaks;

namespace Enhancements
{
    public class PluginConfig
    {
        public virtual ClockConfig clock { get; set; } = new ClockConfig();
        public virtual MiniTweaksConfig minitweaks { get; set; } = new MiniTweaksConfig();
        public virtual VolumeConfig volume { get; set; } = new VolumeConfig();
        public virtual BreaktimeConfig breaktime { get; set; } = new BreaktimeConfig();
    }
}
