using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Message = SiaUtil.Visualizers.WorldSpaceMessage;

namespace Enhancements.Clock
{
    public class ClockObject : MonoBehaviour
    {
        public Message text { get; set; }
        public bool Active { get; set; } = false;

        public void Activate()
        {
            if (!Active)
                StartCoroutine(UpdateClock());
        }

        public void Deactivate() => Active = false;

        private IEnumerator UpdateClock()
        {
            Active = true;
            while (Active == true) //Oh yes ping me every 1/4 seconds
            {
                text.Text = DateTime.Now.ToString("h:mm:ss tt");
                yield return new WaitForSecondsRealtime(.25f);
            }
            Active = false;
            text.Text = "";
            Destroy(this);
        }
    }
}
