using System;
using Zenject;
using System.Threading;
using System.Threading.Tasks;
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

		public async void Initialize()
		{
			await Task.Run(() => Thread.Sleep(100));
			MenuButtons.instance.RegisterButton(menuButton);
		}

		public void Dispose()
		{
			MenuButtons.instance?.UnregisterButton(menuButton);
		}

		private void SummonFlowCoordinator()
		{
			_mainFlowCoordinator.PresentFlowCoordinator(_settingsFlowCoordinator);
		}
	}
}
