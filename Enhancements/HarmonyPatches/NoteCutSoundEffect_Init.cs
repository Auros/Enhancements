using HarmonyLib;

namespace Enhancements.HarmonyPatches
{
    [HarmonyPatch(typeof(NoteCutSoundEffect))]
    [HarmonyPatch("Init")]
    class NoteCutSoundEffect_Init
    {
        static void Prefix(ref float ____goodCutVolume, ref float ____badCutVolume)
        {
            ____goodCutVolume = Plugin.config.volume.GoodCuts * 0.5f;
            ____badCutVolume = Plugin.config.volume.BadCuts * 0.9f;
        }
    }
}
