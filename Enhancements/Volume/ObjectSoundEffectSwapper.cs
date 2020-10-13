using Zenject;
using HarmonyLib;
using IPA.Utilities;

namespace Enhancements.Volume
{
    [HarmonyPatch(typeof(EffectPoolsInstaller), "ManualInstallBindings")]
    internal class ObjectSoundEffectSwapper
    {
        private static readonly FieldAccessor<NoteCutSoundEffect, float>.Accessor GetGoodCutVolume = FieldAccessor<NoteCutSoundEffect, float>.GetAccessor("_goodCutVolume");
        private static readonly FieldAccessor<NoteCutSoundEffect, float>.Accessor GetBadCutVolume = FieldAccessor<NoteCutSoundEffect, float>.GetAccessor("_badCutVolume");

        internal static void Prefix(DiContainer container, NoteCutSoundEffect ____noteCutSoundEffectPrefab)
        {
            var settings = container.Resolve<VolumeSettings>();

            GetGoodCutVolume(ref ____noteCutSoundEffectPrefab) = 0.5f * settings.GoodCuts;
            GetBadCutVolume(ref ____noteCutSoundEffectPrefab) = 0.9f * settings.BadCuts;
        }
    }
}