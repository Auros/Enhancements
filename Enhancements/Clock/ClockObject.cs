using System;
using UnityEngine;
using System.Collections;
using Enhancements.Utilities;
using Message = SiaUtil.Visualizers.WorldSpaceMessage;

namespace Enhancements.Clock
{
    public class ClockObject : MonoBehaviour
    {
        public Message text { get; set; }
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
            while (Active == true) //Oh yes papi chulo ping me every 1/4 seconds
            {
                text.Text = DateTime.Now.ToString(format);
                if (format != "yyyy MM dd THH:mm:ss.fffffffK")
                    yield return new WaitForSecondsRealtime(.25f);
                else
                    yield return new WaitForEndOfFrame();
            }
            Active = false;
            text.Text = "";
            Destroy(this);
        }

        public void ConfigSet(ClockConfig cfg)
        {
            if (Active)
            {
                text.Color = cfg.color.ToColor();
                text.transform.localPosition = cfg.position.ToVector3();
                text.transform.localRotation = Quaternion.Euler(cfg.rotation.ToVector3());
                text._messagePrompt.fontSize = cfg.fontSize;
                format = cfg.format;
            }
        }
    }
}
