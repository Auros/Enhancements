using Harmony;
using IPA.Utilities;
using UnityEngine;
using static Enhancements.EnhancementsManager;

namespace Enhancements.Harmony
{
    [HarmonyPatch(typeof(GameSongController))]
    [HarmonyPatch("StartSong")]
    class GameSongControllerStartSong
    {
        static bool Prefix(ref GameSongController __instance)
        {
            AudioTimeSyncController audioController = __instance.GetPrivateField<AudioTimeSyncController>("_audioTimeSyncController");
            AudioSource audioSource = ReflectionUtil.GetPrivateField<AudioSource>(audioController, "_audioSource");
            audioSource.volume = Settings.VolumeAssistant.Music;
            return true;
        }
    }
}
