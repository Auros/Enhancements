using Zenject;
using UnityEngine;
using Enhancements.Clock;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI.Clock
{
    [HotReload(RelativePathToLayout = @"..\..\Views\Clock\clock-settings-pos-col-view.bsml")]
    public class ClockSettingsPosColView : BSMLAutomaticViewController
    {
        private ClockSettings _settings;

        [UIValue("pos-x")]
        protected float PosX
        {
            get => _settings.Position.x;
            set => _settings.Position = new Vector3(value, _settings.Position.y, _settings.Position.z);
        }

        [UIValue("pos-y")]
        protected float PosY
        {
            get => _settings.Position.y;
            set => _settings.Position = new Vector3(_settings.Position.x, value, _settings.Position.z);
        }

        [UIValue("pos-z")]
        protected float PosZ
        {
            get => _settings.Position.z;
            set => _settings.Position = new Vector3(_settings.Position.x, _settings.Position.y, value);
        }

        [UIValue("rot-x")]
        protected float RotX
        {
            get => _settings.Rotation.x;
            set => _settings.Rotation = new Vector3(value, _settings.Rotation.y, _settings.Rotation.z);
        }

        [UIValue("rot-y")]
        protected float RotY
        {
            get => _settings.Rotation.y;
            set => _settings.Rotation = new Vector3(_settings.Rotation.x, value, _settings.Rotation.z);
        }


        [UIValue("rot-z")]
        protected float RotZ
        {
            get => _settings.Rotation.z;
            set => _settings.Rotation = new Vector3(_settings.Rotation.x, _settings.Rotation.y, value);
        }

        [UIValue("color")]
        protected Color Color
        {
            get => _settings.Color;
            set => _settings.Color = value;
        }

        [UIValue("opacity")]
        protected float Opacity
        {
            get => _settings.Opacity;
            set => _settings.Opacity = value;
        }

        [Inject]
        public void Construct(ClockSettings settings)
        {
            _settings = settings;
            
        }
    }
}