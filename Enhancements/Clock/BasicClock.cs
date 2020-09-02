using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using BeatSaberMarkupLanguage.FloatingScreen;

namespace Enhancements.Clock
{
    public class BasicClock : IInitializable, IDisposable
    {
        private readonly BasicClockView _basicClockView;
        private readonly FloatingScreen _floatingScreen;
        private readonly IClockController _clockController;

        public BasicClock(BasicClockView basicClockView, IClockController clockController)
        {
            _basicClockView = basicClockView;
            _clockController = clockController;
            _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(100f, 50f), false, new Vector3(0f, 2.7f, 2.4f), Quaternion.identity);
            
        }
        public void Initialize()
        {
            _floatingScreen.GetComponent<Image>().enabled = false;
            _floatingScreen.SetRootViewController(_basicClockView, false);

            _clockController.DateUpdated += ClockController_DateUpdated;
        }

        public void Dispose()
        {
            _clockController.DateUpdated -= ClockController_DateUpdated;
        }

        private void ClockController_DateUpdated(DateTime time)
        {
            _basicClockView.ClockText = time.ToString();
        }
    }
}
