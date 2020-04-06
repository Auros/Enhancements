using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;
using Enhancements.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enhancements.Settings
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance;
        
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        [UIAction("#post-parse")]
        public void Parsed()
        {
            
        }

        [UIParams] public BSMLParserParams parserParams;

        [UIValue("breaktime_enabled")] public bool BreaktimeEnabled { get => Plugin.config.breaktime.Enabled; set => Plugin.config.breaktime.Enabled = value; }
        [UIValue("breaktime_time")] public float BreaktimeTime { get => Plugin.config.breaktime.MinimumBreakTime; set => Plugin.config.breaktime.MinimumBreakTime = value; }
        [UIValue("breaktime_image")] public bool BreaktimeImage { get => Plugin.config.breaktime.ShowImage; set => Plugin.config.breaktime.ShowImage = value; }
        [UIValue("breaktime_audio")] public bool BreaktimeAudio { get => Plugin.config.breaktime.PlayAudio; set => Plugin.config.breaktime.PlayAudio = value; }
        [UIValue("breaktime_r")] public float Breaktime_R { get => Plugin.config.breaktime.Color.r; set => Plugin.config.breaktime.Color.r = value; }
        [UIValue("breaktime_g")] public float Breaktime_G { get => Plugin.config.breaktime.Color.g; set => Plugin.config.breaktime.Color.g = value; }
        [UIValue("breaktime_b")] public float Breaktime_B { get => Plugin.config.breaktime.Color.b; set => Plugin.config.breaktime.Color.b = value; }
        [UIValue("breaktime_profile")]
        public string BreaktimeProfile
        {
            get => Plugin.config.breaktime.SelectedProfile;
            set => Plugin.config.breaktime.SelectedProfile = value;
        }

        [UIValue("breaktime_profiles")]
        public List<object> Profiles => BTProfiles().ToList();

        internal object[] BTProfiles()
        {
            List<object> list = new List<object>
            {
                "Default",
                "osu!",
                "Bobbie"
            };
            var pr = Breaktime.BreaktimeManager.GetValidBreaktimePaths();
            for (int i = 0; i < pr.Length; i++)
            {
                list.Add(pr[i].Name);
            }
            return list.ToArray();
        }

        /*[UIAction("breaktime_fprofile")]
        public string FormatProfile(object o)
        {
            //if (o.GetType() == typeof(DirectoryInfo))
                //return (o as DirectoryInfo).Name;
            return o.ToString();
        }*/

        [UIValue("clock-enabled")] public bool ClockEnabled { get => Plugin.config.clock.enabled; set => Plugin.config.clock.enabled = value; }
        [UIValue("clock_size")] public float Clock_Size { get => Plugin.config.clock.fontSize; set => Plugin.config.clock.fontSize = value; }
        [UIValue("clock_mode")] public BSScene Scene { get => Plugin.config.clock.activeIn; set => Plugin.config.clock.activeIn = value; }
        [UIValue("clock_format")] public string Format { get => Plugin.config.clock.format; set => Plugin.config.clock.format = value; }
        [UIValue("clock_font")] public string Font { get => Plugin.config.clock.font; set => Plugin.config.clock.font = value; }

        [UIValue("clock_x")] public float Clock_X { get => Plugin.config.clock.position.x; set => Plugin.config.clock.position.x = value; }
        [UIValue("clock_y")] public float Clock_Y { get => Plugin.config.clock.position.y; set => Plugin.config.clock.position.y = value; }
        [UIValue("clock_z")] public float Clock_Z { get => Plugin.config.clock.position.z; set => Plugin.config.clock.position.z = value; }
        [UIValue("clock_j")] public float Clock_J { get => Plugin.config.clock.rotation.x; set => Plugin.config.clock.rotation.x = value; }
        [UIValue("clock_k")] public float Clock_K { get => Plugin.config.clock.rotation.y; set => Plugin.config.clock.rotation.y = value; }
        [UIValue("clock_l")] public float Clock_L { get => Plugin.config.clock.rotation.z; set => Plugin.config.clock.rotation.z = value; }
        [UIValue("clock_r")] public float Clock_R { get => Plugin.config.clock.color.r; set => Plugin.config.clock.color.r = value; }
        [UIValue("clock_g")] public float Clock_G { get => Plugin.config.clock.color.g; set => Plugin.config.clock.color.g = value; }
        [UIValue("clock_b")] public float Clock_B { get => Plugin.config.clock.color.b; set => Plugin.config.clock.color.b = value; }

        [UIAction("clock_update")] public void UpdateClock(object v) => SharedCoroutineStarter.instance.StartCoroutine(WaitBro());

        [UIAction("clock-ena")] public void CheckEnable(bool v) => SharedCoroutineStarter.instance.StartCoroutine(DudeBro(v));

        public IEnumerator DudeBro(bool v)
        {
            yield return new WaitForSeconds(.03f);
            Enhancements.Instance.ChangeClockState(v);
        }

        public IEnumerator WaitBro()
        {
            yield return new WaitForSeconds(.03f);
            Enhancements.ClockInstance.ConfigSet(Plugin.config.clock);
        }

        [UIAction("clock_resetpos")]
        public void ResetPos()
        {
            Plugin.config.clock.position = new Float3(0f, 2.8f, 2.4f);
            if (Enhancements.ClockInstance != null)
                Enhancements.ClockInstance.ConfigSet(Plugin.config.clock);
            parserParams.EmitEvent("clock_update");
        }

        [UIAction("clock_resetrot")]
        public void ResetRot()
        {
            Plugin.config.clock.rotation = Float3.zero;
            if (Enhancements.ClockInstance != null)
                Enhancements.ClockInstance.ConfigSet(Plugin.config.clock);
            parserParams.EmitEvent("clock_update");
        }

        [UIAction("clock_resetcol")]
        public void ResetCol()
        {
            Plugin.config.clock.color = new Color4(1f, 1f, 1f, 1f);
            if (Enhancements.ClockInstance != null)
                Enhancements.ClockInstance.ConfigSet(Plugin.config.clock);
            parserParams.EmitEvent("clock_update");
        }

        [UIValue("clock_modes")]
        internal List<object> ClockModes = new object[]
        {
            BSScene.Menu,
            BSScene.Game,
            BSScene.Menu & BSScene.Game
        }.ToList();

        [UIAction("clock_fmodes")]
        public string FormatModes(BSScene v)
        {
            if (v == 0)
                return "Both";
            return v.ToString();
        }

        [UIValue("clock_formats")]
        internal List<object> ClockFormat = new object[]
        {
            "h:mm tt",
            "h:mm:ss tt",
            "HH:mm",
            "HH:mm:ss",
            "dddd",
            "dddd, dd MMMM yyyy HH:mm:ss",
            "yyyy MM dd THH:mm:ss.fffffffK"
        }.ToList();

        [UIAction("clock_formatter")]
        public string FormatTimes(string f) => DateTime.Now.ToString(f);

        [UIValue("clock_fonts")]
        internal List<object> ClockFonts = new object[]
        {
            "Teko (Default)",
            "ArcadePix",
            "Assistant",
            "BLACK METAL",
            "Caveat",
            "Comic Sans",
            "LiterallyNatural",
            "Minecraft Enchantment",
            "Minecrafter 3",
            "Minecraftia",
            "PermanentMarker",
            "SpicyRice"
        }.ToList();

        [UIAction("clock_font_formatter")]
        public string FormatFonts(string f) => f.ToString();

        [UIValue("buttonlock-enabled")]
        public bool BLEnabled { get => Plugin.config.minitweaks.buttonlockenabled; set => Plugin.config.minitweaks.buttonlockenabled = value; }
        [UIValue("buttonlock-time")]
        public float BLTime { get => Plugin.config.minitweaks.buttonlocktime; set => Plugin.config.minitweaks.buttonlocktime = value; }


        [UIValue("v-goodcuts")]
        public float VGoodNote { get => Plugin.config.volume.GoodCuts; set => Plugin.config.volume.GoodCuts = value; }
        [UIValue("v-badcuts")]
        public float VBadNote { get => Plugin.config.volume.BadCuts; set => Plugin.config.volume.BadCuts = value; }
        [UIValue("v-music")]
        public float VMusic { get => Plugin.config.volume.Music; set => Plugin.config.volume.Music = value; }
        [UIValue("v-preview")]
        public float VPreview { get => Plugin.config.volume.SongPreview; set => Plugin.config.volume.SongPreview = value; }
        [UIValue("v-background")]
        public float VBackground { get => Plugin.config.volume.MenuBackground; set
            {
                Plugin.config.volume.MenuBackground = value;
                if (Enhancements.menuPlayer != null)
                    Enhancements.menuPlayer.volume = value;
            }
        }
    }
}
