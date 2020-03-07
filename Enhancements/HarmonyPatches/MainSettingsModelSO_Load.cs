using HarmonyLib;

namespace Enhancements.HarmonyPatches
{
    [HarmonyPatch(typeof(MainSettingsModelSO), "Load")]
    class MainSettingsModelSO_Load
    {
        static void Postfix(ref MainSettingsModelSO __instance)
        {
            Enhancements.baseGameVolume = __instance.volume;
        }
    }
}
