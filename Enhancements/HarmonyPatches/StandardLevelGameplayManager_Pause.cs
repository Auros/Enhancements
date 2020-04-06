using Enhancements.MiniTweaks;
using HarmonyLib;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Enhancements.HarmonyPatches
{
    [HarmonyPatch(typeof(StandardLevelGameplayManager), "HandlePauseControllerDidPause")]
    class StandardLevelGameplayManager_Pause
    {
        public static void Postfix()
        {
            if (Plugin.config.minitweaks.buttonlockenabled)
            {
                var buttonLock = Resources.FindObjectsOfTypeAll<ButtonLock>()?.FirstOrDefault();
                var allButtons = Resources.FindObjectsOfTypeAll<Button>();
                buttonLock.ButtonLocker(allButtons);
            }
        }
    }
}
