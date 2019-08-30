using Harmony;
using System;
using static Enhancements.EnhancementsManager;

namespace Enhancements.Harmony
{
    [HarmonyPatch(typeof(AudioPitchGainEffect))]
    [HarmonyPatch("StartEffect")]
    [HarmonyPatch(new Type[] { typeof(float), typeof(Action) })]
    class AudioPitchGainEffectStartEffect
    {
        static bool Prefix(AudioPitchGainEffect __instance, float volumeScale, Action finishCallback)
        {
            __instance.StartEffect(Settings.VolumeAssistant.Music, finishCallback);
            return false;
        }
    }
}