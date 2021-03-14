using HarmonyLib;
using System;
using UnityEngine;

namespace Enhancements.Volume
{
    [HarmonyPatch(typeof(SongPreviewPlayer), nameof(SongPreviewPlayer.CrossfadeTo), argumentTypes: new Type[] { typeof(AudioClip), typeof(float), typeof(float), typeof(bool) })]
    internal class PreviewPatcher
    {
        internal static void Postfix(ref float ____volumeScale, bool isDefault)
        {
            if (!isDefault)
                ____volumeScale = Config.Value.Volume.SongPreview;
        }
    }

    [HarmonyPatch(typeof(AudioPitchGainEffect), nameof(AudioPitchGainEffect.StartEffect))]
    internal class FixGain
    {
        internal static void Prefix(ref float volumeScale)
        {
            volumeScale *= Config.Value.Volume.Music;
        }
    }
}