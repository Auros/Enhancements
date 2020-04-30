using IPA;
using HarmonyLib;
using IPA.Config;
using UnityEngine;
using IPA.Config.Stores;
using Version = SemVer.Version;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace Enhancements
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static string Name => "Enhancements";
        internal static Version Version => new Version("2.0.3");
        internal static PluginConfig config;
        internal static Harmony harmony;
        [Init]
        public void Init(IPALogger logger, Config conf)
        {
            Logger.log = logger;
            config = conf.Generated<PluginConfig>();
        }

        [OnEnable]
        public void Enable()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
            harmony = new Harmony($"com.auros.BeatSaber.{Name}");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void Disable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            harmony?.UnpatchAll();

            Enhancements.WrapItUpBoysItsTimeToGo();
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
    }
}
