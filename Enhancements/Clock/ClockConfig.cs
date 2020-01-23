using Enhancements.Settings;

namespace Enhancements.Clock
{
    public class ClockConfig
    {
        public string format { get; set; } = "h:mm tt";
        public bool enabled { get; set; } = true;
        public float fontSize { get; set; } = 2f;
        public Float3 position { get; set; } = new Float3(0f, 2.8f, 2.4f);
        public Float3 rotation { get; set; } = new Float3(0f, 0f, 0f);
        public Color4 color { get; set; } = new Color4(1f, 1f, 1f, 1f);
        public BSScene activeIn { get; set; } = BSScene.Menu & BSScene.Game;
    }
}
