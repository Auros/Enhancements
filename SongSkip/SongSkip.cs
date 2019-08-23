using System;
using System.Linq;
using UnityEngine;
using BS_Utils.Gameplay;
using System.Collections;
using Enhancements.Breaktime;
using IPA.Utilities;

namespace Enhancements.SongSkip
{
    public class SongSkip : MonoBehaviour
    {
        private static LevelData _mainGameSceneSetupData = null;
        Radial radial;
        private VRController _lC;
        private VRController _rC;
        private AudioTimeSyncController _audioTimeSyncController;

        private bool _skipIntro = true;
        private bool _skipOutro = false;
        private float _minIntroTime = 5f;
        private bool _radialEnabled = false;
        private bool _textEnabled = true;
        private Color _radialColor = new Color(1f, .639f, .964f);
        private float _songLength = -1f;

        public static void Load(bool SkipIntro, bool SkipOutro, float MinIntroTime, bool Radial, bool Text, Color Notif)
        {
            if (!SkipIntro && !SkipOutro)
                return;
            var ss = new GameObject("SongSkip").AddComponent<SongSkip>();
            ss.ApplySettings(SkipIntro, SkipOutro, MinIntroTime, Radial, Text, Notif);
        }

        public void ApplySettings(bool intro, bool outro, float min, bool rad, bool text, Color not)
        {
            _skipIntro = intro;
            _skipOutro = outro;
            _minIntroTime = min;
            _radialEnabled = rad;
            _textEnabled = text;
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
            yield return new WaitForSecondsRealtime(.5f);

            enumeratorsMakeMeWantDie = true;
            if (type == Skip.Intro)
            {
                if (_audioTimeSyncController.songTime > 5f)
                {
                    enumeratorsMakeMeWantDie = false;
                    yield break;
                }

                while (skipTo > _audioTimeSyncController.songTime)
                {
                    radial = new GameObject().AddComponent<Radial>();
                    radial.Create(_radialEnabled, _textEnabled);

                    //Fade in and fill radial
                    float percent = 0f;
                    while (percent < 1f)
                    {
                        string _text = $"<size=35%>Press Trigger </size><size=35%>To Skip</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        percent += .1f;
                        CheckSkip(skipTo);
                        radial.UpdateRadial(_text, percent, new Color(1f, 1f, 1f, Mathf.Clamp(percent + .2f, 0f, 1f)));
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                    float currentSeperation = skipTo - _audioTimeSyncController.songTime;
                    while (skipTo - 2f > _audioTimeSyncController.songTime) //TIME SCALE
                    {
                        string _text = $"<size=35%>Press Trigger </size><size=35%>To Skip</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        CheckSkip(skipTo);
                        radial.UpdateRadial(_text,  1f - (_audioTimeSyncController.songTime / currentSeperation), Color.white);
                        yield return new WaitForSecondsRealtime(.1f);
                    }
                    float coloradjust = 1f;
                    while (skipTo > _audioTimeSyncController.songTime)
                    {
                        string _text = $"{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        coloradjust -= .1f;
                        radial.UpdateRadial(_text, 1f - (_audioTimeSyncController.songTime / currentSeperation), new Color(1f, 1f, 1f, coloradjust));
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                }

                radial.DestroyRadial();
                radial = null;
            }
            else if (type == Skip.Outro)
            {
                while (skipTo > _audioTimeSyncController.songTime)
                {
                    radial = new GameObject().AddComponent<Radial>();
                    radial.Create(_radialEnabled, _textEnabled);

                    //Fade in and fill radial
                    float percent = 0f;
                    while (percent < 1f)
                    {
                        string _text = $"<size=35%>Press Trigger </size><size=35%>To Skip</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        percent += .05f;
                        CheckSkip(skipTo);
                        radial.UpdateRadial(_text, percent, new Color(_radialColor.r, _radialColor.g, _radialColor.b, Mathf.Clamp(percent + .2f, 0f, 1f)));
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                    float currentSeperation = _songLength - _audioTimeSyncController.songTime;
                    while (skipTo - 2f > _audioTimeSyncController.songTime) //TIME SCALE
                    {
                        string _text = $"<size=35%>Press Trigger </size><size=35%>To Skip</size>\n{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        CheckSkip(skipTo);
                        radial.UpdateRadial(_text, (_songLength - _audioTimeSyncController.songTime) / currentSeperation, _radialColor);
                        yield return new WaitForSecondsRealtime(.1f);
                    }
                    float coloradjust = 1f;
                    while (skipTo > _audioTimeSyncController.songTime)
                    {
                        string _text = $"{Mathf.Round(skipTo - _audioTimeSyncController.songTime)}";
                        coloradjust -= .05f;
                        radial.UpdateRadial(_text, (_songLength - _audioTimeSyncController.songTime) / currentSeperation, new Color(_radialColor.r, _radialColor.g, _radialColor.b, coloradjust));
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                }

                radial.DestroyRadial();
                radial = null;
            }
            enumeratorsMakeMeWantDie = false;
            yield break;
        }

        private void CheckSkip(float toSkipTo)
        {
            if (Controller(CT.Left).triggerValue == 1 || Controller(CT.Right).triggerValue == 1)
            {
                StartCoroutine(ActivateSkip(toSkipTo));
            }
        }

        private IEnumerator ActivateSkip(float toSkipTo)
        {
            yield return new WaitForSecondsRealtime(.01f);
            var _songAudio = _audioTimeSyncController.GetPrivateField<AudioSource>("_audioSource");
            _songAudio.time = toSkipTo - 1.5f;
        }

        private IEnumerator SetAudioLength()
        {
            yield return new WaitForSecondsRealtime(1f);
            var _songAudio = _audioTimeSyncController.GetPrivateField<AudioSource>("_audioSource");
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
