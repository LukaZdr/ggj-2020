using UnityEngine;
using System;


namespace Pressure
{
    [Serializable]
    public class SteamContainer
    {
        [Tooltip("How much steam in liters can fit in this tank")]
        public float size;
        [Tooltip("Amount of steam in liters currently stored")]
        public float stored;

        public float fillPercent => stored / size * 100;

        public SteamContainer(float capacity, float initialStore)
        {
            this.size = capacity;
            stored = initialStore;
        }
    }
}
