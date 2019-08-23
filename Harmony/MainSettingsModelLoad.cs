using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancements.Harmony
{
    [HarmonyPatch(typeof(MainSettingsModel))]
    [HarmonyPatch("Load")]
    class MainSettingsModelLoad
    {
        static void Postfix(ref MainSettingsModel __instance)
        {
            Plugin.baseGameVolume = __instance.volume.value;
        }
    }
}
