using Enhancements.Clock;
using Enhancements.Settings;
using Enhancements.MiniTweaks;
using Enhancements.SongSkip;

namespace Enhancements
{
    public class PluginConfig
    {
        public virtual ClockConfig clock { get; set; } = new ClockConfig();
        public virtual MiniTweaksConfig minitweaks { get; set; } = new MiniTweaksConfig();
        public virtual VolumeConfig volume { get; set; } = new VolumeConfig();
        public virtual SkipConfig songskip { get; set; } = new SkipConfig();
    }
}
