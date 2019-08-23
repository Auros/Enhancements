using Harmony;
using IPA.Utilities;
using System;
using UnityEngine;
using static Enhancements.EnhancementsManager;

namespace Enhancements.Harmony
{
    [HarmonyPatch(typeof(SongPreviewPlayer))]
    [HarmonyPatch("CrossfadeTo")]
    [HarmonyPatch(new Type[] { typeof(AudioClip), typeof(float), typeof(float), typeof(float) })]
    class SongPreviewPlayerCrossfadeTo
    {
        static bool Prefix(SongPreviewPlayer __instance, AudioClip audioClip, float startTime, float duration, float volumeScale = 1f)
        {
            Instance.menuPlayer = __instance;
            var defaultClip = ReflectionUtil.GetPrivateField<AudioClip>(__instance, "_defaultAudioClip");
            if (audioClip == defaultClip)
            {
                __instance.volume = Settings.VolumeAssistant.MenuBackground;
            }
            else
            {
                if (Settings.VolumeAssistant.PreviewVolume == 0) return false;
                    __instance.volume = Settings.VolumeAssistant.PreviewVolume;
            }
            return true;
        }
    }
}