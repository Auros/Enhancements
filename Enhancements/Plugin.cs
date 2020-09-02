using IPA;
using SiraUtil.Zenject;
using Enhancements.Installers;
using IPALogger = IPA.Logging.Logger;

namespace Enhancements
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        private readonly EInstaller.InitData _enhancementsInitData;

        internal static IPALogger Log { get; set; }

        [Init]
        public Plugin(IPALogger logger)
        {
            Log = logger;

            _enhancementsInitData = new EInstaller.InitData();
        }

        [OnEnable]
        public void OnEnable()
        {
            Installer.RegisterAppInstaller(_enhancementsInitData);
            Installer.RegisterMenuInstaller<EMenuInstaller>();
        }

        [OnDisable]
        public void OnDisable()
        {
            Installer.UnregisterAppInstaller(_enhancementsInitData);
            Installer.UnregisterAppInstaller<EMenuInstaller>();
        }
    }
}