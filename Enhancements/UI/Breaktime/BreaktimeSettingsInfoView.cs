using UnityEngine.UI;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI.Breaktime
{
    [ViewDefinition("Enhancements.Views.Breaktime.breaktime-settings-info-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\..\Views\Breaktime\breaktime-settings-info-view.bsml")]
    public class BreaktimeSettingsInfoView : BSMLAutomaticViewController
    {
        [UIComponent("example")]
        protected Image exampleImage;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
            if (firstActivation)
            {
                exampleImage.SetImage("http://cdn.auros.dev/sira/enhancements/breaktime.gif");
            }
        }
    }
}