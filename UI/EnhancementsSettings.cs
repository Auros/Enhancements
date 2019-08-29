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

        
    }
}
