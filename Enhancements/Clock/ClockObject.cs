using System;
using UnityEngine;
using System.Collections;
using Enhancements.Utilities;
using Message = Enhancements.Utilities.WorldSpaceMessage;

namespace Enhancements.Clock
{
    public class ClockObject : MonoBehaviour
    {
        public Message Text { get; set; }
        public bool Active { get; internal set; } = false;
        public string format = "h:mm tt";

        public void Activate()
        {
            if (!Active)
                StartCoroutine(UpdateClock());
        }

        public void Deactivate() => Active = false;

        private IEnumerator UpdateClock()
        {
            Active = true;
            Text.FontX = Extensions.ArcadePix;
            while (Active) //Oh yes papi chulo ping me every 1/4 seconds
            {
                Text.Text = DateTime.Now.ToString(format);
                if (format != "yyyy MM dd THH:mm:ss.fffffffK")
                    yield return new WaitForSecondsRealtime(.25f);
                else
                    yield return new WaitForEndOfFrame();
            }
            Active = false;
            Text.Text = "";
            Destroy(this);
        }

        public void ConfigSet(ClockConfig cfg)
        {
            if (Active)
            {
                Text.FontX = Enhancements.Instance.fontPairs[cfg.font];
                Text.Color = cfg.color.ToColor();
                Text.transform.localPosition = cfg.position.ToVector3();
                Text.transform.localRotation = Quaternion.Euler(cfg.rotation.ToVector3());
                Text._messagePrompt.fontSize = cfg.fontSize;
                format = cfg.format;
            }
        }
    }
}
