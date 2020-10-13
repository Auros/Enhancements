using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using Enhancements.Misc;
using Zenject;

namespace Enhancements.UI.Misc
{
    [ViewDefinition("Enhancements.Views.Misc.optidra-settings-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Misc\optidra-settings-view.bsml")]
    public class OptidraSettingsView : BSMLAutomaticViewController
    {
        private OptidraSettings _settings;

        [UIValue("enabled")]
        protected bool Enabled
        {
            get => _settings.Enabled;
            set => _settings.Enabled = value;
        }

        [UIValue("init-note-size")]
        protected int InitialNotePool
        {
            get => _settings.InitialNotePoolSize;
            set => _settings.InitialNotePoolSize = value;
        }

        [UIValue("init-bomb-size")]
        protected int InitialBombPool
        {
            get => _settings.InitialBombPoolSize;
            set => _settings.InitialBombPoolSize = value;
        }

        [UIValue("init-wall-size")]
        protected int InitialWallPool
        {
            get => _settings.InitialWallPoolSize;
            set => _settings.InitialWallPoolSize = value;
        }

        [Inject]
        public void Construct(OptidraSettings settings)
        {
            _settings = settings;
        }
    }
}