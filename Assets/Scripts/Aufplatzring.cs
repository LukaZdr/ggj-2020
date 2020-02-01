using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufplatzring : MonoBehaviour
{
    [SerializeField]
    GameObject boltPlaceholder;
    [SerializeField]
    float deleteDelay = 10f;

    // Im Erfolgsfall ist other = Bolzen
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bolt")
        {
            Destroy(other.gameObject);
            boltPlaceholder.SetActive(true);
            this.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, deleteDelay);
        }
    }
}
