using System;
using Zenject;
using UnityEngine;
using System.Linq;
using Enhancements.Breaktime;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components.Settings;
using SiraUtil;

namespace Enhancements.UI.Breaktime
{
    [ViewDefinition("Enhancements.Views.Breaktime.breaktime-settings-profile-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Breaktime\breaktime-settings-profile-view.bsml")]
    public class BreaktimeSettingsProfileView : BSMLAutomaticViewController
    {
        private BreaktimeLoader _loader;
        private BreaktimeSettings _settings;

        public event Action ProfilesUpdated;

        private Profile _currentProfile = new Profile
        {
            ShowText = true,
            AudioPath = null,
            ImagePath = null,
            Name = "New Profile",
            TextColor = Color.white,
            ImageColor = Color.white,
            Animation = Animation.FadeIn
        };

        [UIParams]
        protected BSMLParserParams parserParams;

        [UIComponent("image-list")]
        protected DropDownListSetting imageDropdown;

        [UIComponent("audio-list")]
        protected DropDownListSetting audioDropdown;

        private string _buttonText = "Switch To Edit Mode";
        [UIValue("button-text")]
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                NotifyPropertyChanged();
            }
        }

        private bool _createMode = true;
        [UIValue("create-mode")]
        public bool CreateMode
        {
            get => _createMode;
            set
            {
                _createMode = value;
                NotifyPropertyChanged();
            }
        }

        private string _modeText = "Create New Profile";
        [UIValue("mode-text")]
        public string ModeText
        {
            get => _modeText;
            set
            {
                _modeText = value;
                NotifyPropertyChanged();
            }
        }

        private string _cod = "Create";
        [UIValue("cod")]
        public string COD
        {
            get => _cod;
            set
            {
                _cod = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("name")]
        protected string Name
        {
            get => _currentProfile.Name;
            set
            {
                _currentProfile.Name = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("color")]
        protected Color Color
        {
            get => _currentProfile.TextColor;
            set
            {
                _currentProfile.TextColor = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("show-text")]
        protected bool ShowText
        {
            get => _currentProfile.ShowText;
            set
            {
                _currentProfile.ShowText = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("image")]
        protected string Image
        {
            get => _currentProfile.ImagePath ?? "None";
            set => _currentProfile.ImagePath = value;
        }

        [UIValue("audio")]
        protected string Audio
        {
            get => _currentProfile.AudioPath ?? "None";
            set => _currentProfile.AudioPath = value;
        }

        [UIValue("animation")]
        protected Animation Animation
        {
            get => _currentProfile.Animation;
            set => _currentProfile.Animation = value;
        }

        [UIValue("opacity")]
        protected float Opacity
        {
            get => _currentProfile.ImageOpacity;
            set => _currentProfile.ImageOpacity = value;
        }


        [UIValue("image-options")]
        protected List<object> ImageOptions = new List<object>();

        [UIValue("audio-options")]
        protected List<object> AudioOptions = new List<object>();

        [UIValue("animation-options")]
        protected List<object> AnimationOptions => AnimateOptions();

        [Inject]
        public void Construct(BreaktimeLoader loader, BreaktimeSettings settings)
        {
            _loader = loader;
            _settings = settings;
            NewProfileMode();
            ReloadOptions();
        }

        [UIAction("#post-parse")]
        protected void Parsed()
        {
            NewProfileMode();
            ReloadOptions();
        }

        protected void ReloadOptions()
        {
            ImageOptions.Clear();
            AudioOptions.Clear();
            ImageOptions.Add("None");
            AudioOptions.Add("None");
            ImageOptions.AddRange(_loader.GetImageFileInfo().Select(x => x.Name));
            AudioOptions.AddRange(_loader.GetAudioFileInfo().Select(x => x.Name));

            imageDropdown?.UpdateChoices();
            audioDropdown?.UpdateChoices();
            parserParams?.EmitEvent("update-lists");
        }

        [UIAction("switch")]
        protected void Switch()
        {
            CreateMode = !CreateMode;
            ModeText = CreateMode ? "Create New Profile" : $"Editing <color=#f2493d>{_settings.SelectedProfile}</color>";
            ButtonText = CreateMode ? "Switch To Edit Mode" : "Switch To Create Mode";
            if (!CreateMode)
            {
                _currentProfile = _loader.SelectedProfile();
                ReloadOptions();
                parserParams?.EmitEvent("upd");
                parserParams?.EmitEvent("update-lists");
                COD = "Destroy";
            }
            else
            {
                NewProfileMode();
                COD = "Create";
            }
        }

        [UIAction("reset")]
        private void NewProfileMode()
        {
            _currentProfile = new Profile
            {
                ShowText = true,
                AudioPath = null,
                ImagePath = null,
                Name = "New Profile",
                TextColor = Color.white,
                ImageColor = Color.white,
                Animation = Animation.FadeIn
            };
            parserParams?.EmitEvent("upd");
            parserParams?.EmitEvent("update-lists");
            Name = _currentProfile.Name;
            Color = _currentProfile.TextColor;
            ShowText = _currentProfile.ShowText;
        }

        [UIAction("create-or-destroy")]
        protected void CreateOrDestroyProfile()
        {
            if (CreateMode)
            {
                _settings.Profiles.Add(_currentProfile);
                _settings.SelectedProfile = _settings.Profiles.Last().Name;
                NewProfileMode();
            }
            else
            {
                if (_currentProfile.Name == "Default" || 1 >= _settings.Profiles.Count)
                {
                    Switch();
                }
                _settings.Profiles.Remove(_currentProfile);
                _settings.SelectedProfile = _settings.Profiles.LastOrDefault().Name ?? "Default";
                Switch();
            }
            ProfilesUpdated?.Invoke();
        }

        [UIAction("format-animation")]
        protected string FormatRandom(Animation mode)
        {
            return BreaktimeSettingsGlobalView.Pascaler(mode.ToString());
        }

        [UIAction("percent")]
        public string PercentFormat(float value)
        {
            return value.ToString("P");
        }

        private List<object> AnimateOptions()
        {
            List<object> list = new List<object>();
            foreach (var item in Enum.GetValues(typeof(Animation)))
            {
                list.Add(item);
            }
            return list;
        }
    }
}