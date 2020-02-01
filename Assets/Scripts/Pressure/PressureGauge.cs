using UnityEngine;
using System.Collections;

namespace Pressure
{
    public class PressureGauge : MonoBehaviour
    {
        public float minRotation = 0;
        public float maxRoation = 180;

        [SerializeField]
        public AbstractSteamSource pressureSource;

        private Vector3 _rotation;

        private void Start()
        {
            _rotation = transform.localEulerAngles;
        }

        void FixedUpdate()
        {
            if (pressureSource != null)
            {
                _rotation.x = (pressureSource.pressure / 100 * (maxRoation - minRotation)) + minRotation;
                transform.localEulerAngles = _rotation;
            }
        }
    }
}
