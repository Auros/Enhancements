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
                public static bool Enable { get; set; } = true;
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
                BTSettings.Enable = ModuleEnabled("Breaktime");

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

            private static bool ModuleEnabled(string name)
            {
                return Config.GetBool(BuildUIString(name), "Enable", true, true);
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
                Clock.Clock.Instance.UpdateClockState(true);
            }
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuCore")
            {
                BeatSaberMarkupLanguage.Settings.BSMLSettings.instance.AddSettingsMenu("Enhancements", "Enhancements.Views.enhancementssettings.bsml", PersistentSingleton<UI.EnhancementsSettings>.instance);
                PersistentSingleton<UI.EnhancementsSettings>.instance.SetTexts();
                CreateUI();
                if (Settings.CLSettings.Enable)
                {
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
                }
            }
        }

        internal void CreateUI()
        {
            var menu = SettingsUI.CreateSubMenu("Enhancements");

            CommentMaker(menu, $"<align=\"center\"><b>For your <color=#00ffff>convience</color>...</b></align>");
            CommentMaker(menu, "");
            CommentMaker(menu, $"<size=75%>You <color=\"red\">DO NOT</color> have to press the <color=\"green\">APPLY</color> or <color=\"green\">OK</color> buttons" +
                $" for all settings with the arrows on the left & right. Once you change the values, just press Cancel! Hover over settings to learn more about them. </size>");
            CommentMaker(menu, "");

            //Breaktime Settings

            string bTModuleName = "Breaktime";
            SubMenu btMenu = SubmenuGenerator(menu, bTModuleName, $"Settings for {bTModuleName}");
            MenuPropertyGenerator(btMenu, bTModuleName, MenuType.Bool, "Enable", Settings.BTSettings.Enable, "Enable or Disable the Breaktime Module");
            MenuPropertyGenerator(btMenu, bTModuleName, MenuType.Bool, "Radial", Settings.BTSettings.Radial, "Enable or Disable the radial that appears");
            MenuPropertyGenerator(btMenu, bTModuleName, MenuType.Bool, "Timer", Settings.BTSettings.Timer, "Enable or Disable the timer that appears");
            MenuPropertyGenerator(btMenu, bTModuleName, MenuType.Slider, "Minimum Break Time", Settings.BTSettings.MinimumBreakTime, "The minimum amount of time between blocks for the break to activate.", null, 1f, 20f, .25f);
            SubMenu btColMenu = SubmenuGenerator(btMenu, "Color Settings", $"Adjust the color of the radial.");
            var colbt = CommentMaker(btColMenu, "<align=\"center\">Hover over me to see updated color!</align>");
            colbt.gameObject.AddComponent<BreaktimeCommentColorChanger>();
            MenuPropertyGenerator(btColMenu, bTModuleName, MenuType.Slider, "Radial Color R", Settings.BTSettings.RadialColor.r, "How much red is in it", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(btColMenu, bTModuleName, MenuType.Slider, "Radial Color G", Settings.BTSettings.RadialColor.g, "How much green is in it", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(btColMenu, bTModuleName, MenuType.Slider, "Radial Color B", Settings.BTSettings.RadialColor.b, "How much blue is in it", null, 0f, 1f, .08f, 1f);
            CommentMaker(btColMenu, "<color=#fce428>Breaktime might override transparency!</color>");
            MenuPropertyGenerator(btColMenu, bTModuleName, MenuType.Slider, "Radial Color A", Settings.BTSettings.RadialColor.a, "Transparency", null, 0f, 1f, .08f, 1f);
            CommentMaker(btMenu, $"<align=\"center\"><b><color=#3bffcb><u>Visualization</u></color></b></align>");
            CommentMaker(btMenu, "<size=75%><align=\"center\">When visualization is activated, an image and audio effect will activate.</align></size>");
            CommentMaker(btMenu, "<size=75%><align=\"center\">You can adjust the individual settings for the visualization here. You can</align></size>");
            CommentMaker(btMenu, "<size=75%><align=\"center\">also set the visualization to Custom and put your own files (.wav and .png)</align></size>");
            CommentMaker(btMenu, "<size=75%><align=\"center\">into the folder below. If you have one audio file & image in the folder, it</align></size>");
            CommentMaker(btMenu, "<size=75%><align=\"center\">will always play those. Multiple files will randomize the values!</align></size>");
            MenuPropertyGenerator(btMenu, bTModuleName, MenuType.List, "Visualization", Settings.BTSettings.Visualization, "Changing this will change the audio that plays and the image that appears once a break starts.", Breaktime.BreakTime.animType);
            MenuPropertyGenerator(btMenu, bTModuleName, MenuType.Bool, "Image", Settings.BTSettings.Image, "Enable or Disable the image that appears");
            MenuPropertyGenerator(btMenu, bTModuleName, MenuType.Bool, "Audio", Settings.BTSettings.Audio, "Enable or Disable Breaktime's audio player");
            ClickableCommentMaker(CommentMaker(btMenu, "Click Me To Open Folder!", BeatSaberDirectory() + "/UserData/Lifeline/Breaktime"));


            //Clock Settings

            string clModuleName = "Clock";
            SubMenu clMenu = SubmenuGenerator(menu, clModuleName, $"Settings for {clModuleName}");
            var enable = MenuPropertyGenerator(clMenu, clModuleName, MenuType.Bool, "Enable", Settings.CLSettings.Enable, "Enable or Disable the Clock Module") as BoolViewController;
            var buttonToModify = enable.GetType().BaseType.BaseType.GetField("_decButton", BindingFlags.NonPublic | BindingFlags.Instance);
            var decButton = (Button)buttonToModify.GetValue(enable);
            buttonToModify = enable.GetType().BaseType.BaseType.GetField("_incButton", BindingFlags.NonPublic | BindingFlags.Instance);
            var incButton = (Button)buttonToModify.GetValue(enable);
            incButton.onClick.AddListener(delegate { SetClockAdjuster(true); });
            decButton.onClick.AddListener(delegate { SetClockAdjuster(false); });
            MenuPropertyGenerator(clMenu, clModuleName, MenuType.Slider, "Font Size", Settings.CLSettings.FontSize, "The font size of the text.", null, 1f, 20f, .25f);
            var timeFormat = MenuPropertyGenerator(clMenu, clModuleName, MenuType.List, "Time Format", Settings.CLSettings.TimeFormat, "The format of the date.", Clock.Clock.timeType) as ListViewController;
            var buttonToModify2 = timeFormat.GetType().BaseType.BaseType.GetField("_decButton", BindingFlags.NonPublic | BindingFlags.Instance);
            var decButton2 = (Button)buttonToModify2.GetValue(timeFormat);
            buttonToModify2 = timeFormat.GetType().BaseType.BaseType.GetField("_incButton", BindingFlags.NonPublic | BindingFlags.Instance);
            var incButton2 = (Button)buttonToModify2.GetValue(timeFormat);
            incButton2.onClick.AddListener(delegate { Invoke("SetTimeFormat", .05f); });
            decButton2.onClick.AddListener(delegate { Invoke("SetTimeFormat", .05f); });
            SubMenu clPosMenu = SubmenuGenerator(clMenu, "Position Settings", $"Adjust the position of the clock.");
            CommentMaker(clPosMenu, "<color=red><u><align=\"center\">Warning</align></u></color>");
            CommentMaker(clPosMenu, "<size=75%><align=\"center\">The UI Panel will hide the clock if they overlap, even if the clock</align></size>");
            CommentMaker(clPosMenu, "<size=75%><align=\"center\">is in front. This is a limitation with the text and panel shaders.</align></size>");
            MenuPropertyGenerator(clPosMenu, clModuleName, MenuType.Slider, "Position X", Settings.CLSettings.ClockPosition.x, "(Left/Right)", null, -3f, 3f, .05f, 0f);
            MenuPropertyGenerator(clPosMenu, clModuleName, MenuType.Slider, "Position Y", Settings.CLSettings.ClockPosition.y, "(Up/Down)", null, -1f, 3f, .05f, 0f);
            MenuPropertyGenerator(clPosMenu, clModuleName, MenuType.Slider, "Position Z", Settings.CLSettings.ClockPosition.z, "(Forward/Back)", null, -1f, 10f, .1f, 0f);
            SubMenu clRotMenu = SubmenuGenerator(clMenu, "Rotation Settings", $"Adjust the position of the clock.");
            MenuPropertyGenerator(clRotMenu, clModuleName, MenuType.Slider, "Rotation X", Settings.CLSettings.ClockRotation.x, "(Towards and Behind You)", null, -180f, 180f, 5f, 0f);
            MenuPropertyGenerator(clRotMenu, clModuleName, MenuType.Slider, "Rotation Y", Settings.CLSettings.ClockRotation.y, "(Pretend it's on top of a pole and you're rotating that pole.)", null, -180f, 180f, 5f, 0f);
            MenuPropertyGenerator(clRotMenu, clModuleName, MenuType.Slider, "Rotation Z", Settings.CLSettings.ClockRotation.z, "(Tilt)", null, -180f, 180f, 5f, 0f);
            SubMenu clColMenu = SubmenuGenerator(clMenu, "Color Settings", $"Adjust the color of the clock text.");
            MenuPropertyGenerator(clColMenu, clModuleName, MenuType.Slider, "Color R", Settings.CLSettings.ClockColor.r, "How much red is in it", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(clColMenu, clModuleName, MenuType.Slider, "Color G", Settings.CLSettings.ClockColor.g, "How much green is in it", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(clColMenu, clModuleName, MenuType.Slider, "Color B", Settings.CLSettings.ClockColor.b, "How much blue is in it", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(clColMenu, clModuleName, MenuType.Slider, "Color A", Settings.CLSettings.ClockColor.a, "Transparency", null, 0f, 1f, .08f, 1f);

            // Volume Assistant
            string vaModuleName = "Volume Assistant";
            SubMenu vaMenu = SubmenuGenerator(menu, vaModuleName, $"Settings for the {vaModuleName}");
            CommentMaker(vaMenu, "<color=blue><u><align=\"center\">Volume Assistant</align></u></color>");
            MenuPropertyGenerator(vaMenu, vaModuleName, MenuType.Slider, "Note Hit", Settings.VolumeAssistant.NoteHit, "The sound that plays when you hit the block.", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(vaMenu, vaModuleName, MenuType.Slider, "Note Miss", Settings.VolumeAssistant.NoteMiss, "The sound that plays when you're bad at the game.", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(vaMenu, vaModuleName, MenuType.Slider, "Music", Settings.VolumeAssistant.Music, "The sound of the in game music.", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(vaMenu, vaModuleName, MenuType.Slider, "Menu Background", Settings.VolumeAssistant.MenuBackground, "You know that annoying background music that plays in the menu? Yeah.", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(vaMenu, vaModuleName, MenuType.Slider, "Song Preview", Settings.VolumeAssistant.PreviewVolume, "The volume of the preview music when you click on a song in the menu.", null, 0f, 1f, .08f, 1f);

            // Song Skip Settings
            string ssModuleName = "SongSkip";
            SubMenu ssMenu = SubmenuGenerator(menu, ssModuleName, $"Settings for {ssModuleName}");
            MenuPropertyGenerator(ssMenu, ssModuleName, MenuType.Bool, "Enable", Settings.SongSkip.Enable, "Enable or Disable SongSkip");
            MenuPropertyGenerator(ssMenu, ssModuleName, MenuType.Bool, "Skip Intro", Settings.SongSkip.SkipIntro, "This controls if the song skips the intro");
            MenuPropertyGenerator(ssMenu, ssModuleName, MenuType.Bool, "Skip Outro", Settings.SongSkip.SkipOutro, "This controls if the song skips the outro");
            MenuPropertyGenerator(ssMenu, ssModuleName, MenuType.Bool, "Radial", Settings.SongSkip.Radial, "Enable or Disable the radial that appears");
            MenuPropertyGenerator(ssMenu, ssModuleName, MenuType.Bool, "Text", Settings.SongSkip.Text, "Enable or Disable the text that appears.");
            MenuPropertyGenerator(ssMenu, ssModuleName, MenuType.Slider, "Minimum Intro Time", Settings.SongSkip.MinimumIntroTime, "The minimum amount of time the intro has to be.", null, 3f, 10f, .49f);
            SubMenu ssColMenu = SubmenuGenerator(ssMenu, "Color Settings", $"Adjust the color of the radial.");
            var colss = CommentMaker(ssColMenu, "<align=\"center\">Hover over me to see updated color!</align>");
            colss.gameObject.AddComponent<SongSkipCommentColorChanger>();
            MenuPropertyGenerator(ssColMenu, ssModuleName, MenuType.Slider, "Notification Color R", Settings.SongSkip.Notification.r, "How much red is in it", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(ssColMenu, ssModuleName, MenuType.Slider, "Notification Color G", Settings.SongSkip.Notification.g, "How much green is in it", null, 0f, 1f, .08f, 1f);
            MenuPropertyGenerator(ssColMenu, ssModuleName, MenuType.Slider, "Notification Color B", Settings.SongSkip.Notification.b, "How much blue is in it", null, 0f, 1f, .08f, 1f);
            CommentMaker(ssColMenu, "<color=#fce428>Song Skip might override transparency!</color>");
            MenuPropertyGenerator(ssColMenu, ssModuleName, MenuType.Slider, "Notification Color A", Settings.SongSkip.Notification.a, "Transparency", null, 0f, 1f, .08f, 1f);
        }

        private void SetClockAdjuster(bool value)
        {
            Settings.CLSettings.Enable = value;
            Clock.Clock.Instance.UpdateClockState(value);
        }

        private void SetTimeFormat()
        {
            Clock.Clock.Instance.UpdateFormat(Settings.CLSettings.TimeFormat);
        }


        private object MenuPropertyGenerator(SubMenu menu, string menuName, MenuType type, string name, object setting, string hint = "", List<string> array = null, float min = 0, float max = 10, float inc = 1, float def = 5f)
        {
            if (type == MenuType.Bool)
            {
                bool property = (bool)setting;

                var tmenu = menu.AddBool(name, hint);
                tmenu.applyImmediately = true;

                if (menuName.Contains("Break"))
                {
                    if (name == "Radial")
                    {
                        tmenu.GetValue += delegate { return Settings.BTSettings.Radial; };
                        tmenu.SetValue += delegate (bool value) { Settings.BTSettings.Radial = value; };
                    }
                    else if (name == "Timer")
                    {
                        tmenu.GetValue += delegate { return Settings.BTSettings.Timer; };
                        tmenu.SetValue += delegate (bool value) { Settings.BTSettings.Timer = value; };
                    }
                    else if (name == "Image")
                    {
                        tmenu.GetValue += delegate { return Settings.BTSettings.Image; };
                        tmenu.SetValue += delegate (bool value) { Settings.BTSettings.Image = value; };
                    }
                    else if (name == "Audio")
                    {
                        tmenu.GetValue += delegate { return Settings.BTSettings.Audio; };
                        tmenu.SetValue += delegate (bool value) { Settings.BTSettings.Audio = value; };
                    }
                    else if (name == "Enable")
                    {
                        tmenu.GetValue += delegate { return Settings.BTSettings.Enable; };
                        tmenu.SetValue += delegate (bool value) { Settings.BTSettings.Enable = value; };
                    }
                }
                else if (menuName.Contains("Clock"))
                {
                    if (name == "Enable")
                    {
                        tmenu.GetValue += delegate { return Settings.CLSettings.Enable; };
                        tmenu.SetValue += delegate (bool value) { Settings.CLSettings.Enable = value; };
                    }
                }
                else if (menuName.Contains("SongSkip"))
                {
                    if (name == "Radial")
                    {
                        tmenu.GetValue += delegate { return Settings.SongSkip.Radial; };
                        tmenu.SetValue += delegate (bool value) { Settings.SongSkip.Radial = value; };
                    }
                    else if (name == "Text")
                    {
                        tmenu.GetValue += delegate { return Settings.SongSkip.Text; };
                        tmenu.SetValue += delegate (bool value) { Settings.SongSkip.Text = value; };
                    }
                    else if (name == "Skip Intro")
                    {
                        tmenu.GetValue += delegate { return Settings.SongSkip.SkipIntro; };
                        tmenu.SetValue += delegate (bool value) { Settings.SongSkip.SkipIntro = value; };
                    }
                    else if (name == "Skip Outro")
                    {
                        tmenu.GetValue += delegate { return Settings.SongSkip.SkipOutro; };
                        tmenu.SetValue += delegate (bool value) { Settings.SongSkip.SkipOutro = value; };
                    }
                    else if (name == "Enable")
                    {
                        tmenu.GetValue += delegate { return Settings.SongSkip.Enable; };
                        tmenu.SetValue += delegate (bool value) { Settings.SongSkip.Enable = value; };
                    }
                }
                return tmenu;
            }
            if (type == MenuType.Slider)
            {
                CustomSlider slider = null;
                float property = (float)setting;

                //HELLA JANK LMAOOOOOO, I don't blame you if you don't understand this.
                var tmenu = menu.AddSlider(name, hint, min, max, inc, false);
                tmenu.GetValue += delegate {
                    slider = tmenu.GetPrivateField<CustomSlider>("_sliderInst");
                    float storeValue = property;
                    if (slider != null)
                    {
                        slider.Scrollbar.valueDidChangeEvent += delegate (RangeValuesTextSlider slide, float value)
                        {
                            storeValue = property;
                            slider.SetCurrentValueFromPercentage(value);
                            property = (float)Math.Round(slider.CurrentValue, 1);


                            if (menuName == "Volume Assistant")
                            {
                                if (name.Contains("Hit"))
                                    Settings.VolumeAssistant.NoteHit = property;
                                else if (name.Contains("Miss"))
                                    Settings.VolumeAssistant.NoteMiss = property;
                                else if (name.Contains("Music"))
                                    Settings.VolumeAssistant.Music = property;
                                else if (name.Contains("Menu"))
                                {
                                    Settings.VolumeAssistant.MenuBackground = property;
                                    if (menuPlayer != null)
                                        menuPlayer.volume = Settings.VolumeAssistant.MenuBackground;
                                }
                                else if (name.Contains("Preview"))
                                    Settings.VolumeAssistant.PreviewVolume = property;
                            }
                            else if (Settings.CLSettings.Enable && menuName == "Clock")
                            {
                                var cp = Settings.CLSettings.ClockPosition;
                                var cr = Settings.CLSettings.ClockRotation;
                                var cc = Settings.CLSettings.ClockColor;

                                if (name.Contains("Position X"))
                                {
                                    Settings.CLSettings.ClockPosition = new Vector3(property, cp.y, cp.z);
                                    Clock.Clock.Instance.UpdatePos(Settings.CLSettings.ClockPosition);
                                }
                                else if (name.Contains("Position Y"))
                                {
                                    Settings.CLSettings.ClockPosition = new Vector3(cp.x, property, cp.z);
                                    Clock.Clock.Instance.UpdatePos(Settings.CLSettings.ClockPosition);
                                }
                                else if (name.Contains("Position Z"))
                                {
                                    Settings.CLSettings.ClockPosition = new Vector3(cp.x, cp.y, property);
                                    Clock.Clock.Instance.UpdatePos(Settings.CLSettings.ClockPosition);
                                }
                                else if (name.Contains("Rotation X"))
                                {
                                    Settings.CLSettings.ClockRotation = new Vector3(property, cr.y, cr.z);
                                    Clock.Clock.Instance.UpdateRot(Settings.CLSettings.ClockRotation);
                                }
                                else if (name.Contains("Rotation Y"))
                                {
                                    Settings.CLSettings.ClockRotation = new Vector3(cr.x, property, cr.z);
                                    Clock.Clock.Instance.UpdateRot(Settings.CLSettings.ClockRotation);
                                }
                                else if (name.Contains("Rotation Z"))
                                {
                                    Settings.CLSettings.ClockRotation = new Vector3(cr.x, cr.y, property);
                                    Clock.Clock.Instance.UpdateRot(Settings.CLSettings.ClockRotation);
                                }
                                else if (name.Contains("Font Size"))
                                {
                                    Settings.CLSettings.FontSize = property;
                                    Clock.Clock.Instance.UpdateSize(Settings.CLSettings.FontSize);
                                }
                                else if (name.Contains("Color R"))
                                {
                                    Settings.CLSettings.ClockColor = new Color(property, cc.g, cc.b, cc.a);
                                    Clock.Clock.Instance.UpdateColor(Settings.CLSettings.ClockColor);
                                }
                                else if (name.Contains("Color G"))
                                {
                                    Settings.CLSettings.ClockColor = new Color(cc.r, property, cc.b, cc.a);
                                    Clock.Clock.Instance.UpdateColor(Settings.CLSettings.ClockColor);
                                }
                                else if (name.Contains("Color B"))
                                {
                                    Settings.CLSettings.ClockColor = new Color(cc.r, cc.g, property, cc.a);
                                    Clock.Clock.Instance.UpdateColor(Settings.CLSettings.ClockColor);
                                }
                                else if (name.Contains("Color A"))
                                {
                                    Settings.CLSettings.ClockColor = new Color(cc.r, cc.g, cc.b, property);
                                    Clock.Clock.Instance.UpdateColor(Settings.CLSettings.ClockColor);
                                }
                            }
                            else if (Settings.BTSettings.Enable && menuName == "Breaktime")
                            {
                                var cc = Settings.BTSettings.RadialColor;

                                if (name.Contains("Minimum Break Time"))
                                {
                                    Settings.BTSettings.MinimumBreakTime = property;
                                }
                                else if (name.Contains("Color R"))
                                {
                                    Settings.BTSettings.RadialColor = new Color(property, cc.g, cc.b, cc.a);
                                }
                                else if (name.Contains("Color G"))
                                {
                                    Settings.BTSettings.RadialColor = new Color(cc.r, property, cc.b, cc.a);
                                }
                                else if (name.Contains("Color B"))
                                {
                                    Settings.BTSettings.RadialColor = new Color(cc.r, cc.g, property, cc.a);
                                }
                                else if (name.Contains("Color A"))
                                {
                                    Settings.BTSettings.RadialColor = new Color(cc.r, cc.g, cc.b, property);
                                }
                            }
                            else if (Settings.SongSkip.Enable && menuName == "SongSkip")
                            {
                                var cc = Settings.SongSkip.Notification;

                                if (name.Contains("Minimum Intro Time"))
                                {
                                    Settings.SongSkip.MinimumIntroTime = property;
                                }
                                else if (name.Contains("Color R"))
                                {
                                    Settings.SongSkip.Notification = new Color(property, cc.g, cc.b, cc.a);
                                }
                                else if (name.Contains("Color G"))
                                {
                                    Settings.SongSkip.Notification = new Color(cc.r, property, cc.b, cc.a);
                                }
                                else if (name.Contains("Color B"))
                                {
                                    Settings.SongSkip.Notification = new Color(cc.r, cc.g, property, cc.a);
                                }
                                else if (name.Contains("Color A"))
                                {
                                    Settings.SongSkip.Notification = new Color(cc.r, cc.g, cc.b, property);
                                }
                            }
                        };
                        return storeValue;
                    }
                    else
                        return property;
                };
                //JANKINESS HAS ENDED
            }
            if (type == MenuType.List)
            {
                if (array == null)
                    return null;

                int property = (int)setting;

                float[] arrayFloat = new float[array.Count];

                for (int i = 0; i < array.Count; i++) arrayFloat[i] = i;

                var tMenu = menu.AddList(name, arrayFloat);
                tMenu.applyImmediately = true;

                if (name.Contains("Time Format"))
                {
                    tMenu.GetValue += delegate { return Settings.CLSettings.TimeFormat; };
                    tMenu.SetValue += delegate (float value) { Settings.CLSettings.TimeFormat = (int)value; };
                    tMenu.FormatValue += delegate (float value) { return DateTime.Now.ToString(array[(int)value]); };
                }
                else if (name.Contains("Visualization"))
                {
                    tMenu.GetValue += delegate { return Settings.BTSettings.Visualization; };
                    tMenu.SetValue += delegate (float value) { Settings.BTSettings.Visualization = (int)value; };
                    tMenu.FormatValue += delegate (float value) { return array[(int)value]; };
                }
                return tMenu;
            }
            return null;
        }

        private static string BuildUIString(string name)
        {
            return $"Enhancements - {name}";
        }

        /// <summary>
        /// Comment Maker derived from BeatSaberTweaks
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private BoolViewController CommentMaker(SubMenu menu, string text, string hint = "")
        {
            var comment = menu.AddBool(text, hint);
            comment.GetValue += delegate { return false; };
            comment.SetValue += delegate (bool value) { };
            try
            {
                var buttonToDisable = comment.GetType().BaseType.BaseType.GetField("_decButton", BindingFlags.NonPublic | BindingFlags.Instance);
                var decButton = (MonoBehaviour)buttonToDisable.GetValue(comment);
                buttonToDisable = comment.GetType().BaseType.BaseType.GetField("_incButton", BindingFlags.NonPublic | BindingFlags.Instance);
                var incButton = (MonoBehaviour)buttonToDisable.GetValue(comment);
                decButton.gameObject.SetActive(false);
                incButton.gameObject.SetActive(false);
                var textToDisable = comment.GetType().BaseType.BaseType.GetField("_text", BindingFlags.NonPublic | BindingFlags.Instance);
                var uselessText = (MonoBehaviour)textToDisable.GetValue(comment);
                uselessText.gameObject.SetActive(false);
            }
            catch
            {

            }

            return comment;
        }

        private SubMenu SubmenuGenerator(SubMenu menu, string name, string hint)
        {
            return menu.AddSubMenu(name, hint, true);
        }

        private void ClickableCommentMaker(BoolViewController bVC)
        {
            bVC.gameObject.AddComponent<ClickableComment>();
        }


        private enum MenuType
        {
            Bool,
            String,
            List,
            Slider
        }


        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
            SceneManager.sceneLoaded -= OnSceneLoad;
        }

        private class ClickableComment : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
        {
            public void OnPointerClick(PointerEventData eventData)
            {
                if (!Directory.Exists(Environment.CurrentDirectory.Replace('\\', '/') + "/UserData/Lifeline/Breaktime"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory.Replace('\\', '/') + "/UserData/Lifeline/Breaktime");
                }

                ShowExplorer(Instance.BeatSaberDirectory() + "/UserData/Lifeline/Breaktime/");
            }

            public void OnPointerEnter(PointerEventData eventData)
            {
                var controller = gameObject.GetComponent<BoolViewController>();
                var text = controller.GetComponentInChildren<TMP_Text>();
                text.color = new Color(1f, .7f, .4f);
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                var controller = gameObject.GetComponent<BoolViewController>();
                var text = controller.GetComponentInChildren<TMP_Text>();
                text.color = Color.white;
            }

            public void ShowExplorer(string itemPath)
            {
                itemPath = itemPath.Replace(@"/", @"\");
                System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
            }
        }

        private class BreaktimeCommentColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
        {
            public void OnPointerEnter(PointerEventData eventData)
            {
                var controller = gameObject.GetComponent<BoolViewController>();
                var text = controller.GetComponentInChildren<TMP_Text>();
                text.color = Settings.BTSettings.RadialColor;
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                var controller = gameObject.GetComponent<BoolViewController>();
                var text = controller.GetComponentInChildren<TMP_Text>();
                text.color = Settings.BTSettings.RadialColor;
            }
        }

        private class SongSkipCommentColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
        {
            public void OnPointerEnter(PointerEventData eventData)
            {
                var controller = gameObject.GetComponent<BoolViewController>();
                var text = controller.GetComponentInChildren<TMP_Text>();
                text.color = Settings.SongSkip.Notification;
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                var controller = gameObject.GetComponent<BoolViewController>();
                var text = controller.GetComponentInChildren<TMP_Text>();
                text.color = Settings.SongSkip.Notification;
            }
        }
    }
}
