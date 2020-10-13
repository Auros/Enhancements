using Zenject;
using HarmonyLib;
using IPA.Utilities;
using Enhancements.Misc;

namespace Enhancements.Misc
{
    [HarmonyPatch(typeof(GameplayCoreSceneSetup), "InstallBindings")]
    internal class BPM360Verifier
    {
        internal static readonly PropertyAccessor<BeatmapData, int>.Setter SetRotationCount = PropertyAccessor<BeatmapData, int>.GetSetter("spawnRotationEventsCount");
        internal static readonly PropertyAccessor<MonoInstallerBase, DiContainer>.Getter GetDiContainer = PropertyAccessor<MonoInstallerBase, DiContainer>.GetGetter("Container");

        internal static void Prefix(ref GameplayCoreSceneSetup __instance, ref GameplayCoreSceneSetupData ____sceneSetupData)
        {
            var container = GetContainer(__instance);
            var settings = container.Resolve<MiscSettings>();
            if (settings.BPMFixEnabled)
            {
                bool actually360 = false;
                var data = ____sceneSetupData.difficultyBeatmap.beatmapData.beatmapEventData;
                var spawnRotationProcessor = new SpawnRotationProcessor();
                for (int i = 0; i < data.Length; i++)
                {
                    var bmEvent = data[i];
                    if (bmEvent.type == BeatmapEventType.Event14 || bmEvent.type == BeatmapEventType.Event15)
                    {
                        if (spawnRotationProcessor.RotationForEventValue(bmEvent.value) != 0)
                        {
                            actually360 = true;
                        }
                    }
                }
                if (!actually360)
                {
                    var beatmapData = ____sceneSetupData.difficultyBeatmap.beatmapData;
                    SetRotationCount(ref beatmapData, 0);
                }
            }
        }

        private static DiContainer GetContainer(GameplayCoreSceneSetup gameplayCoreSceneSetup)
        {
            MonoInstallerBase monoInstaller = gameplayCoreSceneSetup;
            DiContainer container = GetDiContainer(ref monoInstaller);
            return container;
        }
    }
}