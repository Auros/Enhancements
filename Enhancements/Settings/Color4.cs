using UnityEngine;

namespace Enhancements.Settings
{
    public class Color4
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color4() { }
        public Color4(Color4 color4) : this(color4.r, color4.g, color4.b, color4.a) { }
        public Color4(float r, float b, float g, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public Color4(Color color)
        {
            r = color.r;
            g = color.g;
            b = color.b;
            a = color.a;
        }
        public static Color ToColor(Color4 color4) => new Color()
        {
            r = color4.r,
            g = color4.g,
            b = color4.b,
            a = color4.a
        };
    }
}
