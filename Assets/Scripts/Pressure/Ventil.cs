using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Pressure
{
    [RequireComponent(typeof(LinearMapping))]
    public class Ventil : MonoBehaviour
    {
        [Tooltip("Subject tank of which the maxInputCapacity is adjusted")]
        public SteamTank subject;
        [Tooltip("Maximum value to which the subjects maxInputCapacity is set")]
        public float maxCapacity = 1;
        [Tooltip("Minimum value to which the subjects maxInputCapacity is set")]
        public float minCapacity = 0;

        private LinearMapping _linearMapping;

        public void Start()
        {
            _linearMapping = GetComponent<LinearMapping>();

            if (subject == null)
                Debug.LogWarning($"{this.name} subject is null");
        }

        public void Update()
        {
            if (subject != null)
                subject.maxInputCapacity = ((maxCapacity - minCapacity) * _linearMapping.value) + minCapacity;
        }

    }
}
