using System.Collections.Generic;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

namespace Enhancements.Breaktime
{
    public class BreaktimeSettings
    {
        public virtual bool Enabled { get; set; }

        [UseConverter(typeof(EnumConverter<RandomizeMode>))]
        public virtual RandomizeMode RandomizeMode { get; set; }

        public virtual float MinimumBreakTime { get; set; } = 5f;

        [NonNullable]
        public virtual string SelectedProfile { get; set; } = "Default";

        [NonNullable]
        [UseConverter(typeof(ListConverter<Profile>))]
        public virtual List<Profile> Profiles { get; set; } = new List<Profile>();

        public virtual bool FirstLaunch { get; set; } = true;
    }
}