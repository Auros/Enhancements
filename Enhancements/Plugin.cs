using IPA;
using IPA.Config;
using UnityEngine;
using IPA.Utilities;
using Version = SemVer.Version;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace Enhancements
{
    public class Plugin : IBeatSaberPlugin, IDisablablePlugin
    {
        internal static string Name => "Enhancements";
        internal static Version Version => new Version("2.0.0-alpha");
        internal static Ref<PluginConfig> config;
        internal static IConfigProvider configProvider;

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
            configProvider = cfgProvider;
            config = configProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig)
                {
                    p.Store(v.Value = new PluginConfig()
                    {
                        RegenerateConfig = false
                    });
                }
                config = v;
            });
        }

        public void OnEnable()
        {
            
        }

        public void OnDisable()
        {
            
        }

        public void OnApplicationStart()
        {
            
        }

        public void OnApplicationQuit()
        {
            configProvider.Store(config.Value);
        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Y) && Enhancements.ClockInstance != null)
                Enhancements.ClockInstance.Active = false;
            else if (Input.GetKeyDown(KeyCode.Y) && Enhancements.ClockInstance == null)
            {
                Enhancements.Instance.InitializeClock();
            }
#endif
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "MenuViewControllers")
                Enhancements.Instance.OnMenu();
            if (nextScene.name == "GameCore")
                Enhancements.Instance.OnGame();
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuViewControllers")
            {
                if (Enhancements.Instance == null)
                {
                    var e = new GameObject("[E2] - Instance").AddComponent<Enhancements>();
                    Object.DontDestroyOnLoad(e.gameObject);
                    e.SetupAll();
                }
            }
        }

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}
