using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using TMPro;
using UnityEngine;

namespace Enhancements.UI
{
    public class EnhancementsSettings : PersistentSingleton<EnhancementsSettings>
    {
        [UIComponent("firstnotice")]
        private TextMeshProUGUI firstcomment;

        [UIComponent("applyimmediatenotice")]
        private TextMeshProUGUI applycomment;

        [UIComponent("visualizationhelp")]
        private TextMeshProUGUI visualizationhelp;
        public void SetTexts()
        {
            firstcomment.text = $"<align=\"center\"><b>For your <color=#00ffff>convience</color>...</b></align>";
            applycomment.text = $"<size=75%>You <color=\"red\">DO NOT</color> have to press the <color=\"green\">APPLY</color> or <color=\"green\">OK</color> buttons for all settings involved with Enhancements. Once you change the values, just press Cancel! Hover over settings to learn more about them. </size>";
            visualizationhelp.text = $"<align=\"center\"><b><color=#3bffcb><u>Visualization</u></color></b></align>\n" +
                $"<size=65%><align=\"center\">" +
                $"When visualization is activated, an image and audio effect will activate. You can adjust the individual settings for the visualization here. You can " +
                $"also set the visualization to Custom and put your own files (.wav and .png) into the folder below. If you have one audio file & image in the folder, it " +
                $"will always play those. Multiple files will randomize the values!</align></size>";
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
        }

        [UIValue("clock_g")]
        private float clock_g = EnhancementsManager.Settings.CLSettings.ClockColor.g;

        [UIAction("clock_g")]
        private void Apply_ClockG(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockColor;
            EnhancementsManager.Settings.CLSettings.ClockColor = new Color(col.r, value, col.b, col.a);
        }

        [UIValue("clock_b")]
        private float clock_b = EnhancementsManager.Settings.CLSettings.ClockColor.b;

        [UIAction("clock_b")]
        private void Apply_ClockB(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockColor;
            EnhancementsManager.Settings.CLSettings.ClockColor = new Color(col.r, col.g, value, col.a);
        }

        [UIValue("clock_a")]
        private float clock_a = EnhancementsManager.Settings.CLSettings.ClockColor.a;

        [UIAction("clock_a")]
        private void Apply_ClockA(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockColor;
            EnhancementsManager.Settings.CLSettings.ClockColor = new Color(col.r, col.g, col.b, value);
        }



        [UIValue("clock_x")]
        private float clock_x = EnhancementsManager.Settings.CLSettings.ClockPosition.x;

        [UIAction("clock_x")]
        private void Apply_ClockX(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockPosition;
            EnhancementsManager.Settings.CLSettings.ClockPosition = new Vector3(value, col.y, col.z);
        }

        [UIValue("clock_y")]
        private float clock_y = EnhancementsManager.Settings.CLSettings.ClockPosition.y;

        [UIAction("clock_y")]
        private void Apply_ClockY(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockPosition;
            EnhancementsManager.Settings.CLSettings.ClockPosition = new Vector3(col.x, value, col.z);
        }

        [UIValue("clock_z")]
        private float clock_z = EnhancementsManager.Settings.CLSettings.ClockPosition.z;

        [UIAction("clock_z")]
        private void Apply_ClockZ(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockPosition;
            EnhancementsManager.Settings.CLSettings.ClockPosition = new Vector3(col.x, col.y, value);
        }


        [UIValue("clock_j")]
        private float clock_j = EnhancementsManager.Settings.CLSettings.ClockRotation.x;

        [UIAction("clock_j")]
        private void Apply_ClockJ(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockRotation;
            EnhancementsManager.Settings.CLSettings.ClockRotation = new Vector3(value, col.y, col.z);
        }

        [UIValue("clock_k")]
        private float clock_k = EnhancementsManager.Settings.CLSettings.ClockRotation.y;

        [UIAction("clock_k")]
        private void Apply_ClockK(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockRotation;
            EnhancementsManager.Settings.CLSettings.ClockRotation = new Vector3(col.x, value, col.z);
        }

        [UIValue("clock_l")]
        private float clock_l = EnhancementsManager.Settings.CLSettings.ClockRotation.z;

        [UIAction("clock_l")]
        private void Apply_ClockL(float value)
        {
            var col = EnhancementsManager.Settings.CLSettings.ClockRotation;
            EnhancementsManager.Settings.CLSettings.ClockRotation = new Vector3(col.x, col.y, value);
        }
    }
}
