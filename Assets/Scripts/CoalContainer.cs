using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoalContainer : MonoBehaviour
{
    public float coalValue = 1f;
    public bool destroyOnConsumption = true;

    public float Consume()
    {
        var _coalValue = coalValue;
        if (destroyOnConsumption)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            coalValue = 0;
        }
        return _coalValue;
    }
}
