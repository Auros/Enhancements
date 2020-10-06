using IPA;
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
        public Plugin(Conf conf, IPALogger logger, Zenjector zenjector)
        {
            Log = logger;
            Config config = conf.Generated<Config>();
            zenjector.OnApp<EInstaller>().WithParameters(config);
            zenjector.OnMenu<EMenuInstaller>();
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