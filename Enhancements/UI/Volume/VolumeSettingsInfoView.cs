using Zenject;
using Enhancements.Volume;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using System.Linq;
using IPA.Utilities;

namespace Enhancements.UI.Volume
{
    [ViewDefinition("Enhancements.Views.Volume.volume-settings-info-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Volume\volume-settings-info-view.bsml")]
    public class VolumeSettingsInfoView : BSMLAutomaticViewController
    {
        private VolumeSettings _settings;
        private MenuVolumeManager _menuVolume;
        private FireworksController _fireworksController;
        private FireworkItemController.Pool _fireworkPool;
        private static readonly FieldAccessor<FireworksController, FireworkItemController.Pool>.Accessor FireworkPool = FieldAccessor<FireworksController, FireworkItemController.Pool>.GetAccessor("_fireworkItemPool");

        [UIValue("good-cut")]
        protected float GoodCut
        {
            get => _settings.GoodCuts;
            set => _settings.GoodCuts = value;
        }

        [UIValue("bad-cut")]
        protected float BadCut
        {
            get => _settings.BadCuts;
            set => _settings.BadCuts = value;
        }

        [UIValue("music")]
        protected float Music
        {
            get => _settings.Music;
            set => _settings.Music = value;
        }

        [UIValue("song-preview")]
        protected float Preview
        {
            get => _settings.SongPreview;
            set
            {
                _settings.SongPreview = value;
                _menuVolume.SetMenuPreviewVolume(value);
            }
        }

        [UIValue("background")]
        protected float Background
        {
            get => _settings.MenuBackground;
            set
            {
                _settings.MenuBackground = value;
                _menuVolume.SetMenuAmbienceVolume(value);
            }
        }

        [UIValue("fireworks")]
        protected float Fireworks
        {
            get => _settings.Fireworks;
            set => _settings.Fireworks = value;
        }

        [Inject]
        public void Construct(VolumeSettings settings, MenuVolumeManager menuVolume, FireworksController fireworkController)
        {
            _settings = settings;
            _menuVolume = menuVolume;
            _fireworksController = fireworkController;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {

            _fireworkPool = FireworkPool(ref _fireworksController);
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
            var items = _fireworkPool.InactiveItems;
            for (int i = 0; i < items.Count(); i++)
            {
                var controller = items.ElementAt(i);
                FireworkSoundEffectSwapper.GetFireworkControllerAudioSource(ref controller).volume = 0.6f * _settings.Fireworks;
            }
        }

        [UIAction("percent")]
        public string PercentFormat(float value)
        {
            return value.ToString("P");
        }
    }
}