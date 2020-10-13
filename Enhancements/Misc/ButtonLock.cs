using Zenject;
using UnityEngine;
using IPA.Utilities;
using UnityEngine.UI;
using System.Collections;

namespace Enhancements.Misc
{
    public class ButtonLock : MonoBehaviour
    {
        private MiscSettings _miscSettings;
        private PauseController _pauseController;
        private PauseMenuManager _pauseMenuManager;

        private bool _menuPrev;
        private bool _restartPrev;
        private bool _continuePrev;

        private Button _menuButton;
        private Button _restartButton;
        private Button _continueButton;

        private static readonly FieldAccessor<PauseMenuManager, Button>.Accessor MenuButton = FieldAccessor<PauseMenuManager, Button>.GetAccessor("_backButton");
        private static readonly FieldAccessor<PauseMenuManager, Button>.Accessor RestartButton = FieldAccessor<PauseMenuManager, Button>.GetAccessor("_restartButton");
        private static readonly FieldAccessor<PauseMenuManager, Button>.Accessor ContinueButton = FieldAccessor<PauseMenuManager, Button>.GetAccessor("_continueButton");

        [Inject]
        public void Construct(MiscSettings miscSettings, PauseController pauseController, PauseMenuManager pauseMenuManager)
        {
            _miscSettings = miscSettings;
            _pauseController = pauseController;
            _pauseMenuManager = pauseMenuManager;
        }

        protected void Start()
        {
            _pauseController.didPauseEvent += Paused;
            _menuButton = MenuButton(ref _pauseMenuManager);
            _restartButton = RestartButton(ref _pauseMenuManager);
            _continueButton = ContinueButton(ref _pauseMenuManager);
        }

        private void Paused()
        {
            _menuPrev = _menuButton.interactable;
            _restartPrev = _restartButton.interactable;
            _continuePrev = _continueButton.interactable;

            _menuButton.interactable = !_miscSettings.ButtonLockMenu;
            _restartButton.interactable = !_miscSettings.ButtonLockRestart;
            _continueButton.interactable = !_miscSettings.ButtonLockContinue;

            StartCoroutine(MakeInteractable());
        }

        private IEnumerator MakeInteractable()
        {
            yield return new WaitForSecondsRealtime(0.75f);
            _menuButton.interactable = _menuPrev;
            _restartButton.interactable = _restartPrev;
            _continueButton.interactable = _continuePrev;
        }

        protected void OnDestroy()
        {
            _pauseController.didPauseEvent -= Paused;
        }
    }
}
