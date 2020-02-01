using UnityEngine;
using UnityEditor;


namespace Pressure
{
    public interface ISteamSink
    {
        /// <summary>
        /// How much steam in liters the sink would accept right now
        /// </summary>
        float sinkCapacity { get; }

        /// <summary>
        /// Receive an amount of pressure from another object.
        /// In order to actually receive pressure, this object should be registered at an IPressureSink
        /// </summary>
        /// <param name="amount">Amount of pressure in liters to receive</param>
        /// <returns>How much steam in liters was actually accepted</returns>
        float SinkSteam(float amount);
    }
}
