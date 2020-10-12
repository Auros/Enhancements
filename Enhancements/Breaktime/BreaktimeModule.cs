using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.ViewControllers;
using Enhancements.Breaktime;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Enhancements.UI.Breaktime
{
    [ViewDefinition("Enhancements.Views.Breaktime.breaktime-module.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\Breaktime\breaktime-module.bsml")]
    public class BreaktimeModule : BSMLAutomaticViewController
    {
        private BreaktimeLoader _loader;
        private FloatingScreen _floatingScreen;
        private BreaktimeManager _breaktimeManager;
        private AudioTimeSyncController _audioTimeSyncController;

        [UIComponent("image")]
        protected Image image;

        private string _timeText = ""; 
        [UIValue("time-text")]
        protected string TimeText
        {
            get => _timeText;
            set
            {
                _timeText = value;
                NotifyPropertyChanged();
            }
        }

        [Inject]
        public void Construct(BreaktimeLoader loader, BreaktimeManager breaktimeManager, AudioTimeSyncController audioTimeSyncController)
        {
            _loader = loader;
            _breaktimeManager = breaktimeManager;
            _audioTimeSyncController = audioTimeSyncController;
        }

        internal IEnumerator SpawnBreakController(float timeUntilEnd)
        {
            CreateScreen();
            IEnumerator textUpdate = UpdateText(timeUntilEnd);
            StartCoroutine(textUpdate);
            _floatingScreen.SetRootViewController(this, false);
            yield return new WaitForSeconds(timeUntilEnd - 1f);
            StopCoroutine(textUpdate);
            _floatingScreen.SetRootViewController(null, false);
        }

        internal async void StartBreak(float time)
        {
            var profile = _loader.ActiveProfile();
            if (profile == null)
            {
                return;
            }
            /*try
            {*/
                var assets = await _loader.GetProfileAssets(profile);
                bool isAnimatedImage = false;
                var spr = assets.Item1;
                var audio = assets.Item2;
                if (assets.Item1 == null)
                {
                    if (profile.ImagePath != null && profile.ImagePath.EndsWith("gif") || profile.ImagePath.EndsWith("apng"))
                    {
                        isAnimatedImage = true;
                    }
                }
                if (isAnimatedImage)
                {
                    image.SetImage(Path.Combine(BreaktimeLoader.IMAGE_FOLDER, profile.ImagePath));
                }
                else if (spr != null)
                {
                    image.sprite = spr;
                }
            /*}
            catch
            {

            }*/
            StartCoroutine(SpawnBreakController(time));
        }

        internal IEnumerator UpdateText(float timeUntilEnd)
        {
            var end = timeUntilEnd + _audioTimeSyncController.songTime;
            while (true)
            {
                TimeText = (end - _audioTimeSyncController.songTime).ToString("F");
                yield return new WaitForSeconds(0.05f);
            }
        }

        internal void CreateScreen()
        {
            if (_floatingScreen == null)
            {
                _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(75f, 75f), false, new Vector3(0f, 2f, 4f), Quaternion.identity);
                _floatingScreen.GetComponent<Image>().enabled = false;
                _floatingScreen.SetRootViewController(null, true);
            }
        }

        protected void Start()
        {
            _breaktimeManager.BreakDetected += BreakDetected;
        }

        private void BreakDetected(float timeForNextNote)
        {
            Plugin.Log.Info("YO");
            StartBreak(timeForNextNote - _audioTimeSyncController.songTime);
            //StartCoroutine(SpawnBreak(timeForNextNote - _audioTimeSyncController.songTime));
        }

        protected override void OnDestroy()
        {
            _breaktimeManager.BreakDetected -= BreakDetected;
            base.OnDestroy();
        }
    }
}