using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enhancements.GameAdjustments
{
    public class ButtonLock : MonoBehaviour
    {
        public void ButtonLocker(Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
            StartCoroutine(RunButtonLock(buttons));
        }

        public IEnumerator RunButtonLock(Button[] buttons)
        {
            yield return new WaitForSecondsRealtime(EnhancementsManager.Settings.GameAdjustments.ButtonLockTime);
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
    }
}
