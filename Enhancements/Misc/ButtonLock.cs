using Zenject;
using UnityEngine;
using IPA.Utilities;
using UnityEngine.UI;
using System.Collections;

namespace Enhancements.Misc
{
    public class ButtonLock : MonoBehaviour, IInitializable
    {
        private MiscSettings _miscSettings;
        private PauseMenuManager _pauseMenuManager;
        private IMenuButtonTrigger _menuButtonTrigger;
        private MultiplayerLocalActivePlayerInGameMenuViewController _multiplayerLocalActivePlayerInGameMenuViewController;
        private bool _menuPrev;
        private bool _restartPrev;
        private bool _continuePrev;

        private Button _menuButton;
        private Button _restartButton;
        private Button _continueButton;

        private static readonly FieldAccessor<PauseMenuManager, Button>.Accessor MenuButton = FieldAccessor<PauseMenuManager, Button>.GetAccessor("_backButton");
        private static readonly FieldAccessor<PauseMenuManager, Button>.Accessor RestartButton = FieldAccessor<PauseMenuManager, Button>.GetAccessor("_restartButton");
        private static readonly FieldAccessor<PauseMenuManager, Button>.Accessor ContinueButton = FieldAccessor<PauseMenuManager, Button>.GetAccessor("_continueButton");

        private static readonly FieldAccessor<MultiplayerLocalActivePlayerInGameMenuViewController, Button>.Accessor DisconnectButton = FieldAccessor<MultiplayerLocalActivePlayerInGameMenuViewController, Button>.GetAccessor("_disconnectButton");
        private static readonly FieldAccessor<MultiplayerLocalActivePlayerInGameMenuViewController, Button>.Accessor GiveUpButton = FieldAccessor<MultiplayerLocalActivePlayerInGameMenuViewController, Button>.GetAccessor("_giveUpButton");
        private static readonly FieldAccessor<MultiplayerLocalActivePlayerInGameMenuViewController, Button>.Accessor ResumeButton = FieldAccessor<MultiplayerLocalActivePlayerInGameMenuViewController, Button>.GetAccessor("_resumeButton");

        [Inject]
        public void Construct(MiscSettings miscSettings, [InjectOptional] PauseMenuManager pauseMenuManager, IMenuButtonTrigger menuButtonTrigger, [InjectOptional] MultiplayerLocalActivePlayerInGameMenuViewController multiplayerLocalActivePlayerInGameMenuViewController)
        {
            _miscSettings = miscSettings;
            _pauseMenuManager = pauseMenuManager;
            _menuButtonTrigger = menuButtonTrigger;
            _multiplayerLocalActivePlayerInGameMenuViewController = multiplayerLocalActivePlayerInGameMenuViewController;
        }

        public void Initialize()
        {
            _menuButtonTrigger.menuButtonTriggeredEvent += Paused;
            if (_pauseMenuManager != null)
            {
                _menuButton = MenuButton(ref _pauseMenuManager);
                _restartButton = RestartButton(ref _pauseMenuManager);
                _continueButton = ContinueButton(ref _pauseMenuManager);
            }
            else if (_multiplayerLocalActivePlayerInGameMenuViewController != null)
            {
                _menuButton = DisconnectButton(ref _multiplayerLocalActivePlayerInGameMenuViewController);
                _menuButton = GiveUpButton(ref _multiplayerLocalActivePlayerInGameMenuViewController);
                _menuButton = ResumeButton(ref _multiplayerLocalActivePlayerInGameMenuViewController);
            }
        }

        private void Paused()
        {
            if (_pauseMenuManager != null || _multiplayerLocalActivePlayerInGameMenuViewController != null)
            {
                _menuPrev = _menuButton.interactable;
                _restartPrev = _restartButton.interactable;
                _continuePrev = _continueButton.interactable;

                _menuButton.interactable = !_miscSettings.ButtonLockMenu;
                _restartButton.interactable = !_miscSettings.ButtonLockRestart;
                _continueButton.interactable = !_miscSettings.ButtonLockContinue;

                StartCoroutine(MakeInteractable());
            }
        }

        private IEnumerator MakeInteractable()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            _menuButton.interactable = _menuPrev;
            _restartButton.interactable = _restartPrev;
            _continueButton.interactable = _continuePrev;
        }

        protected void OnDestroy()
        {
            _menuButtonTrigger.menuButtonTriggeredEvent -= Paused;
        }
    }
}