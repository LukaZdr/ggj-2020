using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectile_spawn;
    public float speed = 750;
    public float despawn = 15;
    public float shots_per_sec = 1;

    private SteamVR_Action_Boolean actionFire = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("gun", "fire");
    private Interactable interactable;
    private float lastShotTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {

        if (interactable.attachedToHand)
        {
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
            bool b_fire = actionFire.GetState(hand);

            if (b_fire && Time.time > lastShotTime+1/shots_per_sec)
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        lastShotTime = Time.time;
        GameObject _projectile = Instantiate(projectile, projectile_spawn.position, projectile_spawn.rotation * projectile.transform.rotation);
        _projectile.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        GameObject.Destroy(_projectile, despawn);
    }
}
