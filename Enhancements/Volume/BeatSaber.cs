using HarmonyLib;

namespace Enhancements.Volume
{
    [HarmonyPatch(typeof(MainSettingsModelSO), "Load")]
    internal class BeatSaber
    {
        internal static float Volume = 1f;

        internal static void Postfix(ref MainSettingsModelSO __instance)
        {
            Volume = __instance.volume.value;
        }
    }
}