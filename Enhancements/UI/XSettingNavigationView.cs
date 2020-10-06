using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI
{
    [HotReload(RelativePathToLayout = @"..\Views\settings-navigation-view.bsml")]
    public class XSettingNavigationView : BSMLAutomaticViewController
    {
        [UIComponent("list")]
        protected CustomListTableData tableList;

        [UIAction("#post-parse")]
        protected void Parsed()
        {
            var tex = BeatSaberMarkupLanguage.Utilities.FindTextureInAssembly("Enhancements.Resources.732426069508096091.png");
            tableList.data.AddRange(new CustomListTableData.CustomCellInfo[]
            {
                new CustomListTableData.CustomCellInfo
                (
                    "Changelog\n",
                    "The changelog for Enhancements",
                    tex
                    
                ),
                new CustomListTableData.CustomCellInfo
                (
                    "Clock\n",
                    "Modify the Clock",
                    tex
                ),
                new CustomListTableData.CustomCellInfo
                (
                    "Timers\n",
                    "Create Reminders In Game!",
                    tex
                ),
                new CustomListTableData.CustomCellInfo
                (
                    "Breaktime\n",
                    "Get Information During Song Breaks!",
                    tex
                ),
                new CustomListTableData.CustomCellInfo
                (
                    "Volume\n",
                    "Change Specific Volume Settings",
                    tex
                ),
                new CustomListTableData.CustomCellInfo
                (
                    "Mini Settings and Optidra\n",
                    "Miscellaneous Tweaks and Experimental Settings",
                    tex
                )
            });
            tableList.tableView.ReloadData();
        }
    }
}