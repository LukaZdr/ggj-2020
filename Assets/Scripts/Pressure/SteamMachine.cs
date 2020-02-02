using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pressure
{
    public class SteamMachine : VoidSink
    {
        [Tooltip("How much steam this machine draws at maximum (in liters per second)")]
        public float maxDraw = 5;
        [Tooltip("How much steam this machine draws at minimum (in liters per second)")]
        public float minDraw = 0.5f;

        override protected void Start()
        {
            base.Start();
            maxInputCapacity = (maxDraw + minDraw) / 2;
        }

        override protected void FixedUpdate()
        {
            base.FixedUpdate();
            maxInputCapacity = Mathf.Min(maxDraw, maxInputCapacity + Random.Range(-0.01f, 0.01f));
        }
    }
}
