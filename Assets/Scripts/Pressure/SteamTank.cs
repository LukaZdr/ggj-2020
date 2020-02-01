using UnityEngine;
using System.Collections;


namespace Pressure
{
    public class SteamTank : AbstractSteamSource, ISteamSink
    { 
        [Tooltip("How much steam this tank accepts (in liters per second)")]
        public float maxInputCapacity = 1;
        [SerializeField]
        [Tooltip("Where this tank receives its steam from")]
        public AbstractSteamSource source;
        [SerializeField]
        public SteamContainer tank = new SteamContainer(100, 0);

        /// <summary>
        /// How much steam this sink would accept right now (in liters pers second)
        /// </summary>
        public float sinkCapacity { get; private set; }

        /// <summary>
        /// How much pressure this tank is currently under
        /// </summary>
        public override float pressure => tank.fillPercent;

        public float SinkSteam(float amount)
        {
            amount = Mathf.Min(amount, sinkCapacity);
            tank.stored += amount;
            sinkCapacity -= amount;
            return amount;
        }

        private void Start()
        {
            sinkCapacity = maxInputCapacity;
            source?.RegisterSink(this);
        }

        private void FixedUpdate()
        {
            tank.stored -= DistributeSteam(tank.stored);

            // capacity regenerates up to a maximum of maxInputCapacity and remaining tank size
            sinkCapacity = Mathf.Min(sinkCapacity + maxInputCapacity * Time.fixedDeltaTime, tank.size - tank.stored, maxInputCapacity);
        }
    }
}
