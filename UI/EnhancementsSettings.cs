using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using TMPro;

namespace Enhancements.UI
{
    public class EnhancementsSettings : PersistentSingleton<EnhancementsSettings>
    {
        [UIComponent("firstnotice")]
        private TextMeshProUGUI firstcomment;

        [UIComponent("applyimmediatenotice")]
        private TextMeshProUGUI applycomment;
        public void SetTexts()
        {
            firstcomment.text = $"<align=\"center\"><b>For your <color=#00ffff>convience</color>...</b></align>";
            applycomment.text = $"<size=75%>You <color=\"red\">DO NOT</color> have to press the <color=\"green\">APPLY</color> or <color=\"green\">OK</color> buttons for all settings involved with Enhancements. Once you change the values, just press Cancel! Hover over settings to learn more about them. </size>";
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

    }
}
