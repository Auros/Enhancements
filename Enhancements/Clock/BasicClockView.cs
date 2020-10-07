using TMPro;
using Zenject;
using UnityEngine;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.Clock
{
    [HotReload(RelativePathToLayout = @"..\Views\Clock\basic-clock.bsml")]
    public class BasicClockView : BSMLAutomaticViewController
    {
        private XLoader _loader;

        [UIComponent("clock-text")]
        protected ClickableText _clockTextObject;

        private string _clockText;
        [UIValue("clock-text")]
        public string ClockText
        {
            get => _clockText;
            set
            {
                _clockText = value;
                NotifyPropertyChanged();
            }
        }

        private float _clockSize = 10f;
        [UIValue("clock-size")]
        public float ClockSize
        {
            get => _clockSize;
            set
            {
                _clockSize = value;
                NotifyPropertyChanged();
            }
        }

        public Color ClockColor
        {
            set
            {
                _clockTextObject.DefaultColor = value;
                _clockTextObject.color = value;
            }
        }

        public TMP_FontAsset Font
        {
            set => _clockTextObject.font = value;
        }

        [Inject]
        public void Construct(XLoader loader)
        {
            _loader = loader;
        }

        [UIAction("#post-parse")]
        protected void Parsed()
        {
            var mat = new Material(_clockTextObject.material) { shader = _loader.ZFixTextShader };
            _clockTextObject.material = mat;
        }
    }
}