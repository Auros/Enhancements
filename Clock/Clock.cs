using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Enhancements.Clock
{
    public class Clock : MonoBehaviour
    {
        public static Clock _instance;
        public static Clock Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject managerGO = new GameObject("Clock");
                    _instance = managerGO.AddComponent<Clock>();
                    //_instance.Init();
                    DontDestroyOnLoad(managerGO);
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        private bool _clockEnabled = true;
        private GameObject _canvas = null;
        private TextMeshProUGUI _text = null;
        private Vector3 _timePos;
        private Quaternion _timeRot;
        private float _timeSize;
        private string _timeFormat = "h:mm tt";
        private Color _color = Color.white;

        public void Init(bool IsClockEnabled, Vector3 pos, Vector3 rot, float size, int timeValue, Color col)
        {
            _clockEnabled = IsClockEnabled;
            _timePos = pos;
            _timeRot = Quaternion.Euler(rot);
            _timeSize = size;
            _timeFormat = (string)timeType[timeValue];
            _color = col;

            CreateClock();
        }

        private void CreateClock()
        {
            Logger.log.Notice("CLOCK CREATED");
            _canvas = new GameObject("Clock Canvas");
            DontDestroyOnLoad(_canvas);
            _canvas.AddComponent<Canvas>();

            _canvas.transform.position = _timePos;
            _canvas.transform.rotation = _timeRot;

            _text = CustomUI.BeatSaber.BeatSaberUI.CreateText(_canvas.transform as RectTransform, "00:00", Vector2.zero);
            _text.alignment = TextAlignmentOptions.Center;
            _text.transform.localScale *= .02f;
            _text.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            _text.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            _text.fontSize = _timeSize;
            _text.color = _color;
            _text.material.renderQueue = 5000;

            if (_clockIsRunning == false)
                StartCoroutine(ClockManager());
        }

        private bool _clockIsRunning = false;

        private IEnumerator ClockManager()
        {
            _clockIsRunning = true;
            while (_instance != null && _clockEnabled == true) //Oh yes ping me every 1/4 seconds
            {
                _text.text = DateTime.Now.ToString(_timeFormat);
                yield return new WaitForSecondsRealtime(.25f); //This has almost no performance impact. Don't @ me
            }
            _clockIsRunning = false;
            _text.text = "";
            yield break;
        }

        public void UpdateClockState(bool enable)
        {
            if (_clockEnabled == enable)
                return;

            _clockEnabled = enable;

            if (_clockEnabled == false)
            {
                _text.text = "";
            }
                
            else if (_clockEnabled == true)
            {
                if (_clockIsRunning == false)
                    StartCoroutine(ClockManager());
            }
        }

        public void UpdatePos(Vector3 pos)
        {
            _canvas.transform.position = pos;
        }

        public void UpdateRot(Vector3 rot)
        {
            _canvas.transform.rotation = Quaternion.Euler(rot);
        }

        public void UpdateSize(float size)
        {
            _text.fontSize = size;
        }

        public void UpdateFormat(int place)
        {
            _timeFormat = (string)timeType[place];
        }

        public void UpdateColor(Color col)
        {
            _text.color = col;
        }

        public static readonly List<object> timeType = new List<object>()
        {
            "h:mm tt",
            "h:mm:ss tt",
            "HH:mm",
            "HH:mm:ss",
            "dddd"
        };

        /// <summary>
        /// Emergency Use Only!
        /// </summary>
        public void DestroyClock()
        {
            _instance = null;
            Destroy(this);
        }
    }
}
