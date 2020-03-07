using BS_Utils.Gameplay;
using SiaUtil.Visualizers;
using System.Collections;
using System.Linq;
using UnityEngine;
using IPA.Utilities;
using UnityEngine.UI;

namespace Enhancements.SongSkip
{
    public class SongSkip : MonoBehaviour
    {
        private static LevelData _mainGameSceneSetupData = null;
        WorldSpaceRadial radial;
        private VRController _lC;
        private VRController _rC;
        private AudioTimeSyncController _audioTimeSyncController;

        private bool _skipIntro = true;
        private bool _skipOutro = false;
        private float _minIntroTime = 5f;
        private Color _radialColor = new Color(1f, .639f, .964f);
        private float _songLength = -1f;

        public static void Load(bool SkipIntro, bool SkipOutro, float MinIntroTime, Color Notif)
        {
            if (!SkipIntro && !SkipOutro || Gamemode.IsIsolatedLevel)
                return;
            var ss = new GameObject("[E2] - SongSkip").AddComponent<SongSkip>();
            ss.ApplySettings(SkipIntro, SkipOutro, MinIntroTime, Notif);
        }

        public void ApplySettings(bool intro, bool outro, float min, Color not)
        {
            _skipIntro = intro;
            _skipOutro = outro;
            _minIntroTime = min;
            _radialColor = not;
        }

        private void Start()
        {
            _mainGameSceneSetupData = BS_Utils.Plugin.LevelData;
            _audioTimeSyncController = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().FirstOrDefault();

            StartCoroutine(SetAudioLength());
            MapAnalyzer();

            
        }

        private VRController Controller(CT type)
        {
            if (type == CT.Left)
            {
                if (_lC == null)
                    _lC = Resources.FindObjectsOfTypeAll<VRController>().Where(x => x.ToString() == "ControllerLeft (VRController)").FirstOrDefault();
                return _lC;

            }
            else if (type == CT.Right)
            {
                if (_rC == null)
                    _rC = Resources.FindObjectsOfTypeAll<VRController>().Where(x => x.ToString() == "ControllerRight (VRController)").FirstOrDefault();
                return _rC;
            }
            return null;
        }

        private void MapAnalyzer()
        {
            float firstObjectTime = 10000f;
            float lastObjectTime = 0f;

            foreach (BeatmapLineData lineData in _mainGameSceneSetupData.GameplayCoreSceneSetupData.difficultyBeatmap.beatmapData.beatmapLinesData)
            {
                foreach (BeatmapObjectData objectData in lineData.beatmapObjectsData)
                {
                    if (objectData.beatmapObjectType == BeatmapObjectType.Note)
                    {
                        if (objectData.time < firstObjectTime)
                            firstObjectTime = objectData.time;
                        if (objectData.time > lastObjectTime)
                            lastObjectTime = objectData.time;
                    }
                    else if (objectData.beatmapObjectType == BeatmapObjectType.Obstacle)
                    {
                        ObstacleData obstacle = (ObstacleData)objectData;
                        if (obstacle.lineIndex == 0 && obstacle.width == 1)
                        {

                        }
                        else if (obstacle.lineIndex == 3 && obstacle.width == 1)
                        {

                        }
                        else if (obstacle.duration <= 0 || obstacle.width <= 0)
                        {
                            if (objectData.time < firstObjectTime)
                                firstObjectTime = objectData.time;
                            if (objectData.time > lastObjectTime)
                                lastObjectTime = objectData.time;
                        }
                        else
                        {
                            if (objectData.time > lastObjectTime)
                                lastObjectTime = objectData.time;
                            if (objectData.time < firstObjectTime)
                                firstObjectTime = objectData.time;
                        }
                    }
                }
            }

            if (!enumeratorsMakeMeWantDie && _skipIntro && firstObjectTime >= _minIntroTime)
                StartCoroutine(Generator(Skip.Intro, firstObjectTime));
            if (_skipOutro)
                outroTime = lastObjectTime;
        }

        float outroTime = -1f;
        bool enumeratorsMakeMeWantDie = false;

        private void Update()
        {
            if (_skipOutro)
            {
                if (_audioTimeSyncController.songTime > outroTime && _songLength - outroTime > 4f)
                {
                    _skipOutro = false;
                    StartCoroutine(Generator(Skip.Outro, _songLength));
                }

            }
        }

        private IEnumerator Generator(Skip type, float skipTo)
        {
            yield return new WaitForSecondsRealtime(.75f);

            bool hud360 = Resources.FindObjectsOfTypeAll<FlyingGameHUDRotation>().Any(x => x.isActiveAndEnabled);
            enumeratorsMakeMeWantDie = true;
            if (type == Skip.Intro)
            {
                if (_audioTimeSyncController.songTime > 5f)
                {
                    enumeratorsMakeMeWantDie = false;
                    yield break;
                }
                Image image = Resources.FindObjectsOfTypeAll<ScoreMultiplierUIController>().First().GetField<Image, ScoreMultiplierUIController>("_multiplierProgressImage");
                radial = WorldSpaceRadial.Create(null, image.sprite);
                radial.transform.localPosition = new Vector3(0, 2.7f, 3.3f);
                radial.transform.localRotation = Quaternion.Euler(new Vector3(-35, 0, 0));
                if (hud360)
                    radial.transform.SetParent(Resources.FindObjectsOfTypeAll<FlyingGameHUDRotation>().FirstOrDefault()?.transform, true);
                
                while (skipTo > _audioTimeSyncController.songTime)
                {
                    //Fade in and fill radial
                    float percent = 0f;
                    while (percent < 1f)
                    {
                        string _text = $"<size=35%>Loading...</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        percent += .1f;
                        radial.Text = _text;
                        radial.Color = new Color(_radialColor.r, _radialColor.g, _radialColor.b, Mathf.Clamp(percent + .2f, 0f, 1f));
                        radial.Progress = percent;
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                    float currentSeperation = skipTo - _audioTimeSyncController.songTime;
                    while (skipTo - 2f > _audioTimeSyncController.songTime) //TIME SCALE
                    {
                        string _text = $"<size=35%>Press Trigger </size><size=35%>To Skip</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        CheckSkip(skipTo);
                        radial.Text = _text;
                        radial.Color = _radialColor;
                        radial.Progress = 1f - (_audioTimeSyncController.songTime / currentSeperation);
                        yield return new WaitForSecondsRealtime(.1f);
                    }
                    float coloradjust = 1f;
                    while (skipTo > _audioTimeSyncController.songTime)
                    {
                        string _text = $"{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        coloradjust -= .1f;
                        radial.Text = _text;
                        radial.Color = new Color(_radialColor.r, _radialColor.g, _radialColor.b, coloradjust);
                        radial.Progress = 1f - (_audioTimeSyncController.songTime / currentSeperation);
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                }

                Destroy(radial);
                radial = null;
            }
            else if (type == Skip.Outro)
            {
                Image image = Resources.FindObjectsOfTypeAll<ScoreMultiplierUIController>().First().GetField<Image, ScoreMultiplierUIController>("_multiplierProgressImage");
                radial = WorldSpaceRadial.Create(null, image.sprite);


                if (hud360)
                    radial.transform.SetParent(Resources.FindObjectsOfTypeAll<FlyingGameHUDRotation>().FirstOrDefault()?.transform, false);
                radial.transform.localPosition = new Vector3(0, 2.7f, 3.3f);
                radial.transform.localRotation = Quaternion.Euler(new Vector3(-35, 0, 0));
                while (skipTo > _audioTimeSyncController.songTime)
                {

                    //Fade in and fill radial
                    float percent = 0f;
                    while (percent < 1f)
                    {
                        string _text = $"<size=35%>Press Trigger </size><size=35%>To Skip</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        percent += .05f;
                        CheckSkip(skipTo);
                        radial.Text = _text;
                        radial.Progress = percent;
                        radial.Color = new Color(_radialColor.r, _radialColor.g, _radialColor.b, Mathf.Clamp(percent + .2f, 0f, 1f));
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                    float currentSeperation = _songLength - _audioTimeSyncController.songTime;
                    while (skipTo - 2f > _audioTimeSyncController.songTime) //TIME SCALE
                    {
                        string _text = $"<size=35%>Press Trigger </size><size=35%>To Skip</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        CheckSkip(skipTo);
                        radial.Text = _text;
                        radial.Progress = (_songLength - _audioTimeSyncController.songTime) / currentSeperation;
                        radial.Color = _radialColor;
                        yield return new WaitForSecondsRealtime(.1f);
                    }
                    float coloradjust = 1f;
                    while (skipTo > _audioTimeSyncController.songTime)
                    {
                        string _text = $"{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        coloradjust -= .05f;
                        radial.Text = _text;
                        radial.Progress = (_songLength - _audioTimeSyncController.songTime) / currentSeperation;
                        radial.Color = new Color(_radialColor.r, _radialColor.g, _radialColor.b, coloradjust);
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                }

                Destroy(radial);
                radial = null;
            }
            enumeratorsMakeMeWantDie = false;
            yield break;
        }

        private void CheckSkip(float toSkipTo)
        {
            if (Controller(CT.Left).triggerValue > .95 || Controller(CT.Right).triggerValue > .95)
            {
                StartCoroutine(ActivateSkip(toSkipTo));
            }
        }

        private IEnumerator ActivateSkip(float toSkipTo)
        {
            var _songAudio = _audioTimeSyncController.GetField<AudioSource, AudioTimeSyncController>("_audioSource");
            _songAudio.time = toSkipTo - 1.5f;
            yield return null;
        }

        private IEnumerator SetAudioLength()
        {
            yield return new WaitForSecondsRealtime(1f);
            var _songAudio = _audioTimeSyncController.GetField<AudioSource, AudioTimeSyncController>("_audioSource");
            _songLength = _songAudio.clip.length;
        }

        private enum CT
        {
            Unknown,
            Left,
            Right
        }

        private enum Skip
        {
            Intro,
            Outro
        }
    }
}
