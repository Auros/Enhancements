using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Utilities;
using UnityEngine.SceneManagement;
using SemVer;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using Version = SemVer.Version;

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
            
        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Y) && Enhancements.ClockInstance != null)
                Enhancements.ClockInstance.Active = false;
            else if (Input.GetKeyDown(KeyCode.Y) && Enhancements.ClockInstance == null)
            {
                Enhancements.Instance.InitializeClock();
            }
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {

        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuViewControllers")
            {
                if (Enhancements.Instance == null)
                {
                    var e = new GameObject("[E2] - Instance").AddComponent<Enhancements>();
                    UnityEngine.Object.DontDestroyOnLoad(e.gameObject);
                    e.SetupAll();
                }
            }
        }

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}
