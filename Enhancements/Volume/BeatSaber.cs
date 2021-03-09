using HarmonyLib;
using System;
using UnityEngine;

namespace Enhancements.Volume
{
    // Cringe
    [HarmonyPatch(typeof(MainSettingsModelSO), "Load")]
    internal class BeatSaber
    {
        internal static float Volume = 1f;

        internal static void Postfix(ref MainSettingsModelSO __instance)
        {
            Volume = __instance.volume.value;
        }
    }

    [HarmonyPatch(typeof(SongPreviewPlayer), nameof(SongPreviewPlayer.CrossfadeTo), argumentTypes: new Type[] { typeof(AudioClip), typeof(float), typeof(float), typeof(bool) })]
    internal class PreviewPatcher
    {
        internal static void Postfix(ref float ____volumeScale, bool isDefault)
        {
            if (!isDefault)
                ____volumeScale = Config.Value.Volume.SongPreview;
        }
    }
}