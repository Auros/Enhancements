using Enhancements.Clock;
using IPA.Config.Stores.Attributes;

namespace Enhancements
{
    public class Config
    {
        [NonNullable]
        public virtual ClockSettings Clock { get; set; } = new ClockSettings();

        public virtual void Changed()
        {
            Clock.MarkDirty();
        }
    }
}