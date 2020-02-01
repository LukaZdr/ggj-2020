using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileSpawn;
    public float bulletSpeed = 750;
    public float despawnTime = 15;
    [Range(0.1f, 10)]
    public float shotsPerSec = 1;
    public float feedbackDuration = 0.5f;
    [Range(0f, 320f)]
    public float feedbackFrequency = 0;
    [Range(0f, 1f)]
    public float feedbackAmplitude = 1;

    private SteamVR_Action_Boolean actionFire = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("gun", "fire");
    private SteamVR_Action_Vibration actionVibration = SteamVR_Input.GetVibrationAction("gun", "recoil");
    private Interactable interactable;
    private float lastShotTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (interactable.attachedToHand)
        {
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
            bool b_fire = actionFire.GetState(hand);

            //TODO: Singe action
            if (b_fire && Time.time > lastShotTime+1/shotsPerSec)
            {
                Fire(hand);
            }
        }
    }

    public void Fire(SteamVR_Input_Sources source)
    {
        lastShotTime = Time.time;
        GameObject _projectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation * projectile.transform.rotation);
        Rigidbody rb = _projectile.GetComponent<Rigidbody>();
        _projectile.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * rb.mass);
        actionVibration.Execute(0, feedbackDuration, feedbackFrequency, feedbackAmplitude, source);
        GameObject.Destroy(_projectile, despawnTime);
    }
}
