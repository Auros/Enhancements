using Enhancements.Settings;
using UnityEngine;

namespace Enhancements.SongSkip
{
    public class SkipConfig
    {
        public virtual bool skipIntro { get; set; } = true;
        public virtual bool skipOutro { get; set; } = true;
        public virtual float minimumIntroTime { get; set; } = 5f;
        public virtual Color4 notificationColor { get; set; } = new Color4(Color.white);
    }
}
