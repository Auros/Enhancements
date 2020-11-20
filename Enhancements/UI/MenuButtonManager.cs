using System;
using Zenject;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;

namespace Enhancements.UI
{
    public class MenuButtonManager : IInitializable, IDisposable
	{
		private readonly MenuButton menuButton;
		private readonly MainFlowCoordinator _mainFlowCoordinator;
		private readonly XSettingsFlowCoordinator _settingsFlowCoordinator;

		public MenuButtonManager(MainFlowCoordinator mainFlowCoordinator, XSettingsFlowCoordinator settingsFlowCoordinator)
		{
			_mainFlowCoordinator = mainFlowCoordinator;
			_settingsFlowCoordinator = settingsFlowCoordinator;
			menuButton = new MenuButton("Enhancements", SummonFlowCoordinator);
		}

		public void Initialize()
		{
			MenuButtons.instance.RegisterButton(menuButton);
		}

		public void Dispose()
		{
			if (MenuButtons.IsSingletonAvailable && BSMLParser.IsSingletonAvailable)
            {
				MenuButtons.instance.UnregisterButton(menuButton);
            }
		}

		private void SummonFlowCoordinator()
		{
			_mainFlowCoordinator.PresentFlowCoordinator(_settingsFlowCoordinator);
		}
	}
}
