using Harmony;
using IPA;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace Enhancements
{
    public class Plugin : IBeatSaberPlugin
    {
        public static float baseGameVolume = 1f;

        internal static HarmonyInstance harmony;

        public void Init(IPALogger logger)
        {
            Logger.log = logger;
        }

        public void OnApplicationStart()
        {
            harmony = HarmonyInstance.Create("com.auros.BeatSaber.Enhancements");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

            EnhancementsManager.Settings.Load();
        }

        public void OnApplicationQuit()
        {
            EnhancementsManager.Settings.Save();
        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "EmptyTransition")
                EnhancementsManager.Create();
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {

        }

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}
