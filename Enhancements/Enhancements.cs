using UnityEngine;
using Enhancements.Clock;
using Enhancements.MiniTweaks;
using Enhancements.Utilities;
using System.Collections.Generic;
using TMPro;
using System.Linq;

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
            else if (Input.GetKeyDown(KeyCode.U))
            {
                _ = Extensions.ArcadePix;
            }
#endif
        }

        public void SetupAll()
        {
            InitializeClock();
        }

        public void OnGame()
        {
            var model = Resources.FindObjectsOfTypeAll<PlayerDataModel>().FirstOrDefault();
            var config = Plugin.config;
            
            var clock = config.clock;
            if (clock.enabled == true)
                ChangeClockState((clock.activeIn.HasFlag(Settings.BSScene.Game) || clock.activeIn == 0) && !model.playerData.playerSpecificSettings.noTextsAndHuds);
            if (config.minitweaks.buttonlockenabled)
                new GameObject("[E2] - Button Lock").AddComponent<ButtonLock>();
            if (config.breaktime.Enabled)
            {
                var bt = Breaktime.BreaktimeManager.CreateBreaktimeScreen();
                
            }
            
        }

        public void OnMenu()
        {
            var clock = Plugin.config.clock;
            if (clock.enabled == true)
                ChangeClockState(clock.activeIn.HasFlag(Settings.BSScene.Menu) || clock.activeIn == 0);
            if (menuPlayer != null)
                menuPlayer.volume = Plugin.config.volume.MenuBackground;
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
                clock.Text = WorldSpaceMessage.Create("00:00", Vector2.zero);
                Teko = clock.Text.FontX;
                clock.Text.transform.SetParent(clock.gameObject.transform);
                _ = clock.gameObject;
                ClockInstance = clock;

                ClockInstance.Activate();
                ClockInstance.ConfigSet(Plugin.config.clock);
            }
        }

        public static TMP_FontAsset Teko { get; set; }
        public Dictionary<string, TMP_FontAsset> fontPairs = new Dictionary<string, TMP_FontAsset>()
        {
            { "Teko (Default)", Teko },
            { "ArcadePix", Extensions.ArcadePix },
            { "Assistant", Extensions.Assistant },
            { "BLACK METAL", Extensions.BLACKMETAL },
            { "Caveat", Extensions.Caveat },
            { "Comic Sans", Extensions.ComicSans },
            { "LiterallyNatural", Extensions.LiterallyNatural },
            { "Minecraft Enchantment", Extensions.MinecraftEnchantment },
            { "Minecrafter 3", Extensions.Minecrafter3 },
            { "Minecraftia", Extensions.Minecraftia },
            { "PermanentMarker", Extensions.PermanentMarker },
            { "SpicyRice", Extensions.SpicyRice },

        };
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
