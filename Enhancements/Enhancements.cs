using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Enhancements.Clock;
using SiaUtil.Visualizers;

namespace Enhancements
{
    public class Enhancements : MonoBehaviour
    {
        public static Enhancements Instance { get; set; }

        public void Awake() => Instance = this;
        
        public void SetupAll()
        {
            InitializeClock();
        }


        #region Clock
        public static ClockObject ClockInstance { get; set; }

        public void InitializeClock()
        {
            if (ClockInstance == null)
            {
                var clock = new GameObject("[E2] - Clock").AddComponent<ClockObject>();
                DontDestroyOnLoad(clock.gameObject);
                clock.text = WorldSpaceMessage.Create("00:00", Vector2.zero);
                clock.text.transform.SetParent(clock.gameObject.transform);
                var go = clock.gameObject;
                go.transform.position = new Vector3(0f, 3f, 2f);
                ClockInstance = clock;

                ClockInstance.Activate();
            }
        }
        #endregion
    }
}
