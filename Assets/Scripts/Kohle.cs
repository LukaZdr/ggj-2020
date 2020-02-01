using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kohle : MonoBehaviour
{
    // Im Erfolgsfall ist other = Ofen
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ofen")
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
