using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Enhancements.MiniTweaks
{
    public class ButtonLock : MonoBehaviour
    {
        public void ButtonLocker(Button[] buttons)
        {
            foreach (Button button in buttons)
                button.interactable = false;
            StartCoroutine(RunButtonLock(buttons));
        }

        public IEnumerator RunButtonLock(Button[] buttons)
        {
            yield return new WaitForSecondsRealtime(Plugin.config.minitweaks.buttonlocktime);
            foreach (Button button in buttons)
                button.interactable = true;
        }
    }
}
