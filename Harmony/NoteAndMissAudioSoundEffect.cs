using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enhancements.EnhancementsManager;

namespace Enhancements.Harmony
{
    [HarmonyPatch(typeof(NoteCutSoundEffect))]
    [HarmonyPatch("Init")]
    class NoteAndMissAudioSoundEffect
    {
        static bool Prefix(ref float ____goodCutVolume, ref float ____badCutVolume)
        {
            ____goodCutVolume = Settings.VolumeAssistant.NoteHit * .5f;
            ____badCutVolume = Settings.VolumeAssistant.NoteMiss * .9f;

            return true;
        }
    }
}
