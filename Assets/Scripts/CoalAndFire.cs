using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalAndFire : MonoBehaviour
{
    [SerializeField]
    private GameObject coal;
    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private ParticleSystem fireParticles;
    [SerializeField]
    private Light fireLight;
    [SerializeField]
    private AudioSource fireSound;
    [SerializeField]
    private ParticleSystem smokeParticles;
    [SerializeField]
    public float resizeSpeed = 0.05f;
    [SerializeField]
    public float coalLevelDecay = 0.1f;
    [SerializeField]
    public float coalLevelDecaySpeed = 10;
    [SerializeField]
    public float coalMaxLevel = 10;
    [SerializeField]
    public float startingCoalLevel = 7;

    private float coalLevel;
    private Vector3 coalMaxScale;
    private Vector3 fireMaxScale;
    private float fireMaxEmissionRate;
    private float smokeMaxEmissionRate;
    private float lightMaxRange;
    private float soundMaxVolume;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("decayCoal", coalLevelDecaySpeed, coalLevelDecaySpeed);
        coalMaxScale = coal.transform.localScale;
        fireMaxScale = fireTransform.localScale;
        fireMaxEmissionRate = fireParticles.emission.rateOverTime.constant;
        smokeMaxEmissionRate = smokeParticles.emission.rateOverTime.constant;
        lightMaxRange = fireLight.range;
        coalLevel = startingCoalLevel;
        soundMaxVolume = fireSound.volume;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float ratio = Mathf.Min(coalLevel / coalMaxLevel, 1);
        Vector3 scaleVector = new Vector3(ratio, ratio, ratio);
        Vector3 targetCoalScale = Vector3.Scale(coalMaxScale, scaleVector);

        Vector3 scale = coal.transform.localScale;
        scale.x = Mathf.Max(targetCoalScale.x, scale.x - resizeSpeed);
        scale.y = Mathf.Max(targetCoalScale.y, scale.y - resizeSpeed);
        scale.z = Mathf.Max(targetCoalScale.z, scale.z - resizeSpeed);

        if (coal.transform.localScale != scale)
        {
            coal.transform.localScale = scale;
            applyFireSize(ratio);
        }
    }

    void decayCoal()
    {
        var oldCoalLevel = coalLevel;
        coalLevel = Mathf.Max(0, coalLevel - coalLevelDecay);
        GameManager.instance.BurnedCoal(oldCoalLevel - coalLevel);
    }

    public void AddCoalLevel(float level)
    {
        coalLevel = Mathf.Min(coalMaxLevel, coalLevel + level);
        float ratio = Mathf.Min(coalLevel / coalMaxLevel, 1);
        Vector3 scaleVector = new Vector3(ratio, ratio, ratio);

        coal.transform.localScale = Vector3.Scale(coal.transform.localScale, scaleVector);
        applyFireSize(ratio);
    }

    private void applyFireSize(float ratio)
    {
        Vector3 scaleVector = new Vector3(ratio, ratio, ratio);
        fireTransform.localScale = Vector3.Scale(fireMaxScale, scaleVector);
        var em = fireParticles.emission;
        em.rateOverTime = fireMaxEmissionRate * (Mathf.Pow(ratio, 2));
        em = smokeParticles.emission;
        em.rateOverTime = smokeMaxEmissionRate * (Mathf.Pow(ratio, 2));
        fireLight.range = lightMaxRange * (Mathf.Pow(ratio, 2));
        fireSound.volume = soundMaxVolume * ratio;
    }
}
