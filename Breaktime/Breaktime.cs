using CustomUI.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Enhancements.Breaktime
{
    public class BreakTime : MonoBehaviour
    {
        private ScoreController _scoreController;
        private AudioTimeSyncController _audioTimeSyncController;
        private AudioSource audioSource;
        private List<AudioClip> audioClip = new List<AudioClip>();
        private List<Sprite> _icon = new List<Sprite>();

        private bool radialEnabled = true;
        private bool timerEnabled = true;
        private bool imageEnabled = true;
        private bool audioEnabled = true;
        private float timeScale = 5f;
        private Color radialColor = Color.white;
        private int vmode = 0;
        private System.Random random = new System.Random();
        private float baseVol = 1f;
        public static void Load(bool IsRadialEnabled, bool IsTimerEnabled, Color RadialColor, int VisualizationMode, bool IsImageEnabled, bool IsAudioEnabled, float BreakTime)
        {
            var breakTime = new GameObject("BreakTime").AddComponent<BreakTime>();
            breakTime.ApplySettings(IsRadialEnabled, IsTimerEnabled, RadialColor, VisualizationMode, IsImageEnabled, IsAudioEnabled, BreakTime);
        }

        private void ApplySettings(bool radial, bool timer, Color radcol, int mode, bool image, bool audio, float breaktime)
        {
            radialEnabled = radial;
            timerEnabled = timer;
            radialColor = radcol;
            vmode = mode;
            imageEnabled = image;
            audioEnabled = audio;
            timeScale = breaktime / 5f;

            LoadAudio();
            LoadImages();

            baseVol = Plugin.baseGameVolume;
        }

        private void Awake()
        {
            audioClip = new List<AudioClip>();
            _icon = new List<Sprite>();
            _scoreController = Resources.FindObjectsOfTypeAll<ScoreController>().FirstOrDefault();
            _audioTimeSyncController = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().FirstOrDefault();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = baseVol;
        }

        private void LoadAudio()
        {
            if (audioEnabled == false)
                return;

            var path = Environment.CurrentDirectory.Replace('\\', '/') + "/UserData/Lifeline/Breaktime";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (vmode == 0)
            {
                audioClip.Add(InternalAudioLoader("arrow.Awesome"));
                audioClip.Add(InternalAudioLoader("arrow.Excelent"));
                audioClip.Add(InternalAudioLoader("arrow.Good"));
                audioClip.Add(InternalAudioLoader("arrow.Great"));
                audioClip.Add(InternalAudioLoader("arrow.Nice"));
                audioClip.Add(InternalAudioLoader("arrow.Perfect"));
            }
            else if (vmode == 1)
            {
                audioClip.Add(InternalAudioLoader("sectionpass"));
            }
            else if (vmode == 2)
            {
                audioClip.Add(InternalAudioLoader("bobbie1"));
                audioClip.Add(InternalAudioLoader("bobbie2"));
            }
            else if (vmode == 3)
            {
                foreach (string file in Directory.GetFiles(path + "/", "*.wav"))
                {
                    StartCoroutine(LoadAudio(file));
                }

                //if (audioClip.Count() == 0)
                //    audioClip.Add(InternalAudioLoader("arrow.Nice"));
            }
        }

        IEnumerator LoadAudio(string audiopath)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audiopath, AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.isHttpError || www.isNetworkError)
                {
                    Logger.log.Error("There was an oof while loading audio: " + audiopath);
                    yield break;
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    if (clip == null)
                        yield break;
                    
                    audioClip.Add(clip);
                }
            }
        }

        private AudioClip InternalAudioLoader(string name)
        {
            byte[] clipData = UIUtilities.GetResource(Assembly.GetExecutingAssembly(), $"Enhancements.Resources.{name}.wav");
            return WavUtility.ToAudioClip(clipData);
        }

        private void LoadImages()
        {
            if (imageEnabled == false)
                return;

            var path = Environment.CurrentDirectory.Replace('\\', '/') + "/UserData/Lifeline/Breaktime";

            if (vmode == 0)
                _icon.Add(UIUtilities.LoadSpriteFromResources("Enhancements.Resources.arrow.png"));
            else if (vmode == 1)
                _icon.Add(UIUtilities.LoadSpriteFromResources("Enhancements.Resources.sectionpass.png"));
            else if (vmode == 2)
                _icon.Add(UIUtilities.LoadSpriteFromResources("Enhancements.Resources.bobbie.png"));
            else if (vmode == 3)
            {
                

                foreach (string file in Directory.GetFiles(path + "/", "*.png"))
                {
                    Sprite spr = null;
                    spr = UIUtilities.LoadSpriteFromFile(file);

                    if (spr != null)
                        _icon.Add(spr);

                }
                foreach (string file in Directory.GetFiles(path + "/", "*.jpg"))
                {
                    Sprite spr = null;
                    spr = UIUtilities.LoadSpriteFromFile(file);

                    if (spr != null)
                        _icon.Add(spr);
                }

                if (_icon.Count() == 0)
                    _icon.Add(UIUtilities.LoadSpriteFromResources("Enhancements.Resources.arrow.png"));
            }

            Logger.log.Info(vmode.ToString());
        }

        private void OnEnable()
        {
            _scoreController.noteWasCutEvent += _scoreController_noteWasCutEvent;
            _scoreController.noteWasMissedEvent += _scoreController_noteWasMissedEvent;
        }

        private void NoteEvent(NoteData noteData)
        {
            if (noteData.noteType == NoteType.Bomb)
                return;

            if (noteData.timeToNextBasicNote < timeScale * 5f)
                return;

            _toNext = noteData.timeToNextBasicNote + _audioTimeSyncController.songTime;

            if (noteData.timeToNextBasicNote > 400000f)
                return;

            if (!IHateEnumerators)
                StartCoroutine(Timer(noteData.timeToNextBasicNote));

            if (!MaybeEnumeratorsArentSoBad && !IWasWrongEnumeratorsAreSatan)
                AnimPlayer(vmode);
        }

        private float _toNext = 0f;
        private float _timeUntil;
        private float _countUp;
        private float _cloned;

        bool IHateEnumerators = false;
        bool MaybeEnumeratorsArentSoBad = false;
        bool IWasWrongEnumeratorsAreSatan = false;

        Imager imager;
        Radial radial;
        private void AnimPlayer(int place)
        {
            MaybeEnumeratorsArentSoBad = true;
            IWasWrongEnumeratorsAreSatan = true;
            Invoke("InvokeMeDaddy", .67f);

            if (imageEnabled == true || audioEnabled == true)
            {
                if (place == 0)
                    StartCoroutine(DefaultAnim());
                else if (place == 1)
                    StartCoroutine(OsuAnim());
                else if (place == 2)
                    StartCoroutine(BobbieAnim());
                else if (place == 3)
                    StartCoroutine(CustomAnim());
                else
                    StartCoroutine(DefaultAnim());
            }
        }

        private void InvokeMeDaddy()
        {
            IWasWrongEnumeratorsAreSatan = false;
        }

        private IEnumerator DefaultAnim()
        {
            MaybeEnumeratorsArentSoBad = true;
            Logger.log.Notice("Playing Default Anim");
            if (imageEnabled == true)
            {
                imager = new GameObject().AddComponent<Imager>();
                imager.Create();
            }
            yield return new WaitForSeconds(.6f * timeScale);
            if (imageEnabled == true)
                imager.EnableImager(_icon[0]);

            if (audioEnabled == true)
            {
                int randomAudioClip = random.Next(audioClip.Count);
                audioSource.volume = .6f * baseVol;
                audioSource.PlayOneShot(audioClip[randomAudioClip]);
                audioSource.volume = baseVol;
            }

            int inc = 0;
            while (inc < 10)
            {
                inc++;

                yield return new WaitForSeconds(.05f * timeScale);
                if (imageEnabled == true)
                    imager.ChangeOpacity(inc / 10f);
            }
            int decrease = 25;
            while (decrease > 0)
            {
                decrease--;

                yield return new WaitForSeconds(.06f * timeScale);
                if (imageEnabled == true)
                    imager.ChangeOpacity(decrease / 25f);
            }
            if (imageEnabled == true)
            {
                imager.DisableImager();
                imager.DestroyImager();
                imager = null;
            }
            
            MaybeEnumeratorsArentSoBad = false;
            yield return null;
        }

        private IEnumerator OsuAnim()
        {
            MaybeEnumeratorsArentSoBad = true;
            Logger.log.Notice("Playing Osu Anim");
            if (imageEnabled == true)
                imager = new GameObject().AddComponent<Imager>();
            if (imageEnabled == true)
                imager.Create();
            int randomAudioClip = random.Next(audioClip.Count);

            if (audioEnabled == true)
                audioSource.PlayOneShot(audioClip[randomAudioClip]);
            if (imageEnabled == true)
                imager.EnableImager(_icon[0]);
            yield return new WaitForSeconds(.27f * timeScale);
            if (imageEnabled == true)
                imager.DisableImager();
            yield return new WaitForSeconds(.4f * timeScale);
            if (imageEnabled == true)
                imager.EnableImager(_icon[0]);
            yield return new WaitForSeconds(1.6f * timeScale);
            int decrease = 10;
            while (decrease > 0)
            {
                decrease--;

                yield return new WaitForSeconds(.1f * timeScale);
                if (imageEnabled == true)
                    imager.ChangeOpacity(decrease / 10f);
            }
            if (imageEnabled == true)
                imager.DisableImager();
            if (imageEnabled == true)
                imager.DestroyImager();
            if (imageEnabled == true)
                imager = null;
            MaybeEnumeratorsArentSoBad = false;
            yield return null;
        }

        private IEnumerator BobbieAnim()
        {
            MaybeEnumeratorsArentSoBad = true;
            Logger.log.Notice("Playing Bobbie Anim");
            if (imageEnabled == true)
                imager = new GameObject().AddComponent<Imager>();
            if (imageEnabled == true)
                imager.Create();
            yield return new WaitForSeconds(.6f * timeScale);
            int randomAudioClip = random.Next(audioClip.Count);
            if (audioEnabled == true)
                audioSource.volume = .6f;
            if (audioEnabled == true)
                audioSource.PlayOneShot(audioClip[randomAudioClip]);
            if (audioEnabled == true)
                audioSource.volume = 1f;
            if (randomAudioClip == 0)
            {
                yield return new WaitForSeconds(.54f * timeScale);
                if (imageEnabled == true)
                    imager.EnableImager(_icon[0]);
            }
            else
            {
                if (imageEnabled == true)
                    imager.EnableImager(_icon[0]);
            }
            int decrease = 22;
            while (decrease > 0)
            {
                decrease--;

                yield return new WaitForSeconds(.08f * timeScale);
                if (imageEnabled == true)
                    imager.ChangeOpacity(decrease / 22f);
            }
            if (imageEnabled == true)
                imager.DisableImager();
            if (imageEnabled == true)
                imager.DestroyImager();
            if (imageEnabled == true)
                imager = null;
            MaybeEnumeratorsArentSoBad = false;
            yield return null;
        }
        private IEnumerator CustomAnim()
        {
            MaybeEnumeratorsArentSoBad = true;
            Logger.log.Notice("Playing Custom Anim");
            if (imageEnabled == true)
                imager = new GameObject().AddComponent<Imager>();
            if (imageEnabled == true)
                imager.Create();
            int randomAudioClip = random.Next(audioClip.Count);
            int randomIcon = random.Next(_icon.Count);
            if (audioEnabled == true)
                audioSource.PlayOneShot(audioClip[randomAudioClip]);
            yield return new WaitForSeconds(.75f * timeScale);
            if (imageEnabled == true)
                imager.EnableImager(_icon[randomIcon]);
            if (imageEnabled == true)
                imager.ChangeOpacity(0f);
            int inc = 0;
            while (inc < 10)
            {
                inc++;

                yield return new WaitForSeconds(.05f * timeScale);
                if (imageEnabled == true)
                    imager.ChangeOpacity(inc / 10f);
            }

            yield return new WaitForSeconds(2f * timeScale);

            int decrease = 10;
            while (decrease > 0)
            {
                decrease--;

                yield return new WaitForSeconds(.1f * timeScale);
                if (imageEnabled == true)
                    imager.ChangeOpacity(decrease / 10f);
            }
            if (imageEnabled == true)
                imager.DisableImager();
            if (imageEnabled == true)
                imager.DestroyImager();
            if (imageEnabled == true)
                imager = null;
            MaybeEnumeratorsArentSoBad = false;
            yield return null;
        }

        private IEnumerator Timer(float time, float count = 0)
        {
            IHateEnumerators = true;

            radial = new GameObject().AddComponent<Radial>();
            radial.Create(radialEnabled, timerEnabled);

            _timeUntil = time;
            _cloned = time - .75f;

            _countUp = count;

            while (_timeUntil - .75f > 3)
            {
                _timeUntil = _toNext - _audioTimeSyncController.songTime;
                _countUp += .01f;
                radial.UpdateRadial(Math.Round(_timeUntil - .75f, 2).ToString(), (_toNext - _audioTimeSyncController.songTime - .75f) / _cloned, new Color(radialColor.r, radialColor.g, radialColor.b, Mathf.Clamp(_countUp, .25f, 1f)));
                yield return new WaitForSeconds(.01f * timeScale);

            }
            float nonred = 1;
            float nonr = radialColor.r;
            float nong = radialColor.g;
            float nonb = radialColor.b;
            
            while (_timeUntil - .75f > .5f)
            {
                nonr -= .01f;
                nong -= .01f;
                nonb -= .01f;
                nonred -= .01f;
                _timeUntil = _toNext - _audioTimeSyncController.songTime;
                _countUp += .01f;

                var clamedNonRed = Mathf.Clamp(nonred + .4f, 0f, 1f);
                radial.UpdateRadial(Math.Round(_timeUntil - .75f, 2).ToString(), (_toNext - _audioTimeSyncController.songTime - .75f) / _cloned, new Color(1, nong, nonb, clamedNonRed + .15f));
                yield return new WaitForSeconds(.01f * timeScale);
            }

            while (_timeUntil - .75f > 0f)
            {
                nonr -= .01f;
                nong -= .01f;
                nonb -= .01f;
                nonred -= .01f;
                _timeUntil = _toNext - _audioTimeSyncController.songTime;
                _countUp += .01f;

                var clamedNonRed = Mathf.Clamp(nonred + .4f, 0f, 1f) + .15f;
                radial.UpdateRadial(Math.Round(_timeUntil - .75f, 2).ToString(), (_toNext - _audioTimeSyncController.songTime - .75f) / _cloned, new Color(1, nong, nonb, clamedNonRed));
                yield return new WaitForSeconds(.01f * timeScale);
            }

            IHateEnumerators = false;
            radial.DestroyRadial();
            radial = null;
            yield return null;
        }

        private void _scoreController_noteWasMissedEvent(NoteData arg1, int arg2)
        {
            NoteEvent(arg1);
        }

        private void _scoreController_noteWasCutEvent(NoteData arg1, NoteCutInfo arg2, int arg3)
        {
            NoteEvent(arg1);
        }

        private void OnDisable()
        {
            _scoreController.noteWasCutEvent -= _scoreController_noteWasCutEvent;
            _scoreController.noteWasMissedEvent -= _scoreController_noteWasMissedEvent;
            if (imager != null)
                Destroy(imager);
            if (radial != null)
                Destroy(radial);
        }


        public static readonly List<object> animType = new List<object>()
        {
            "Default",
            "Osu",
            "Bobbie",
            "Custom"
        };
    }
}
