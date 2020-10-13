using TMPro;
using SemVer;
using Zenject;
using UnityEngine;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.UI
{
	[ViewDefinition("Enhancements.Views.info-view.bsml")]
	[HotReload(RelativePathToLayout = @"..\Views\info-view.bsml")]
    public class XInfoView : BSMLAutomaticViewController
    {
        [UIComponent("version-text")]
        protected TextMeshProUGUI _versionText;

        [UIComponent("secondary-text")]
        protected TextMeshProUGUI _secondaryText;

		private string _version = "v0.0.0";
		[UIValue("version")]
		internal string Version
		{
			get => _version;
			set
			{
				_version = value;
				NotifyPropertyChanged();
			}
		}

		private string _secondary = "by Auros";
		[UIValue("secondary")]
		internal string Secondary
		{
			get => _secondary;
			set
			{
				_secondary = value;
				NotifyPropertyChanged();
			}
		}

		private bool _updateAvailable;
		internal bool UpdateAvailable
		{
			get => _updateAvailable;
			set
			{
				_updateAvailable = value;
				if (_updateAvailable)
				{
					_versionText.color = _secondaryText.color = Color.white;
				}
				else
				{
					_versionText.color = new Color(1f, 0.89f, 0.89f);
					_secondaryText.color = new Color(1f, 1f, 0.349f);
				}
			}
		}

		[UIValue("changelog-text")]
		protected string Changelog => Constants.CHANGELOG;

		[Inject]
		public void Construct([Inject(Id = "Enhancements.Version")] Version version)
		{
			Version = $"v{version}";
		}

		[UIAction("secondary-clicked")]
		protected void SecondaryClicked()
		{
			var url = UpdateAvailable ? Constants.LATESTRELEASE : Constants.AUROSDEV;
			Application.OpenURL(url);
		}
	}
}