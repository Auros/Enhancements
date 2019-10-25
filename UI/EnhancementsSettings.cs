using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enhancements.UI
{
    public class EnhancementsSettings : PersistentSingleton<EnhancementsSettings>
    {
        [UIComponent("auros")]
        private RawImage auros;

        [UIComponent("range")]
        private RawImage range;

        [UIComponent("kyle")]
        private RawImage kyle;

        [UIComponent("taz")]
        private RawImage taz;


        [UIComponent("firstnotice")]
        private TextMeshProUGUI firstcomment;

        [UIComponent("applyimmediatenotice")]
        private TextMeshProUGUI applycomment;

        [UIComponent("buttonlocktext")]
        private TextMeshProUGUI btnlck;

        [UIComponent("visualizationhelp")]
        private TextMeshProUGUI visualizationhelp;

        public void SetTexts()
        {
            firstcomment.text = $"<align=\"center\"><b>For your <color=#00ffff>convience</color>...</b></align>";
            applycomment.text = $"<size=65%>You <color=\"red\">DO NOT</color> have to press the <color=\"green\">APPLY</color> or <color=\"green\">OK</color> buttons for all settings involved with Enhancements. Once you change the values, just press Cancel! Hover over settings to learn more about them. </size>";
            visualizationhelp.text = $"<align=\"center\"><b><color=#3bffcb><u>Visualization</u></color></b></align>\n" +
                $"<size=45%><align=\"center\">" +
                $"When visualization is activated, an image and audio effect will activate. You can adjust the individual settings for the visualization here. You can " +
                $"also set the visualization to Custom and put your own files (.wav and .png) into the folder below. If you have one audio file & image in the folder, it " +
                $"will always play those. Multiple files will randomize the values!</align></size>";
            btnlck.text = $"<align=\"center\">Button Lock will temporarily disable the MENU, RESTART, and CONTINUE buttons in the pause menu so you don't accidentally pause and immediately press them. The time is how long" +
                $"the buttons are deactivated.</align>";


            ChangeClockSetting();

            var element = auros.gameObject.AddComponent<AspectRatioFitter>();
            element.aspectRatio = 1f;
            element.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

            var element1 = range.gameObject.AddComponent<AspectRatioFitter>();
            element1.aspectRatio = 1f;
            element1.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

            var element2 = kyle.gameObject.AddComponent<AspectRatioFitter>();
            element2.aspectRatio = 1f;
            element2.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

            var element3 = taz.gameObject.AddComponent<AspectRatioFitter>();
            element3.aspectRatio = 1f;
            element3.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

            auros.texture = CustomUI.Utilities.UIUtilities.LoadTextureFromResources("Enhancements.Resources.auros.png");
            range.texture = CustomUI.Utilities.UIUtilities.LoadTextureFromResources("Enhancements.Resources.range.png");
            kyle.texture = CustomUI.Utilities.UIUtilities.LoadTextureFromResources("Enhancements.Resources.kyle.png");
            taz.texture = CustomUI.Utilities.UIUtilities.LoadTextureFromResources("Enhancements.Resources.taz.png");

            auros.texture.wrapMode = TextureWrapMode.Clamp;
            range.texture.wrapMode = TextureWrapMode.Clamp;
            kyle.texture.wrapMode = TextureWrapMode.Clamp;
            taz.texture.wrapMode = TextureWrapMode.Clamp;
        }

        [UIAction("openfolder")]
        private void OpenFolder()
        {
            if (!Directory.Exists(Environment.CurrentDirectory.Replace('\\', '/') + "/UserData/Lifeline/Breaktime"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory.Replace('\\', '/') + "/UserData/Lifeline/Breaktime");
            }

            ShowExplorer(EnhancementsManager.Instance.BeatSaberDirectory() + "/UserData/Lifeline/Breaktime/");
        }

        public void ShowExplorer(string itemPath)
        {
            itemPath = itemPath.Replace(@"/", @"\");
            System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
        }

        //BREAKTIME

        [UIValue("breaktime_enable")]
        private bool breaktime_enable = EnhancementsManager.Settings.BTSettings.Enable;

        [UIAction("breaktime_enable")]
        private void Apply_BreaktimeEnable(bool value)
        {
            EnhancementsManager.Settings.BTSettings.Enable = value;
        }

        [UIValue("breaktime_radial")]
        private bool breaktime_radial = EnhancementsManager.Settings.BTSettings.Radial;

        [UIAction("breaktime_radial")]
        private void Apply_BreaktimeRadial(bool value)
        {
            EnhancementsManager.Settings.BTSettings.Radial = value;
        }

        [UIValue("breaktime_timer")]
        private bool breaktime_timer = EnhancementsManager.Settings.BTSettings.Timer;

        [UIAction("breaktime_timer")]
        private void Apply_BreaktimeTimer(bool value)
        {
            EnhancementsManager.Settings.BTSettings.Timer = value;
        }

        [UIValue("breaktime_min")]
        private float breaktime_min = EnhancementsManager.Settings.BTSettings.MinimumBreakTime;

        [UIAction("breaktime_min")]
        private void Apply_BreaktimeMin(float value)
        {
            EnhancementsManager.Settings.BTSettings.MinimumBreakTime = value;
        }

        //BT Colors

        [UIValue("breaktime_r")]
        private float breaktime_r = EnhancementsManager.Settings.BTSettings.RadialColor.r;
        
        [UIAction("breaktime_r")]
        private void Apply_BreaktimeR(float value)
        {
            var col = EnhancementsManager.Settings.BTSettings.RadialColor;
            EnhancementsManager.Settings.BTSettings.RadialColor = new Color(value, col.g, col.b, col.a);
        }

        [UIValue("breaktime_g")]
        private float breaktime_g = EnhancementsManager.Settings.BTSettings.RadialColor.g;

        [UIAction("breaktime_g")]
        private void Apply_BreaktimeG(float value)
        {
            var col = EnhancementsManager.Settings.BTSettings.RadialColor;
            EnhancementsManager.Settings.BTSettings.RadialColor = new Color(col.r, value, col.b, col.a);
        }

        [UIValue("breaktime_b")]
        private float breaktime_b = EnhancementsManager.Settings.BTSettings.RadialColor.b;

        [UIAction("breaktime_b")]
        private void Apply_BreaktimeB(float value)
        {
            var col = EnhancementsManager.Settings.BTSettings.RadialColor;
            EnhancementsManager.Settings.BTSettings.RadialColor = new Color(col.r, col.g, value, col.a);
        }

        [UIValue("breaktime_a")]
        private float breaktime_a = EnhancementsManager.Settings.BTSettings.RadialColor.a;

        [UIAction("breaktime_a")]
        private void Apply_BreaktimeA(float value)
        {
            var col = EnhancementsManager.Settings.BTSettings.RadialColor;
            EnhancementsManager.Settings.BTSettings.RadialColor = new Color(col.r, col.g, col.b, value);
        }

        [UIValue("vis-options")]
        private List<object> options = Breaktime.BreakTime.animType;

        [UIValue("vis-choice")]
        private string choice = (string)Breaktime.BreakTime.animType[EnhancementsManager.Settings.BTSettings.Visualization];

        [UIAction("breaktime_visualization")]
        private void Apply_BreaktimeVis(object obj)
        {
            int index = options.FindIndex(a => a == obj);

            EnhancementsManager.Settings.BTSettings.Visualization = index;
        }

        [UIValue("breaktime_image")]
        private bool breaktime_image = EnhancementsManager.Settings.BTSettings.Image;

        [UIAction("breaktime_image")]
        private void Apply_Breaktimeimage(bool value)
        {
            EnhancementsManager.Settings.BTSettings.Image = value;
        }

        [UIValue("breaktime_audio")]
        private bool breaktime_audio = EnhancementsManager.Settings.BTSettings.Audio;

        [UIAction("breaktime_audio")]
        private void Apply_Breaktimeaudio(bool value)
        {
            EnhancementsManager.Settings.BTSettings.Audio = value;
        }




        [UIValue("clock_r")]
        private float clock_r = EnhancementsManager.Settings.CLSettings.ClockColor.r;

        [UIAction("clock_r")]
        private void Apply_ClockR(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockColor;
            EnhancementsManager.Settings.CLSettings.ClockColor = new Color(value, col.g, col.b, col.a);
            Clock.Clock.Instance.UpdateColor(EnhancementsManager.Settings.CLSettings.ClockColor);
        }

        [UIValue("clock_g")]
        private float clock_g = EnhancementsManager.Settings.CLSettings.ClockColor.g;

        [UIAction("clock_g")]
        private void Apply_ClockG(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockColor;
            EnhancementsManager.Settings.CLSettings.ClockColor = new Color(col.r, value, col.b, col.a);
            Clock.Clock.Instance.UpdateColor(EnhancementsManager.Settings.CLSettings.ClockColor);
        }

        [UIValue("clock_b")]
        private float clock_b = EnhancementsManager.Settings.CLSettings.ClockColor.b;

        [UIAction("clock_b")]
        private void Apply_ClockB(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockColor;
            EnhancementsManager.Settings.CLSettings.ClockColor = new Color(col.r, col.g, value, col.a);
            Clock.Clock.Instance.UpdateColor(EnhancementsManager.Settings.CLSettings.ClockColor);
        }

        [UIValue("clock_a")]
        private float clock_a = EnhancementsManager.Settings.CLSettings.ClockColor.a;

        [UIAction("clock_a")]
        private void Apply_ClockA(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockColor;
            EnhancementsManager.Settings.CLSettings.ClockColor = new Color(col.r, col.g, col.b, value);
            Clock.Clock.Instance.UpdateColor(EnhancementsManager.Settings.CLSettings.ClockColor);
        }



        [UIValue("clock_x")]
        private float clock_x = EnhancementsManager.Settings.CLSettings.ClockPosition.x;

        [UIAction("clock_x")]
        private void Apply_ClockX(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockPosition;
            EnhancementsManager.Settings.CLSettings.ClockPosition = new Vector3(value, col.y, col.z);
            Clock.Clock.Instance.UpdatePos(EnhancementsManager.Settings.CLSettings.ClockPosition);
        }

        [UIValue("clock_y")]
        private float clock_y = EnhancementsManager.Settings.CLSettings.ClockPosition.y;

        [UIAction("clock_y")]
        private void Apply_ClockY(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockPosition;
            EnhancementsManager.Settings.CLSettings.ClockPosition = new Vector3(col.x, value, col.z);
            Clock.Clock.Instance.UpdatePos(EnhancementsManager.Settings.CLSettings.ClockPosition);
        }

        [UIValue("clock_z")]
        private float clock_z = EnhancementsManager.Settings.CLSettings.ClockPosition.z;

        [UIAction("clock_z")]
        private void Apply_ClockZ(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockPosition;
            EnhancementsManager.Settings.CLSettings.ClockPosition = new Vector3(col.x, col.y, value);
            Clock.Clock.Instance.UpdatePos(EnhancementsManager.Settings.CLSettings.ClockPosition);
        }


        [UIValue("clock_j")]
        private float clock_j = EnhancementsManager.Settings.CLSettings.ClockRotation.x;

        [UIAction("clock_j")]
        private void Apply_ClockJ(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockRotation;
            EnhancementsManager.Settings.CLSettings.ClockRotation = new Vector3(value, col.y, col.z);
            Clock.Clock.Instance.UpdateRot(EnhancementsManager.Settings.CLSettings.ClockRotation);
        }

        [UIValue("clock_k")]
        private float clock_k = EnhancementsManager.Settings.CLSettings.ClockRotation.y;

        [UIAction("clock_k")]
        private void Apply_ClockK(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockRotation;
            EnhancementsManager.Settings.CLSettings.ClockRotation = new Vector3(col.x, value, col.z);
            Clock.Clock.Instance.UpdateRot(EnhancementsManager.Settings.CLSettings.ClockRotation);
        }

        [UIValue("clock_l")]
        private float clock_l = EnhancementsManager.Settings.CLSettings.ClockRotation.z;

        [UIAction("clock_l")]
        private void Apply_ClockL(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockRotation;
            EnhancementsManager.Settings.CLSettings.ClockRotation = new Vector3(col.x, col.y, value);
            Clock.Clock.Instance.UpdateRot(EnhancementsManager.Settings.CLSettings.ClockRotation);
        }

        [UIAction("clock_resetpos")]
        private void ResetClockPos()
        {
            EnhancementsManager.Settings.CLSettings.ClockPosition = new Vector3(0f, 2.7f, 2.5f);
            Clock.Clock.Instance.UpdatePos(EnhancementsManager.Settings.CLSettings.ClockPosition);
        }

        [UIValue("clock_enable")]
        private bool clock_enable = EnhancementsManager.Settings.CLSettings.Enable;

        [UIAction("clock_enable")]
        private void Apply_ClockEnable(bool value)
        {
            EnhancementsManager.Settings.CLSettings.Enable = value;

            SetClockAdjuster(value);
        }

        [UIValue("clock_size")]
        private float clock_size = EnhancementsManager.Settings.CLSettings.FontSize;

        [UIAction("clock_size")]
        private void Apply_ClockSize(float value)
        {
            EnhancementsManager.Settings.CLSettings.FontSize = value;
            Clock.Clock.Instance.UpdateSize(EnhancementsManager.Settings.CLSettings.FontSize);
        }

        private void ChangeClockSetting()
        {
            choice2 = DateTime.Now.ToString((string)Clock.Clock.timeType[EnhancementsManager.Settings.CLSettings.TimeFormat]);
        }

        [UIValue("time-options")]
        private List<object> options2 = Clock.Clock.timeType;

        [UIValue("time-choice")]
        private string choice2 = (string)Clock.Clock.timeType[EnhancementsManager.Settings.CLSettings.TimeFormat];

        [UIAction("clock_format")]
        private void Apply_ClockFormat(object obj)
        {
            int index = options2.FindIndex(a => a == obj);

            EnhancementsManager.Settings.CLSettings.TimeFormat = index;

            ChangeClockSetting();
            SetTimeFormat();
        }

        private void SetClockAdjuster(bool value)
        {
            EnhancementsManager.Settings.CLSettings.Enable = value;
            Clock.Clock.Instance.UpdateClockState(value);
        }

        private void SetTimeFormat()
        {
            Clock.Clock.Instance.UpdateFormat(EnhancementsManager.Settings.CLSettings.TimeFormat);
        }

        [UIValue("va_hit")]
        private float va_hit = EnhancementsManager.Settings.VolumeAssistant.NoteHit;

        [UIAction("va_hit")]
        private void Apply_NoteHit(float value)
        {
            EnhancementsManager.Settings.VolumeAssistant.NoteHit = value;
        }

        [UIValue("va_miss")]
        private float va_miss = EnhancementsManager.Settings.VolumeAssistant.NoteMiss;

        [UIAction("va_miss")]
        private void Apply_NoteMiss(float value)
        {
            EnhancementsManager.Settings.VolumeAssistant.NoteMiss = value;
        }

        [UIValue("va_music")]
        private float va_music = EnhancementsManager.Settings.VolumeAssistant.Music;

        [UIAction("va_music")]
        private void Apply_Music(float value)
        {
            EnhancementsManager.Settings.VolumeAssistant.Music = value;
        }

        [UIValue("va_background")]
        private float va_background = EnhancementsManager.Settings.VolumeAssistant.MenuBackground;

        [UIAction("va_background")]
        private void Apply_Background(float value)
        {
            EnhancementsManager.Settings.VolumeAssistant.MenuBackground = value;
            ChangeMenuAudio();
        }

        [UIValue("va_preview")]
        private float va_preview = EnhancementsManager.Settings.VolumeAssistant.PreviewVolume;
        [UIAction("va_preview")]
        private void Apply_Preview(float value)
        {
            EnhancementsManager.Settings.VolumeAssistant.PreviewVolume = value;
        }

        private void ChangeMenuAudio()
        {
            if (EnhancementsManager.Instance.menuPlayer != null)
                EnhancementsManager.Instance.menuPlayer.volume = EnhancementsManager.Settings.VolumeAssistant.MenuBackground;
        }



        [UIValue("songskip_enable")]
        private bool songskip_enable = EnhancementsManager.Settings.SongSkip.Enable;

        [UIAction("songskip_enable")]
        private void Apply_songskipEnable(bool value)
        {
            EnhancementsManager.Settings.SongSkip.Enable = value;
        }

        [UIValue("songskip_intro")]
        private bool songskip_intro = EnhancementsManager.Settings.SongSkip.SkipIntro;

        [UIAction("songskip_intro")]
        private void Apply_songskipintro(bool value)
        {
            EnhancementsManager.Settings.SongSkip.SkipIntro = value;
        }

        [UIValue("songskip_outro")]
        private bool songskip_outro = EnhancementsManager.Settings.SongSkip.SkipOutro;

        [UIAction("songskip_outro")]
        private void Apply_songskipoutro(bool value)
        {
            EnhancementsManager.Settings.SongSkip.SkipOutro = value;
        }

        [UIValue("songskip_radial")]
        private bool songskip_radial = EnhancementsManager.Settings.SongSkip.Radial;

        [UIAction("songskip_radial")]
        private void Apply_songskipradial(bool value)
        {
            EnhancementsManager.Settings.SongSkip.Radial = value;
        }

        [UIValue("songskip_text")]
        private bool songskip_text = EnhancementsManager.Settings.SongSkip.Text;

        [UIAction("songskip_text")]
        private void Apply_songskiptext(bool value)
        {
            EnhancementsManager.Settings.SongSkip.Text = value;
        }

        [UIValue("songskip_minintro")]
        private float songskip_minintro = EnhancementsManager.Settings.SongSkip.MinimumIntroTime;
        [UIAction("songskip_minintro")]
        private void Apply_MinIntro(float value)
        {
            EnhancementsManager.Settings.SongSkip.MinimumIntroTime = value;
        }

        [UIValue("songskip_r")]
        private float songskip_r = EnhancementsManager.Settings.SongSkip.Notification.r;

        [UIAction("songskip_r")]
        private void Apply_SSR(float value)
        {
            var col = EnhancementsManager.Settings.SongSkip.Notification;
            EnhancementsManager.Settings.SongSkip.Notification = new Color(value, col.g, col.b, col.a);
        }

        [UIValue("songskip_g")]
        private float songskip_g = EnhancementsManager.Settings.SongSkip.Notification.g;

        [UIAction("songskip_g")]
        private void Apply_SSG(float value)
        {
            var col = EnhancementsManager.Settings.SongSkip.Notification;
            EnhancementsManager.Settings.SongSkip.Notification = new Color(col.r, value, col.b, col.a);
        }

        [UIValue("songskip_b")]
        private float songskip_b = EnhancementsManager.Settings.SongSkip.Notification.b;

        [UIAction("songskip_b")]
        private void Apply_SSB(float value)
        {
            var col = EnhancementsManager.Settings.SongSkip.Notification;
            EnhancementsManager.Settings.SongSkip.Notification = new Color(col.r, col.g, value, col.a);
        }

        [UIValue("songskip_a")]
        private float songskip_a = EnhancementsManager.Settings.SongSkip.Notification.a;

        [UIAction("songskip_a")]
        private void Apply_SSA(float value)
        {
            var col = EnhancementsManager.Settings.SongSkip.Notification;
            EnhancementsManager.Settings.SongSkip.Notification = new Color(col.r, col.g, col.b, value);
        }

        [UIValue("buttonlock")]
        private bool buttonlock = EnhancementsManager.Settings.GameAdjustments.ButtonLock;

        [UIAction("buttonlock")]
        private void Apply_BL(bool value)
        {
            EnhancementsManager.Settings.GameAdjustments.ButtonLock = value;
        }

        [UIValue("buttonlocktime")]
        private float buttonlocktime = EnhancementsManager.Settings.GameAdjustments.ButtonLockTime;

        [UIAction("buttonlocktime")]
        private void Apply_BLT(float value)
        {
            EnhancementsManager.Settings.GameAdjustments.ButtonLockTime = value;
        }
    }
}
