using TMPro;
using Zenject;
using UnityEngine;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.Clock
{
    [HotReload]
    public class BasicClockView : BSMLAutomaticViewController
    {
        private ELoader _loader;

        [UIComponent("clock-text")]
        protected TextMeshProUGUI _clockTextObject;

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

        [Inject]
        public void Construct(ELoader loader)
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