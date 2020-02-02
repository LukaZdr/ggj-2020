using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoalContainer))]
public class Shovel : MonoBehaviour
{
    [SerializeField]
    GameObject kohle;
    [SerializeField]
    float coalValue = 10;

    private CoalContainer coalContainer;

    void Start()
    {
        coalContainer = this.GetComponent<CoalContainer>();
    }
    
    void FixedUpdate()
    {
        if (coalContainer.coalValue <= 0)
        {
            kohle.SetActive(false);
        }
    }

        // Im Erfolgsfall ist other = kohlehaufen
        void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kohlehaufen")
        {
            kohle.SetActive(true);
            coalContainer.coalValue = coalValue;
        }
    }
}
