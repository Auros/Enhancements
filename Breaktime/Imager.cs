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
    public class Imager : MonoBehaviour
    {
        

        static Image _panelLeft;
        private Canvas _imageCanvas;
        public void Create()
        {

            gameObject.transform.position = new Vector3(0f, 0f, 0f);
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            _imageCanvas = gameObject.AddComponent<Canvas>();
            _imageCanvas.renderMode = RenderMode.WorldSpace;
            
            var rectTransform = _imageCanvas.transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(100, 50);

            _panelLeft = new GameObject("Imager").AddComponent<Image>();
            _panelLeft.material = CustomUI.Utilities.UIUtilities.NoGlowMaterial;
            _panelLeft.rectTransform.SetParent(_imageCanvas.transform, false);
            _panelLeft.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _panelLeft.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            _panelLeft.rectTransform.anchoredPosition = new Vector2(4.5f, 4.5f);
            _panelLeft.rectTransform.sizeDelta = new Vector2(15f, 15f);
            _panelLeft.rectTransform.position = new Vector3(0f, 1.4f, 6f);
            _panelLeft.sprite = CustomUI.Utilities.UIUtilities.BlankSprite;

            _panelLeft.material.color = new Color(1, 1, 1, 1);

            _panelLeft.mainTexture.wrapMode = TextureWrapMode.Clamp;
            _panelLeft.material.renderQueue = 4000;
            
        }

        public void ChangeOpacity(float value)
        {
            _panelLeft.material.color = new Color(1, 1, 1, value);
        }

        public void EnableImager(Sprite sprite)
        {
            _panelLeft.sprite = sprite;
        }
        public void DisableImager()
        {
            _panelLeft.sprite = CustomUI.Utilities.UIUtilities.BlankSprite;
        }

        public void DestroyImager()
        {
            DisableImager();
            Destroy(_panelLeft);
            Destroy(_imageCanvas);
            Destroy(this);
        }

        
    }
}
