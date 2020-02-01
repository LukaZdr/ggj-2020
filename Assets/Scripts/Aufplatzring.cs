using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufplatzring : MonoBehaviour
{
    [SerializeField]
    GameObject boltPlaceholder;
    [SerializeField]
    float deleteDelay = 10f;
    [SerializeField]
    ParticleSystem[] particles = new ParticleSystem[2];

    private bool leaking = true;
    private GameManager _gm;

    void Start()
    {
        leaking = true;
        _gm = GameManager.instance;
    }

    void Update()
    {
        if (leaking)
        {
            _gm.Leaking(Time.deltaTime);
        }
    }

    // Im Erfolgsfall ist other = Bolzen
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bolt" && leaking)
        {
            leaking = false;

            // Score
            _gm.PlacedBolt();

            // Bolt
            Destroy(other.gameObject);
            boltPlaceholder.SetActive(true);
            this.GetComponent<Collider>().enabled = false;

            //Animations
            foreach (var ps in particles)
            {
                var em = ps.emission;
                em.rateOverTime = 0;
            }
            this.GetComponent<Animator>().enabled = true;

            //bye bye
            Destroy(this.gameObject, deleteDelay);
        }
    }
}
