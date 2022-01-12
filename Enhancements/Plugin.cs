using IPA;
using IPA.Loader;
using HarmonyLib;
using SiraUtil.Zenject;
using IPA.Config.Stores;
using System.Reflection;
using Enhancements.Installers;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;
using IPA.Utilities;
using UnityEngine;

namespace Enhancements
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        public static readonly FieldAccessor<AudioTimeSyncController, AudioSource>.Accessor AudioSource = FieldAccessor<AudioTimeSyncController, AudioSource>.GetAccessor("_audioSource");
        internal static IPALogger Log { get; set; }
        private readonly Harmony _harmony;

        [Init]
        public Plugin(Conf conf, IPALogger logger, Zenjector zenjector, PluginMetadata metadata)
        {
            Log = logger;
            Config config = conf.Generated<Config>();
            Config.Value = config;

            if (config.Version.ToString() == "0.0.0" && config.Clock.Position == new UnityEngine.Vector3(0f, 2.8f, 2.45f))
            {
                config.Clock.Position = new UnityEngine.Vector3(0f, 3f, 3.9f);
            }
            config.Version = metadata.HVersion;

            _harmony = new Harmony("dev.auros.enhancements");
            zenjector.Install<XInstaller>(Location.App, config, metadata.HVersion);
            zenjector.Install<XGameInstaller>(Location.Player);
            zenjector.Install<XMenuInstaller>(Location.Menu);
        }

        [OnEnable]
        public void OnEnable()
        {
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void OnDisable()
        {
            _harmony.UnpatchSelf();
        }
    }
}