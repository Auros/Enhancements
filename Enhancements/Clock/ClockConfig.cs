using Enhancements.Settings;
using IPA.Config.Stores.Attributes;

namespace Enhancements.Clock
{
    public class ClockConfig
    {
        public virtual string format { get; set; } = "h:mm tt";
        public virtual bool enabled { get; set; } = true;
        public virtual float fontSize { get; set; } = 2f;
        public virtual Float3 position { get; set; } = new Float3(0f, 2.9f, 2.4f);
        public virtual Float3 rotation { get; set; } = new Float3(-20f, 0f, 0f);
        public virtual Color4 color { get; set; } = new Color4(1f, 1f, 1f, 1f);


        [NonNullable, UseConverter(typeof(ConfigConverters.EnumConverter<BSScene>))]
        public virtual BSScene activeIn { get; set; } = BSScene.Menu & BSScene.Game;
    }
}
