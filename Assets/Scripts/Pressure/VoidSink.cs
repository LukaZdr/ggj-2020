using UnityEngine;
using System.Collections;


namespace Pressure
{
    public class VoidSink : MonoBehaviour, ISteamSink
    {
        [Tooltip("How much steam this sink accepts (in liters per second)")]
        public float maxInputCapacity = 100;
        [SerializeField]
        [Tooltip("Where this sink receives its steam from")]
        public AbstractSteamSource source;

        public float sinkCapacity { get; private set; }

        public float SinkSteam(float amount)
        {
            amount = Mathf.Min(amount, sinkCapacity);
            sinkCapacity -= amount;
            return amount;
        }

        virtual protected void Start()
        {
            if (source == null)
                Debug.LogWarning($"{name} steamSource is null");

            source?.RegisterSink(this);
        }

        virtual protected void FixedUpdate()
        {
            sinkCapacity = Mathf.Min(sinkCapacity + maxInputCapacity * Time.fixedDeltaTime, maxInputCapacity);
        }
    }
}
