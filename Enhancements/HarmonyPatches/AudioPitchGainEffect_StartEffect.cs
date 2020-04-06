using System;
using HarmonyLib;


namespace Enhancements.HarmonyPatches
{
    [HarmonyPatch(typeof(AudioPitchGainEffect), "StartEffect")]
    class AudioPitchGainEffect_StartEffect
    {
        static bool Prefix(AudioPitchGainEffect __instance, Action finishCallback)
        {
            __instance.StartCoroutine(__instance.StartEffectCoroutine(Plugin.config.volume.Music, finishCallback));
            return false;
        }
    }
}
