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
            _harmony = new Harmony("dev.auros.enhancements");
            zenjector.OnApp<XInstaller>().WithParameters(config, metadata.Version);
            zenjector.OnMenu<XMenuInstaller>();
            zenjector.OnGame<XGameInstaller>(false).ShortCircuitForTutorial();
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