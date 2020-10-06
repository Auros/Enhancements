using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using BeatSaberMarkupLanguage.FloatingScreen;
using System.Globalization;

namespace Enhancements.Clock
{
    public class BasicClock : IInitializable, IDisposable
    {
        private FloatingScreen _floatingScreen;
        private readonly ClockSettings _clockSettings;
        private readonly BasicClockView _basicClockView;
        private readonly IClockController _clockController;

        public BasicClock(ClockSettings clockSettings, BasicClockView basicClockView, IClockController clockController)
        {
            _clockSettings = clockSettings;
            _basicClockView = basicClockView;
            _clockController = clockController;
        }

        public void Initialize()
        {
            _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(150f, 50f), false, new Vector3(0f, 2.7f, 2.4f), Quaternion.identity);
            _floatingScreen.GetComponent<Image>().enabled = false;
            _floatingScreen.SetRootViewController(_basicClockView, false);

            _basicClockView.ClockText = _clockController.GetCurrentTime().ToString();
            _clockController.DateUpdated += ClockController_DateUpdated;
        }

        public void Dispose()
        {
            _clockController.DateUpdated -= ClockController_DateUpdated;
        }

        private void ClockController_DateUpdated(DateTime time)
        {
            CultureInfo culture = string.IsNullOrEmpty(_clockSettings.Culture) ? CultureInfo.InvariantCulture : new CultureInfo(_clockSettings.Culture);

            _basicClockView.ClockText = time.ToString(_clockSettings.Format, culture);
        }
    }
}
