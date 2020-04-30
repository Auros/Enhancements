using System;
using HarmonyLib;
using UnityEngine;

namespace Enhancements.HarmonyPatches
{
    [HarmonyPatch(typeof(AudioTimeSyncController), "Start")]
    class AudioTimeSyncController_Start
    {
        static void Postfix(ref AudioSource ____audioSource)
        {
            ____audioSource.volume = Plugin.config.volume.Music;
        }
    }
}
