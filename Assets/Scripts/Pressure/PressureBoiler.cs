using UnityEngine;
using System;

namespace Pressure
{
    public class PressureBoiler : AbstractSteamSource
    {
        [Tooltip("How much liters of steam per second this boiler generates")]
        public float generationRate = 1f;
        [SerializeField]
        public SteamContainer tank = new SteamContainer(100, 0);

        public override float pressure => tank.fillPercent;

        void FixedUpdate()
        {
            tank.stored = Mathf.Clamp(tank.stored + generationRate * Time.fixedDeltaTime, 0, tank.size);
            var x = DistributeSteam(tank.stored);
            tank.stored -= x;
        }
    }
}
