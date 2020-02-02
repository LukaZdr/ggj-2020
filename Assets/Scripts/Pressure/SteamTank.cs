using UnityEngine;
using System.Collections;


namespace Pressure
{
    [RequireComponent(typeof(AudioSource))]
    public class SteamTank : AbstractSteamSource, ISteamSink
    { 
        [Tooltip("How much steam this tank accepts (in liters per second)")]
        public float maxInputCapacity = 1;
        [SerializeField]
        [Tooltip("Where this tank receives its steam from")]
        public AbstractSteamSource source;
        [SerializeField]
        public SteamContainer tank = new SteamContainer(100, 0);
        [Tooltip("Time which this tank needs before it can accept steam again after a blowout")]
        public float blowoutTimeout = 20;

        private float blowoutTime = 0;

        /// <summary>
        /// How much steam this sink would accept right now (in liters pers second)
        /// </summary>
        public float sinkCapacity { get; private set; }

        /// <summary>
        /// How much pressure this tank is currently under
        /// </summary>
        public override float pressure => tank.fillPercent;

        private bool blewnOut => blowoutTime + blowoutTimeout < Time.time;

        private AudioSource[] _audioSources;

        public float SinkSteam(float amount)
        {
            amount = Mathf.Min(amount, sinkCapacity);
            tank.stored += amount;
            sinkCapacity -= amount;
            return amount;
        }

        private void Start()
        {
            blowoutTime = -blowoutTimeout;
            sinkCapacity = maxInputCapacity;
            source?.RegisterSink(this);

            _audioSources = GetComponents<AudioSource>();
        }

        private void FixedUpdate()
        {
            var x = DistributeSteam(tank.stored);
            tank.stored -= x;

            if (!blewnOut)
            {
                // capacity regenerates up to a maximum of maxInputCapacity and remaining tank size
                sinkCapacity = Mathf.Min(sinkCapacity + maxInputCapacity * Time.fixedDeltaTime, tank.size - tank.stored, maxInputCapacity);
            } else
            {
                sinkCapacity = 0;
            }

            if (tank.stored == tank.size)
            {
                // blowout
                tank.stored = 0;
                blowoutTime = Time.time;
                foreach (var source in _audioSources)
                    source.Play();
            }
        }
    }
}
