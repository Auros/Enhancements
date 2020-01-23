using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
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

        [UIParams] public BSMLParserParams parserParams;

        [UIValue("clock-enabled")] public bool Enabled { get => Plugin.config.Value.clock.enabled; set => Plugin.config.Value.clock.enabled = value; }
        [UIValue("clock_size")] public float Clock_Size { get => Plugin.config.Value.clock.fontSize; set => Plugin.config.Value.clock.fontSize = value; }
        [UIValue("clock_mode")] public BSScene Scene { get => Plugin.config.Value.clock.activeIn; set => Plugin.config.Value.clock.activeIn = value; }
        [UIValue("clock_format")] public string Format { get => Plugin.config.Value.clock.format; set => Plugin.config.Value.clock.format = value; }

        [UIValue("clock_x")] public float Clock_X { get => Plugin.config.Value.clock.position.x; set => Plugin.config.Value.clock.position.x = value; }
        [UIValue("clock_y")] public float Clock_Y { get => Plugin.config.Value.clock.position.y; set => Plugin.config.Value.clock.position.y = value; }
        [UIValue("clock_z")] public float Clock_Z { get => Plugin.config.Value.clock.position.z; set => Plugin.config.Value.clock.position.z = value; }
        [UIValue("clock_j")] public float Clock_J { get => Plugin.config.Value.clock.rotation.x; set => Plugin.config.Value.clock.rotation.x = value; }
        [UIValue("clock_k")] public float Clock_K { get => Plugin.config.Value.clock.rotation.y; set => Plugin.config.Value.clock.rotation.y = value; }
        [UIValue("clock_l")] public float Clock_L { get => Plugin.config.Value.clock.rotation.z; set => Plugin.config.Value.clock.rotation.z = value; }
        [UIValue("clock_r")] public float Clock_R { get => Plugin.config.Value.clock.color.r; set => Plugin.config.Value.clock.color.r = value; }
        [UIValue("clock_g")] public float Clock_G { get => Plugin.config.Value.clock.color.g; set => Plugin.config.Value.clock.color.g = value; }
        [UIValue("clock_b")] public float Clock_B { get => Plugin.config.Value.clock.color.b; set => Plugin.config.Value.clock.color.b = value; }

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
            Enhancements.ClockInstance.ConfigSet(Plugin.config.Value.clock);
        }

        [UIAction("clock_resetpos")] public void Reset()
        {
            Plugin.config.Value.clock.position = new Float3(0f, 2.8f, 2.4f);
            Plugin.config.Value.clock.rotation = Float3.zero;
            Plugin.config.Value.clock.color = new Color4(1f, 1f, 1f, 1f);
            if (Enhancements.ClockInstance != null)
                Enhancements.ClockInstance.ConfigSet(Plugin.config.Value.clock);
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
        
    }
}
