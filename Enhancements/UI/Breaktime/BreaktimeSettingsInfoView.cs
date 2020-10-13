using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI.Breaktime
{
    [ViewDefinition("Enhancements.Views.Breaktime.breaktime-settings-info-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Breaktime\breaktime-settings-info-view.bsml")]
    public class BreaktimeSettingsInfoView : BSMLAutomaticViewController
    {
    }
}