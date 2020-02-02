using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


namespace Pressure
{
    public abstract class AbstractSteamSource : MonoBehaviour
    {
        protected IList<ISteamSink> _sinks = new List<ISteamSink>();

        public abstract float pressure { get; }

        /// <summary>
        /// Register a new sink which receives pressure from this source
        /// </summary>
        public void RegisterSink(ISteamSink sink)
        {
            _sinks.Add(sink);
        }

        /// <summary>
        /// Distribute amount of steam to sources
        /// </summary>
        /// <param name="amount">Amount of steam in liters which get evenly distributed between peers</param>
        /// <return>How much steam in liters was actually distributed</return>
        protected float DistributeSteam(float amount)
        {
            // Query how much steam each sink would take right now
            float[] sinkTakes = new float[_sinks.Count];
            for (int i = 0; i < _sinks.Count; i++)
                sinkTakes[i] = _sinks[i].sinkCapacity;
            float totalTakes = sinkTakes.Sum();

            // Calculate a fair distribution based on a sinks share on the total amount of available Capacity
            var sinkGets = sinkTakes.Select(takes => amount * (totalTakes / takes)).ToArray();

            // Actually distribute steam
            float actuallyTaken = 0;
            for (int i = 0; i < _sinks.Count; i++)
                actuallyTaken += _sinks[i].SinkSteam(sinkGets[i]);
            return actuallyTaken;
        }
    }
}
