using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalConsumer : MonoBehaviour
{
    [SerializeField]
    CoalAndFire coalAndFire;

    void OnTriggerEnter(Collider other)
    {
        var container = other.GetComponent<CoalContainer>();
        if (container)
        {
            coalAndFire.AddCoalLevel(container.Consume());
        }
    }

}
