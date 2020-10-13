using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI.Misc
{
    [ViewDefinition("Enhancements.Views.Misc.misc-settings-info-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Misc\misc-settings-info-view.bsml")]
    public class MiscSettingsInfoView : BSMLAutomaticViewController
    {
    }
}