using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enhancements.Breaktime
{
    public class Radial : MonoBehaviour
    {
        private TextMeshProUGUI _time;
        private GameObject _timer;
        private Image _radialImage;

        private bool radialEnabled = true;
        private bool timerEnabled = true;

        public void Create(bool radial, bool timer)
        {
            radialEnabled = radial;
            timerEnabled = timer;

            if (!radialEnabled && !timerEnabled)
                return;

            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
            canvasScaler.scaleFactor = 10.0f;
            canvasScaler.dynamicPixelsPerUnit = 10f;
            _ = gameObject.AddComponent<GraphicRaycaster>();
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            Image image = Resources.FindObjectsOfTypeAll<ScoreMultiplierUIController>().First().GetPrivateField<Image>("_multiplierProgressImage");

            _time = CustomUI.BeatSaber.BeatSaberUI.CreateText(canvas.transform as RectTransform, "", Vector2.zero);
            _time.alignment = TextAlignmentOptions.Center;
            _time.transform.localScale *= .12f;
            _time.fontSize = 2.5f;
            _time.color = Color.white;
            _time.lineSpacing = -25f;
            _time.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            _time.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            _time.enableWordWrapping = false;
            _time.transform.localPosition = new Vector3(0f, -.9f, 3.5f);
            _time.transform.localEulerAngles = new Vector3(50f, 0f, 0f);
            
            if (radialEnabled)
            {
                _timer = new GameObject();
                _radialImage = _timer.AddComponent<Image>();
                _timer.transform.SetParent(_time.transform);
                _timer.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
                _timer.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
                _timer.transform.localScale = new Vector3(4, 4, 4);
                _timer.transform.localPosition = Vector3.zero;

                _radialImage.sprite = image?.sprite;
                _radialImage.type = Image.Type.Filled;
                _radialImage.fillMethod = Image.FillMethod.Radial360;
                _radialImage.fillOrigin = (int)Image.Origin360.Top;
                _radialImage.fillClockwise = true;
                _radialImage.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        public void UpdateRadial(string time, float percent, Color color)
        {
            if (!radialEnabled && !timerEnabled)
                return;

            if (timerEnabled == true)
            {
                _time.color = color;
                _time.text = time;
            }
            if (radialEnabled == true)
            {
                _radialImage.color = color;
                _radialImage.fillAmount = percent;
            }
        }

        public void DestroyRadial()
        {
            if (!radialEnabled && !timerEnabled)
                return;

            if (timerEnabled)
                Destroy(_time);

            if (radialEnabled)
            {
                Destroy(_radialImage);
                Destroy(_timer);
            }
            Destroy(this);
        }
    }
}