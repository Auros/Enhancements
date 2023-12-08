using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using BeatSaberMarkupLanguage.FloatingScreen;
using VRUIControls;
using IPA.Utilities;

namespace Enhancements.Clock
{
    public class BasicClock : IInitializable, IDisposable
    {
        private bool _disabled;
        private XLoader _loader;
        private FloatingScreen _floatingScreen;
        private readonly ClockSettings _clockSettings;
        private readonly BasicClockView _basicClockView;
        private readonly IClockController _clockController;
        private readonly PhysicsRaycasterWithCache _physicsRaycasterWithCache;

        public BasicClock(XLoader loader, ClockSettings clockSettings, BasicClockView basicClockView, IClockController clockController, PhysicsRaycasterWithCache physicsRaycasterWithCache)
        {
            _loader = loader;
            _clockSettings = clockSettings;
            _basicClockView = basicClockView;
            _clockController = clockController;
            _physicsRaycasterWithCache = physicsRaycasterWithCache;
        }

        public void Initialize()
        {
            _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(150f, 50f), false, _clockSettings.Position, Quaternion.Euler(_clockSettings.Rotation));
            _floatingScreen.GetComponent<VRGraphicRaycaster>().SetField("_physicsRaycaster", _physicsRaycasterWithCache);
            //_floatingScreen.GetComponent<Image>().enabled = false;
            _floatingScreen.SetRootViewController(_basicClockView, HMUI.ViewController.AnimationType.Out);

            _disabled = !_clockSettings.Enabled;
            _clockSettings.MarkDirty();
            ClockController_DateUpdated(DateTime.Now);
            _clockController.DateUpdated += ClockController_DateUpdated;
        }

        public void Dispose()
        {
            _clockController.DateUpdated -= ClockController_DateUpdated;
        }

        private void ClockController_DateUpdated(DateTime time)
        {
            if (_disabled && !_clockSettings.Enabled)
            {
                return;
            }
            else if (_disabled && _clockSettings.Enabled)
            {
                _floatingScreen.gameObject.SetActive(true);
                _disabled = false;
            }
            CultureInfo culture = string.IsNullOrEmpty(_clockSettings.Culture) ? CultureInfo.InvariantCulture : new CultureInfo(_clockSettings.Culture);
            if (_clockSettings.Enabled)
            {
                _basicClockView.ClockText = time.ToString(_clockSettings.Format, culture);
                if (_clockSettings.IsDirty)
                {
                    _basicClockView.ClockSize = _clockSettings.Size;
                    _basicClockView.Font = _loader.GetFont(_clockSettings.Font);
                    _basicClockView.ClockColor = addAlphaToColor(_clockSettings.Color, _clockSettings.Opacity);
                    _floatingScreen.ScreenPosition = _clockSettings.Position;
                    _floatingScreen.ScreenRotation = Quaternion.Euler(_clockSettings.Rotation);
                    _clockSettings.IsDirty = false;
                }
            }
            else
            {
                _disabled = true;
                _basicClockView.ClockText = "";
                _floatingScreen.gameObject.SetActive(false);
            }
        }

        private Color addAlphaToColor(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}