using TMPro;
using System;
using Zenject;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.spacepuppy.Tween;
using Enhancements.Breaktime;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.ViewControllers;
using IPA.Utilities;

namespace Enhancements.UI.Breaktime
{
    [ViewDefinition("Enhancements.Views.Breaktime.breaktime-module.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\Breaktime\breaktime-module.bsml")]
    public class BreaktimeModule : BSMLAutomaticViewController, IInitializable
    {
        private bool _breakHappening;
        private BreaktimeLoader _loader;
        private AudioSource _audioSource;
        private FloatingScreen _floatingScreen;
        private BreaktimeManager _breaktimeManager;
        private AudioTimeSyncController _audioTimeSyncController;

        [UIComponent("image")]
        protected Image image;

        [UIComponent("text")]
        protected TextMeshProUGUI text;

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
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = Plugin.AudioSource(ref _audioTimeSyncController).outputAudioMixerGroup;
            gameObject.SetActive(true);
        }

        internal async void StartBreak(float time)
        {
            var profile = _loader.ActiveProfile();
            if (profile == null)
            {
                return;
            }
            var assets = await _loader.GetProfileAssets(profile);
            gameObject.SetActive(true);
            _audioTimeSyncController.StartCoroutine(HandleBreak(time, profile, assets));
        }

        internal IEnumerator HandleBreak(float timeUntilEnd, Profile profile, Tuple<Sprite, AudioClip> assets)
        {
            if (!_breakHappening)
            {
                CreateScreen();
                _breakHappening = true;
                var endPoint = timeUntilEnd + _audioTimeSyncController.songTime;
                gameObject.SetActive(true);
                _floatingScreen.gameObject.SetActive(true);
                _floatingScreen.SetRootViewController(this, profile.Animation == Animation.FadeIn ? AnimationType.In : AnimationType.None);
                IEnumerator textUpdate = UpdateText(endPoint);
                SetupVisuals(profile, assets);
                _audioTimeSyncController.StartCoroutine(textUpdate);
                ModifyVisuals(profile);
                if (profile.Animation == Animation.SlideUp)
                {
                    _audioTimeSyncController.StartCoroutine(ActivateSlidingAnimation(endPoint));
                }
                yield return new WaitUntil(() => _audioTimeSyncController.songTime > endPoint - 2f);
                _floatingScreen.ScreenPosition = new Vector3(0f, 1.5f, 4f);
                if (profile.Animation == Animation.SlideUp)
                {
                    yield return new WaitForSeconds(1f);
                }
                _floatingScreen.SetRootViewController(null, profile.Animation == Animation.FadeIn ? AnimationType.In : AnimationType.None);
                if (profile.Animation == Animation.FadeIn)
                {
                    yield return new WaitForSeconds(1f);
                }
                _audioTimeSyncController.StopCoroutine(textUpdate);
                _floatingScreen.gameObject.SetActive(true);
                _breakHappening = false;
            }
        }

        private Color addAlphaToColor(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        private void ModifyVisuals(Profile profile)
        {
            image.color = addAlphaToColor(profile.ImageColor, profile.ImageOpacity);
            text.color = addAlphaToColor(profile.TextColor, profile.ImageOpacity);
        }

        internal IEnumerator UpdateText(float endPoint)
        {
            while (true)
            {
                TimeText = ((endPoint - _audioTimeSyncController.songTime) / _audioTimeSyncController.timeScale).ToString("F");
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void SetupVisuals(Profile profile, Tuple<Sprite, AudioClip> assets)
        {
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
                spr.texture.wrapMode = TextureWrapMode.Clamp;
                image.sprite = spr;
            }
            if (audio != null)
            {
                _audioSource.PlayOneShot(audio);
            }
        }

        internal IEnumerator ActivateSlidingAnimation(float endPoint)
        {
            _floatingScreen.ScreenPosition = new Vector3(0f, -5f, 4f);
            float t = 0f;
            var ease = EaseMethods.GetEase(EaseStyle.ElasticEaseOut);
            var ease2 = EaseMethods.GetEase(EaseStyle.ElasticEaseIn);
            while (t < 1f)
            {
                float sc = ease(t, 0f, 1f, 1f);
                _floatingScreen.ScreenPosition = Vector3.LerpUnclamped(new Vector3(0f, -5f, 4f), new Vector3(0f, 1.5f, 4f), sc);
                yield return null;
                t += Time.deltaTime;
            }
            _floatingScreen.ScreenPosition = new Vector3(0f, 1.5f, 4f);
            yield return new WaitUntil(() => _audioTimeSyncController.songTime > endPoint - 2f);
            t = 0f;
            while (t < 1f)
            {
                float sc = ease2(t, 0f, 1f, 1f);
                _floatingScreen.ScreenPosition = Vector3.LerpUnclamped(new Vector3(0f, 1.5f, 4f), new Vector3(0f, -5f, 4f), sc);
                yield return null;
                t += Time.deltaTime;
            }
            _floatingScreen.ScreenPosition = new Vector3(0f, -5f, 4f);
        }

        internal void CreateScreen()
        {
            if (_floatingScreen == null)
            {
                _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(75f, 75f), false, new Vector3(0f, 1.5f, 4f), Quaternion.identity);
                _floatingScreen.SetRootViewController(null, AnimationType.In);
                _floatingScreen.gameObject.SetActive(false);
            }
            else
            {
                _floatingScreen.gameObject.SetActive(true);
            }
        }

        public void Start()
        {
            _breaktimeManager.BreakDetected += BreakDetected;
        }

        private void BreakDetected(float timeForNextNote)
        {
            StartBreak(timeForNextNote - _audioTimeSyncController.songTime);
        }

        protected override void OnDestroy()
        {
            _breaktimeManager.BreakDetected -= BreakDetected;
            base.OnDestroy();
        }

        public void Initialize()
        {

        }
    }
}