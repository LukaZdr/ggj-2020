 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufplatzring : MonoBehaviour
{
    [SerializeField]
    GameObject boltPlaceholder;
    [SerializeField]
    GameObject korkPlaceholder;
    [SerializeField]
    float deleteDelay = 10f;
    [SerializeField]
    ParticleSystem[] particles = new ParticleSystem[2];
    [SerializeField]
    AudioSource audioSource;

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

    // Im Erfolgsfall ist other = Bolzen oder Korken
    void OnTriggerEnter(Collider other)
    {
        if (!leaking) return;
        
        if (other.tag == "Bolt")
        {
            // Bolt
            boltPlaceholder.SetActive(true);
            destroy(other);
        } else if (other.tag == "Kork")
        {
            // Kork
            korkPlaceholder.SetActive(true);
            destroy(other);
        }
    }

    void OnHandHoverBegin()
    {
        setParticleEmissionAndSoundEnabled(false);
    }

    void OnHandHoverEnd()
    {
        if (!boltPlaceholder.activeSelf && !korkPlaceholder.activeSelf)
        {
            setParticleEmissionAndSoundEnabled(true);
        }
    }

    private void destroy(Collider other)
    {
        leaking = false;
        Destroy(other.gameObject);
        this.GetComponent<Collider>().enabled = false;

        // Score
        _gm.StuffedHole();

        //Animations
        setParticleEmissionAndSoundEnabled(false);
        this.GetComponent<Animator>().enabled = true;

        //bye bye
        Destroy(this.gameObject, deleteDelay);
    }

    private void setParticleEmissionAndSoundEnabled(bool enabled)
    {
        leaking = enabled;

        foreach (var ps in particles)
        {
            var em = ps.emission;
            em.enabled = enabled;
        }
        if (audioSource)
        {
            if (enabled && !audioSource.isPlaying)
            {
                audioSource.Play();
            }else if(!enabled && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
