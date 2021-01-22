using IPA;
using IPA.Loader;
using HarmonyLib;
using SiraUtil.Zenject;
using IPA.Config.Stores;
using System.Reflection;
using Enhancements.Installers;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace Enhancements
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; set; }
        private readonly Harmony _harmony;

        [Init]
        public Plugin(Conf conf, IPALogger logger, Zenjector zenjector, PluginMetadata metadata)
        {
            Log = logger;
            Config config = conf.Generated<Config>();

            if (config.Version.ToString() == "0.0.0" && config.Clock.Position == new UnityEngine.Vector3(0f, 2.8f, 2.45f))
            {
                config.Clock.Position = new UnityEngine.Vector3(0f, 3f, 3.9f);
            }
            config.Version = metadata.Version;

            _harmony = new Harmony("dev.auros.enhancements");
            zenjector.OnApp<XInstaller>().WithParameters(config, metadata.Version);
            zenjector.OnGame<XGameInstaller>(false).ShortCircuitForTutorial();
            zenjector.OnMenu<XMenuInstaller>();
        }

        [OnEnable]
        public void OnEnable()
        {
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void OnDisable()
        {
            _harmony.UnpatchAll("dev.auros.enhancements");
        }
    }
}