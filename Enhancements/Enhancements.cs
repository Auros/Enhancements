using UnityEngine;
using Enhancements.Clock;
using SiaUtil.Visualizers;
using Enhancements.MiniTweaks;
using Enhancements.Utilities;

namespace Enhancements
{
    public class Enhancements : MonoBehaviour
    {
        public static Enhancements Instance { get; set; }
        public static float baseGameVolume;
        public static SongPreviewPlayer menuPlayer;

        public void Awake()
        {
            Instance = this;
            var g = gameObject.AddComponent<Settings.Settings>();
            BeatSaberMarkupLanguage.Settings.BSMLSettings.instance.AddSettingsMenu("[E2] Enhancements", "Enhancements.Views.settings.bsml", g);
        }

        public void Update()
        {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Y) && ClockInstance != null)
                ClockInstance.Active = false;
            else if (Input.GetKeyDown(KeyCode.Y) && ClockInstance == null)
            {
                InitializeClock();
            }
#endif
        }

        public void SetupAll()
        {
            InitializeClock();
        }

        public void OnGame()
        {
            var config = Plugin.config;
            var clock = config.clock;
            if (clock.enabled == true)
                ChangeClockState(clock.activeIn.HasFlag(Settings.BSScene.Game) || clock.activeIn == 0);
            if (config.minitweaks.buttonlockenabled)
                new GameObject("[E2] - Button Lock").AddComponent<ButtonLock>();
            if (config.songskip.skipIntro || config.songskip.skipOutro)
                SongSkip.SongSkip.Load(config.songskip.skipIntro, config.songskip.skipOutro, config.songskip.minimumIntroTime, config.songskip.notificationColor.ToColor());
        }

        public void OnMenu()
        {
            var clock = Plugin.config.clock;
            if (clock.enabled == true)
                ChangeClockState(clock.activeIn.HasFlag(Settings.BSScene.Menu) || clock.activeIn == 0);
        }

        #region Clock
        public static ClockObject ClockInstance { get; set; }

        public void ChangeClockState(bool value)
        {
            if (ClockInstance != null)
                ClockInstance.Active = value;
            else if (value == true)
                InitializeClock();
        }

        public void InitializeClock()
        {
            if (Plugin.config.clock.enabled && ClockInstance == null)
            {
                var clock = new GameObject("[E2] - Clock").AddComponent<ClockObject>();
                DontDestroyOnLoad(clock.gameObject);
                clock.text = WorldSpaceMessage.Create("00:00", Vector2.zero);
                clock.text.transform.SetParent(clock.gameObject.transform);
                _ = clock.gameObject;
                ClockInstance = clock;

                ClockInstance.Activate();
                ClockInstance.ConfigSet(Plugin.config.clock);
            }
        }
        #endregion

        internal static void WrapItUpBoysItsTimeToGo()
        {
            if (Instance != null)
            {
                if (ClockInstance != null)
                    ClockInstance.Deactivate();
                Destroy(Instance);
            }
        }
    }
}
