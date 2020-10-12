using UnityEngine;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

namespace Enhancements.Breaktime
{
    public class Profile
    {
        public virtual string Name { get; set; }

        public virtual string ImagePath { get; set; }

        public virtual string AudioPath { get; set; }

        public virtual float ImageOpacity { get; set; } = 1f;

        public bool ShowText { get; set; } = true;

        [UseConverter(typeof(HexColorConverter))]
        public Color TextColor { get; set; } = Color.white;

        [UseConverter(typeof(HexColorConverter))]
        public Color ImageColor { get; set; } = Color.white;


        [UseConverter(typeof(EnumConverter<Animation>))]
        public Animation Animation { get; set; } = Animation.FadeIn;
    }
}