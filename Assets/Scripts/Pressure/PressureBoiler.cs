using UnityEngine;
using System;

namespace Pressure
{
    [RequireComponent(typeof(SpawnOnSurface))]
    public class PressureBoiler : AbstractSteamSource
    {
        [Tooltip("How much liters of steam per second this boiler generates")]
        public float generationRate = 1f;
        public float maxGenerationRate = 10;
        public float minGenerationRate = 0;
        [SerializeField]
        public SteamContainer tank = new SteamContainer(100, 0);

        [Tooltip("Pressure percentage from which leaking starts")]
        public float startLeakingAt = 50;
        [Tooltip("Minimum time to wait between leakage spawns (affects spawnrate at maximum leakage)")]
        public float minLeakInterval = 2;
        [Tooltip("Maximum time to wait between leakage spawns (affects spawnrate at minimum leakage)")]
        public float maxLeakInterval = 30;

        public override float pressure => tank.fillPercent;

        private SpawnOnSurface _spawner;

        void Start()
        {
            _spawner = GetComponent<SpawnOnSurface>();
        }

        void FixedUpdate()
        {
            var previousStored = tank.stored;
            tank.stored = Mathf.Clamp(tank.stored + generationRate * Time.fixedDeltaTime, 0, tank.size);
            tank.stored -= DistributeSteam(tank.stored);

            GameManager.instance.GeneratedSteam(tank.stored - previousStored);

            if (pressure >= startLeakingAt)
            {
                _spawner.enabled = true;
                _spawner.spawnInterval = (int)(maxLeakInterval - ((maxLeakInterval - minLeakInterval) * pressure / 100));
            } else
            {
                _spawner.enabled = false;
            }
        }
    }
}
