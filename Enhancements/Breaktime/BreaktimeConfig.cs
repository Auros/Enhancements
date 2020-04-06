using Enhancements.Settings;

namespace Enhancements.Breaktime
{
    public class BreaktimeConfig
    {
        public virtual bool Enabled { get; set; } = false;
        public virtual Color4 Color { get; set; } = new Color4(1f, 1f, 1f, 1f);

        public virtual float MinimumBreakTime { get; set; } = 5f;

        public virtual bool ShowImage { get; set; } = true;
        public virtual bool PlayAudio { get; set; } = true;

        public virtual string SelectedProfile { get; set; } = "Default";
    }
}
