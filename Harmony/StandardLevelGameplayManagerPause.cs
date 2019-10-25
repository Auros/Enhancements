using Enhancements.GameAdjustments;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enhancements.Harmony
{
    [HarmonyPatch(typeof(StandardLevelGameplayManager), "Pause")]
    class StandardLevelGameplayManagerPause
    {
        public static bool Prefix()
        {
            if (EnhancementsManager.Settings.GameAdjustments.ButtonLock)
            {
                var buttonLock = Resources.FindObjectsOfTypeAll<ButtonLock>().FirstOrDefault();
                var allButtons = Resources.FindObjectsOfTypeAll<Button>();
                buttonLock.ButtonLocker(allButtons);
            }
            Logger.log.Info(EnhancementsManager.Settings.GameAdjustments.ButtonLock.ToString());
            
            return true;
        }
    }
}
