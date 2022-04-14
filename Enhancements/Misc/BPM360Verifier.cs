using Zenject;
using HarmonyLib;
using IPA.Utilities;
using Enhancements.Misc;

namespace Enhancements.Misc
{
    [HarmonyPatch(typeof(GameplayCoreInstaller), "InstallBindings")]
    internal class BPM360Verifier
    {
        internal static readonly PropertyAccessor<MonoInstallerBase, DiContainer>.Getter GetDiContainer = PropertyAccessor<MonoInstallerBase, DiContainer>.GetGetter("Container");

        internal static void Postfix(ref GameplayCoreInstaller __instance, ref GameplayCoreSceneSetupData ____sceneSetupData)
        {
            var container = GetContainer(__instance);
            var settings = container.Resolve<MiscSettings>();
            if (settings.BPMFixEnabled)
            {
                bool actually360 = false;
                foreach (var item in ____sceneSetupData.transformedBeatmapData.allBeatmapDataItems)
                {
                    if (item is SpawnRotationBeatmapEventData rotation)
                    {
                        if (rotation.rotation != 0)
                        {
                            actually360 = true;
                            break;
                        }
                    }
                }
                if (!actually360)
                {
                    container.Unbind<BeatLineManager>();
                }
            }
        }

        private static DiContainer GetContainer(GameplayCoreInstaller gameplayCoreInstaller)
        {
            MonoInstallerBase monoInstaller = gameplayCoreInstaller;
            DiContainer container = GetDiContainer(ref monoInstaller);
            return container;
        }
    }
}