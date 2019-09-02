using CustomUI.Settings;
using CustomUI.UIElements;
using HMUI;
using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Enhancements
{
    public class EnhancementsManager : MonoBehaviour
    {
        public class Settings
        {
            static readonly float[] defaults = { 0f, 2.7f, 2.5f };
            static readonly float[] rotDefaults = { 0f, 0f, 0f };
            static readonly float[] colDefaults = { 1f, 1f, 1f, 1f };
            static readonly float[] kirby = { 1f, .639f,.964f, 1f };

            public class BTSettings
            {
                public static bool Radial { get; set; } = true;
                public static bool Timer { get; set; } = true;
                public static Color RadialColor { get; set; } = Color.white;
                public static int Visualization { get; set; } = 0;
                public static bool Image { get; set; } = true;
                public static bool Audio { get; set; } = true;
                public static float MinimumBreakTime { get; set; } = 5f;
                public static bool Enable { get; set; } = false;
            }
            public class CLSettings
            {
                public static Vector3 ClockPosition { get; set; } = new Vector3(0f, 2.7f, 2.5f);
                public static Vector3 ClockRotation { get; set; } = new Vector3(-30f, 0f, 0f);
                public static float FontSize { get; set; } = 5f;
                public static int TimeFormat { get; set; } = 0;
                public static Color ClockColor { get; set; } = Color.white;
                public static bool Enable { get; set; } = true;
            }
            public class VolumeAssistant
            {
                public static float NoteHit { get; set; } = 1.0f;
                public static float NoteMiss { get; set; } = 1.0f;
                public static float Music { get; set; } = 1.0f;
                public static float MenuBackground { get; set; } = 1.0f;
                public static float PreviewVolume { get; set; } = 1.0f;
            }
            public class SongSkip
            {
                public static bool SkipIntro { get; set; } = true;
                public static bool SkipOutro { get; set; } = false;
                public static float MinimumIntroTime { get; set; } = 5f;
                public static Color Notification { get; set; } = new Color(1f, .639f, .964f);
                public static bool Radial { get; set; } = false;
                public static bool Text { get; set; } = true;
                public static bool Enable { get; set; } = true;
            }

            public static void Load()
            {
                BTSettings.Radial = Config.GetBool(BuildUIString("Breaktime"), "Radial", true, true);
                BTSettings.Timer = Config.GetBool(BuildUIString("Breaktime"), "Timer", true, true);
                BTSettings.RadialColor = ColorBuilderFromConfig("Breaktime", "Radial Color", colDefaults);
                BTSettings.Visualization = Config.GetInt(BuildUIString("Breaktime"), "Visualization", 0, true);
                BTSettings.Image = Config.GetBool(BuildUIString("Breaktime"), "Image", true, true);
                BTSettings.Audio = Config.GetBool(BuildUIString("Breaktime"), "Audio", true, true);
                BTSettings.MinimumBreakTime = Config.GetFloat(BuildUIString("Breaktime"), "Minimum Break Time", 5f, true);
                BTSettings.Enable = ModuleEnabled("Breaktime", false);

                CLSettings.ClockPosition = VectorBuilderFromConfig("Clock", "Position", defaults);
                CLSettings.ClockRotation = VectorBuilderFromConfig("Clock", "Rotation", rotDefaults);
                CLSettings.FontSize = Config.GetFloat(BuildUIString("Clock"), "Font Size", 5f, true);
                CLSettings.TimeFormat = Config.GetInt(BuildUIString("Clock"), "Time Format", 0, true);
                CLSettings.ClockColor = ColorBuilderFromConfig("Clock", "Color", colDefaults);
                CLSettings.Enable = ModuleEnabled("Clock");

                VolumeAssistant.NoteHit = Config.GetFloat(BuildUIString("Volume Assistant"), "Note Hit", 1f, true);
                VolumeAssistant.NoteMiss = Config.GetFloat(BuildUIString("Volume Assistant"), "Note Miss", 1f, true);
                VolumeAssistant.Music = Config.GetFloat(BuildUIString("Volume Assistant"), "Music", 1f, true);
                VolumeAssistant.MenuBackground = Config.GetFloat(BuildUIString("Volume Assistant"), "Menu Background", 1f, true);
                VolumeAssistant.PreviewVolume = Config.GetFloat(BuildUIString("Volume Assistant"), "Song Preview", 1f, true);

                SongSkip.SkipIntro = Config.GetBool(BuildUIString("SongSkip"), "Skip Intro", true, true);
                SongSkip.SkipOutro = Config.GetBool(BuildUIString("SongSkip"), "Skip Outro", true, false);
                SongSkip.Radial = Config.GetBool(BuildUIString("SongSkip"), "Radial", true, false);
                SongSkip.Text = Config.GetBool(BuildUIString("SongSkip"), "Text", true, true);
                SongSkip.Notification = ColorBuilderFromConfig("SongSkip", "Notification Color", kirby);
                SongSkip.MinimumIntroTime = Config.GetFloat(BuildUIString("SongSkip"), "Minimum Intro Time", 5f, true);
                SongSkip.Enable = ModuleEnabled("SongSkip");
            }

            public static void Save()
            {
                Config.SetBool(BuildUIString("Breaktime"), "Radial", BTSettings.Radial);
                Config.SetBool(BuildUIString("Breaktime"), "Timer", BTSettings.Timer);
                Config.SetFloat(BuildUIString("Breaktime"), "Radial Color R", BTSettings.RadialColor.r);
                Config.SetFloat(BuildUIString("Breaktime"), "Radial Color G", BTSettings.RadialColor.g);
                Config.SetFloat(BuildUIString("Breaktime"), "Radial Color B", BTSettings.RadialColor.b);
                Config.SetFloat(BuildUIString("Breaktime"), "Radial Color A", BTSettings.RadialColor.a);
                Config.SetInt(BuildUIString("Breaktime"), "Visualization", BTSettings.Visualization);
                Config.SetBool(BuildUIString("Breaktime"), "Image", BTSettings.Image);
                Config.SetBool(BuildUIString("Breaktime"), "Audio", BTSettings.Audio);
                Config.SetFloat(BuildUIString("Breaktime"), "Minimum Break Time", BTSettings.MinimumBreakTime);
                Config.SetBool(BuildUIString("Breaktime"), "Enable", BTSettings.Enable);

                Config.SetFloat(BuildUIString("Clock"), "Position X", CLSettings.ClockPosition.x);
                Config.SetFloat(BuildUIString("Clock"), "Position Y", CLSettings.ClockPosition.y);
                Config.SetFloat(BuildUIString("Clock"), "Position Z", CLSettings.ClockPosition.z);
                Config.SetFloat(BuildUIString("Clock"), "Rotation X", CLSettings.ClockRotation.x);
                Config.SetFloat(BuildUIString("Clock"), "Rotation Y", CLSettings.ClockRotation.y);
                Config.SetFloat(BuildUIString("Clock"), "Rotation Z", CLSettings.ClockRotation.z);
                Config.SetFloat(BuildUIString("Clock"), "Font Size", CLSettings.FontSize);
                Config.SetInt(BuildUIString("Clock"), "Time Format", CLSettings.TimeFormat);
                Config.SetFloat(BuildUIString("Clock"), "Color R", CLSettings.ClockColor.r);
                Config.SetFloat(BuildUIString("Clock"), "Color G", CLSettings.ClockColor.g);
                Config.SetFloat(BuildUIString("Clock"), "Color B", CLSettings.ClockColor.b);
                Config.SetFloat(BuildUIString("Clock"), "Color A", CLSettings.ClockColor.a);
                Config.SetBool(BuildUIString("Clock"), "Enable", CLSettings.Enable);

                Config.SetFloat(BuildUIString("Volume Assistant"), "Note Hit", VolumeAssistant.NoteHit);
                Config.SetFloat(BuildUIString("Volume Assistant"), "Note Miss", VolumeAssistant.NoteMiss);
                Config.SetFloat(BuildUIString("Volume Assistant"), "Music", VolumeAssistant.Music);
                Config.SetFloat(BuildUIString("Volume Assistant"), "Menu Background", VolumeAssistant.MenuBackground);
                Config.SetFloat(BuildUIString("Volume Assistant"), "Song Preview", VolumeAssistant.PreviewVolume);

                Config.SetBool(BuildUIString("SongSkip"), "Skip Intro", SongSkip.SkipIntro);
                Config.SetBool(BuildUIString("SongSkip"), "Skip Outro", SongSkip.SkipOutro);
                Config.SetBool(BuildUIString("SongSkip"), "Radial", SongSkip.Radial);
                Config.SetBool(BuildUIString("SongSkip"), "Text", SongSkip.Text);
                Config.SetFloat(BuildUIString("SongSkip"), "Notification Color R", SongSkip.Notification.r);
                Config.SetFloat(BuildUIString("SongSkip"), "Notification Color G", SongSkip.Notification.g);
                Config.SetFloat(BuildUIString("SongSkip"), "Notification Color B", SongSkip.Notification.b);
                Config.SetFloat(BuildUIString("SongSkip"), "Notification Color A", SongSkip.Notification.a);
                Config.SetFloat(BuildUIString("SongSkip"), "Minimum Intro Time", SongSkip.MinimumIntroTime);
                Config.SetBool(BuildUIString("SongSkip"), "Enable", SongSkip.Enable);

            }

            private static string BuildUIString(string name)
            {
                return $"Enhancements - {name}";
            }

            private static bool ModuleEnabled(string name, bool defval = true)
            {
                return Config.GetBool(BuildUIString(name), "Enable", defval, true);
            }

            private static Vector3 VectorBuilderFromConfig(string category, string name, float[] defaults)
            {
                var x = Config.GetFloat(BuildUIString(category), name + " X", defaults[0], true);
                var y = Config.GetFloat(BuildUIString(category), name + " Y", defaults[1], true);
                var z = Config.GetFloat(BuildUIString(category), name + " Z", defaults[2], true);

                return new Vector3(x, y, z);
            }

            private static Color ColorBuilderFromConfig(string category, string name, float[] defaults)
            {
                var x = Config.GetFloat(BuildUIString(category), name + " R", defaults[0], true);
                var y = Config.GetFloat(BuildUIString(category), name + " G", defaults[1], true);
                var z = Config.GetFloat(BuildUIString(category), name + " B", defaults[2], true);
                var a = Config.GetFloat(BuildUIString(category), name + " A", defaults[3], true);

                return new Color(x, y, z, a);
            }
        }

        private string _beatSaberDirectory;
        internal static BS_Utils.Utilities.Config Config = new BS_Utils.Utilities.Config("[LM] - Enhancements");
        public static bool noHud = false;
        public SongPreviewPlayer menuPlayer;
        
        public static void Create()
        {
           _ = Instance;
        }

        private static EnhancementsManager _instance;
        public static EnhancementsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject managerGO = new GameObject("Enhancements Manager");
                    _instance = managerGO.AddComponent<EnhancementsManager>();
                    _instance.Init();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        internal void Init()
        {
            SceneManager.activeSceneChanged += OnSceneChange;
            SceneManager.sceneLoaded += OnSceneLoad;


        }

        public string BeatSaberDirectory()
        {
            if (_beatSaberDirectory == null)
                _beatSaberDirectory = Directory.GetCurrentDirectory().Replace('\\', '/');
            return _beatSaberDirectory;
        }

        private void OnSceneChange(Scene prevScene, Scene newScene)
        {
            if (newScene.name == "GameCore")
            {
                noHud = Resources.FindObjectsOfTypeAll<PlayerDataModelSO>().FirstOrDefault().playerData.playerSpecificSettings.noTextsAndHuds;

                if (noHud)
                    Clock.Clock.Instance.UpdateClockState(false);

                if (Settings.BTSettings.Enable && !noHud)
                {
                    Breaktime.BreakTime.Load
                    (
                        Settings.BTSettings.Radial,
                        Settings.BTSettings.Timer,
                        Settings.BTSettings.RadialColor,
                        Settings.BTSettings.Visualization,
                        Settings.BTSettings.Image,
                        Settings.BTSettings.Audio,
                        Settings.BTSettings.MinimumBreakTime
                    );
                }

                if (Settings.SongSkip.Enable)
                {
                    var radial = Settings.SongSkip.Radial;
                    var text = Settings.SongSkip.Text;
                    if (noHud) { radial = false; text = false; }

                    SongSkip.SongSkip.Load
                    (
                        Settings.SongSkip.SkipIntro,
                        Settings.SongSkip.SkipOutro,
                        Settings.SongSkip.MinimumIntroTime,
                        radial,
                        text,
                        Settings.SongSkip.Notification
                    );
                }

            }
            else if (newScene.name == "MenuCore")
            {
                Clock.Clock.Instance.UpdateClockState(Settings.CLSettings.Enable);
            }
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuCore")
            {
                BeatSaberMarkupLanguage.Settings.BSMLSettings.instance.AddSettingsMenu("Enhancements", "Enhancements.Views.enhancementssettings.bsml", PersistentSingleton<UI.EnhancementsSettings>.instance);
                PersistentSingleton<UI.EnhancementsSettings>.instance.SetTexts();

                    if (Clock.Clock._instance == null)
                        Clock.Clock.Instance.Init
                        (
                            Settings.CLSettings.Enable,
                            Settings.CLSettings.ClockPosition,
                            Settings.CLSettings.ClockRotation,
                            Settings.CLSettings.FontSize,
                            Settings.CLSettings.TimeFormat,
                            Settings.CLSettings.ClockColor
                        );

                Clock.Clock.Instance.UpdateClockState(Settings.CLSettings.Enable);
            }
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
            SceneManager.sceneLoaded -= OnSceneLoad;
        }
    }
}
