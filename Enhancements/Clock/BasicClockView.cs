using TMPro;
using Zenject;
using UnityEngine;
using Enhancements.Timers;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.Clock
{
    [ViewDefinition("Enhancements.Views.Clock.basic-clock.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\Clock\basic-clock.bsml")]
    public class BasicClockView : BSMLAutomaticViewController
    {
        private XLoader _loader;
        private TimerSettings _timerSettings;
        private NewReminderView _newReminderView;

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
        public void Construct(XLoader loader, TimerSettings timerSettings, NewReminderView newReminderView)
        {
            _loader = loader;
            _timerSettings = timerSettings;
            _newReminderView = newReminderView;
        }

        [UIAction("#post-parse")]
        protected void Parsed()
        {
            var mat = new Material(_clockTextObject.material) { shader = _loader.ZFixTextShader };
            _clockTextObject.material = mat;
        }

        [UIAction("clicked")]
        protected void ClickedClock()
        {
            if (_timerSettings.Enabled)
            {
                _newReminderView.Visible = true;
            }
        }
    }
}