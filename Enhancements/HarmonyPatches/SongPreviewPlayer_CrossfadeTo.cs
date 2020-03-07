using HarmonyLib;
using UnityEngine;

namespace Enhancements.HarmonyPatches
{
    [HarmonyPatch(typeof(SongPreviewPlayer), "CrossfadeTo")]
    class SongPreviewPlayer_CrossfadeTo
    {
        static bool Prefix(SongPreviewPlayer __instance, AudioClip audioClip, ref AudioClip ____defaultAudioClip)
        {
            Enhancements.menuPlayer = __instance;
            if (audioClip == ____defaultAudioClip)
                __instance.volume = Plugin.config.volume.MenuBackground;
            else if (Plugin.config.volume.SongPreview != 0)
                __instance.volume = Plugin.config.volume.SongPreview;
            else return false;
            return true;
        }
    }
}
