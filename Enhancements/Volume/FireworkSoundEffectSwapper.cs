using Zenject;
using SiraUtil;
using HarmonyLib;
using UnityEngine;
using IPA.Utilities;

namespace Enhancements.Volume
{
    [HarmonyPatch(typeof(MenuInstaller), "InstallBindings")]
    internal class FireworkSoundEffectSwapper
    {
        internal static FieldAccessor<FireworkItemController, AudioSource>.Accessor GetFireworkControllerAudioSource = FieldAccessor<FireworkItemController, AudioSource>.GetAccessor("_audioSource");

        internal static void Prefix(MenuInstaller __instance, FireworkItemController ____fireworkItemControllerPrefab)
        {
            var mib = __instance as MonoInstallerBase;
            var container = Accessors.GetDiContainer(ref mib);

            var vs = container.ParentContainers[0].TryResolve<VolumeSettings>();
            if (!(vs is null))
            {
                GetFireworkControllerAudioSource(ref ____fireworkItemControllerPrefab).volume = 0.6f * vs.Fireworks;
            }
        }
    }
}