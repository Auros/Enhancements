using UnityEngine;
using Enhancements.Clock;
using SiaUtil.Visualizers;

namespace Enhancements
{
    public class Enhancements : MonoBehaviour
    {
        public static Enhancements Instance { get; set; }

        public void Awake()
        {
            Instance = this;
            var g = gameObject.AddComponent<Settings.Settings>();
            BeatSaberMarkupLanguage.Settings.BSMLSettings.instance.AddSettingsMenu("[E2] Enhancements", "Enhancements.Views.settings.bsml", g);
        }
        
        public void SetupAll()
        {
            InitializeClock();
        }

        public void OnGame()
        {
            var clock = Plugin.config.Value.clock;
            if (clock.enabled == true)
                ChangeClockState(clock.activeIn.HasFlag(Settings.BSScene.Game) || clock.activeIn == 0);
        }

        public void OnMenu()
        {
            var clock = Plugin.config.Value.clock;
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
            if (Plugin.config.Value.clock.enabled && ClockInstance == null)
            {
                var clock = new GameObject("[E2] - Clock").AddComponent<ClockObject>();
                DontDestroyOnLoad(clock.gameObject);
                clock.text = WorldSpaceMessage.Create("00:00", Vector2.zero);
                clock.text.transform.SetParent(clock.gameObject.transform);
                _ = clock.gameObject;
                ClockInstance = clock;

                ClockInstance.Activate();
                ClockInstance.ConfigSet(Plugin.config.Value.clock);
            }
        }
        #endregion
    }
}
