using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.ViewControllers;
using Enhancements.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enhancements.Breaktime
{
    public class BreaktimeManager : HotReloadableViewController
    {
        public override string ResourceName => "Enhancements.Views.breaktime.bsml";

        public override string ContentFilePath => IPA.Utilities.UnityGame.InstallPath + "\\breaktime.bsml";

        private string _image = "Enhancements.Resources.Default.png";
        [UIValue("image")]
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }

        private string _time = "0.00";
        [UIValue("time")]
        public string Timer
        {
            get => _time;
            set
            {
                _time = value;
                NotifyPropertyChanged();
            }
        }

        [UIComponent("tmp")]
        public TextMeshProUGUI text;

        [UIComponent("img")]
        public Image img;

        [UIObject("everything")]
        public GameObject tform;
        
        private AudioClip _currentClip;
        internal FloatingScreen screenS;
        private ScoreController _scoreController;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                screenS.transform.localPosition = new Vector3(0f, -5.5f, 3f);
                StartCoroutine(ActivateBreaktimeCoroutine(9f));
            }
        }

        private void Load()
        {
            
            text.color = Plugin.config.breaktime.Color.ToColor();
            text.font = Extensions.ArcadePix;
            _scoreController = Resources.FindObjectsOfTypeAll<ScoreController>().FirstOrDefault();
            _scoreController.noteWasCutEvent += ScoreController_noteWasCutEvent;
            _scoreController.noteWasMissedEvent += ScoreController_noteWasMissedEvent;

            var selProf = Plugin.config.breaktime.SelectedProfile;
            if (selProf.Equals("Default") || selProf.Equals("osu!") || selProf.Equals("Bobbie"))
            {
                if (Plugin.config.breaktime.ShowImage) Image = $"Enhancements.Resources.{selProf}.png";
                else img.enabled = false;
                if (Plugin.config.breaktime.PlayAudio)
                {
                    byte[] clipData = BeatSaberMarkupLanguage.Utilities.GetResource(System.Reflection.Assembly.GetExecutingAssembly(), $"Enhancements.Resources.{selProf}.wav");
                    _currentClip = WavUtility.ToAudioClip(clipData);
                }

                if (selProf == "osu!") img.color = Plugin.config.breaktime.Color.ToColor();
            }
            else 
            {
                var profile = GetValidBreaktimePaths().Where(x => x.Name == selProf).FirstOrDefault();
                if (profile != null)
                {
                    var files = profile.GetFiles();
                    bool imageExists = files.Any(x => x.Name.ToLower().Contains("image"));
                    bool audioExists = files.Any(x => x.Name.ToLower().Contains("audio.wav"));
                    if (imageExists && Plugin.config.breaktime.ShowImage)
                    {
                        var file = files.Where(x => x.Name.ToLower().Contains("image")).FirstOrDefault();
                        Image = file.FullName;
                    }
                    else img.enabled = false;
                    if (audioExists && Plugin.config.breaktime.PlayAudio)
                    {
                        try
                        {
                            var file = files.Where(x => x.Name.ToLower().Contains("audio.wav")).FirstOrDefault();
                            var bytes = File.ReadAllBytes(file.FullName);
                            _currentClip = WavUtility.ToAudioClip(bytes);
                        }
                        catch
                        {
                            Logger.log.Info("Audio DID NOT LOAD!");
                        }
                    }
                } 
            }
            StartCoroutine(WaitABit());
        }

        private IEnumerator WaitABit()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            bool hud360 = Resources.FindObjectsOfTypeAll<FlyingGameHUDRotation>().Any(x => x.isActiveAndEnabled);
            if (hud360)
                screenS.transform.SetParent(Resources.FindObjectsOfTypeAll<FlyingGameHUDRotation>().FirstOrDefault().transform, true);
            Logger.log.Info(hud360 ? "360 mode active" : "360 mode not active");

        }


        internal IEnumerator ActivateBreaktimeCoroutine(float length)
        {
            if (_currentClip != null)
            {
                AudioUtil.Instance.PlayAudioClip(_currentClip);
            }
            AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, .4f, 6f);
            float pos = 0f;
            float startTime = Time.time;

            float timeElapsed() => Time.time - startTime;
            float timeUntil() => length - timeElapsed();

            while (pos <= 1.48f)
            {
                yield return new WaitForSeconds(.015f);
                pos = curve.Evaluate(Mathf.Abs(timeElapsed())) - 4.5f;
                screenS.transform.localPosition = new Vector3(0f, pos, 3f);
                Timer = Math.Round(timeUntil(), 1).ToString();
            }
            screenS.transform.localPosition = new Vector3(0f, pos, 3f);
            pos = 1.5f;
            while (timeUntil() > 1.5f)
            {
                yield return new WaitForSeconds(.015f);
                Timer = Math.Round(timeUntil(), 1).ToString();
            }
                
            AnimationCurve curve2 = AnimationCurve.EaseInOut(1.5f, 1.5f, 0f, -4.5f);
            while (pos > -4.48f)
            {
                yield return new WaitForSeconds(.015f);
                pos = curve2.Evaluate(timeUntil());
                screenS.transform.localPosition = new Vector3(0f, pos, 3f);
                Timer = Math.Round(timeUntil(), 1).ToString();
            }
            screenS.transform.localPosition = new Vector3(0f, -100f, 3f);
            _activated = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _scoreController.noteWasCutEvent -= ScoreController_noteWasCutEvent;
            _scoreController.noteWasMissedEvent -= ScoreController_noteWasMissedEvent;
        }

        private void ScoreController_noteWasMissedEvent(NoteData arg1, int _) => NoteEvent(arg1);
        private void ScoreController_noteWasCutEvent(NoteData arg1, NoteCutInfo _, int __) => NoteEvent(arg1);

        private bool _activated;

        private void NoteEvent(NoteData noteData)
        {
            if (noteData.noteType == NoteType.Bomb)
                return;

            // Self destruct if the map is corrupted
            if (noteData.timeToNextBasicNote < 0 || noteData.timeToNextBasicNote > 999)
                Destroy(this);

            if (!_activated && noteData.timeToNextBasicNote >= Plugin.config.breaktime.MinimumBreakTime)
            {
                _activated = true;
                StartCoroutine(ActivateBreaktimeCoroutine(noteData.timeToNextBasicNote));
            }

        }

        internal static BreaktimeManager CreateBreaktimeScreen() //new Vector3(0f, 1.5f, 3f)
        {
            BreaktimeManager manager = BeatSaberUI.CreateViewController<BreaktimeManager>();
            manager.screenS = FloatingScreen.CreateFloatingScreen(new Vector2(50f, 50f), false, new Vector3(0f, 0f, 3f), Quaternion.Euler(Vector3.zero));
            manager.screenS.SetRootViewController(manager, true);
            manager.screenS.GetComponent<Image>().enabled = false;
            manager.screenS.ScreenPosition = new Vector3(0f, -100f, 3f);
            manager.Load();
            return manager;
        }

        public static DirectoryInfo[] GetValidBreaktimePaths()
        {
            List<DirectoryInfo> directories = new List<DirectoryInfo>();
            var path = IPA.Utilities.UnityGame.UserDataPath + "\\Breaktime";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return directories.ToArray();
            }
            DirectoryInfo mainDir = new DirectoryInfo(path);
            DirectoryInfo[] subDirs = mainDir.GetDirectories();
            for (int i = 0; i < subDirs.Length; i++)
            {
                DirectoryInfo workingDir = subDirs[i];
                FileInfo[] files = workingDir.GetFiles();
                bool imageExists = files.Any(x => x.Name.ToLower().Contains("image"));
                bool audioExists = files.Any(x => x.Name.ToLower().Contains("audio.wav"));
                if (imageExists || audioExists)
                    directories.Add(workingDir);
            }
            return directories.ToArray();
        }

        
    }
}
