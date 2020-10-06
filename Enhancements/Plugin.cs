using IPA;
using IPA.Loader;
using SiraUtil.Zenject;
using IPA.Config.Stores;
using Enhancements.Installers;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace Enhancements
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; set; }

        [Init]
        public Plugin(Conf conf, IPALogger logger, Zenjector zenjector, PluginMetadata metadata)
        {
            Log = logger;
            Config config = conf.Generated<Config>();
            zenjector.OnApp<XInstaller>().WithParameters(config, metadata.Version);
            zenjector.OnMenu<XMenuInstaller>();
        }

        [OnEnable]
        public void OnEnable()
        {
            
        }

        [OnDisable]
        public void OnDisable()
        {

        }
    }
}