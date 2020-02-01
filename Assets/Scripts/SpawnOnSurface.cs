using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnOnSurface : MonoBehaviour
{
    [Tooltip("Point from which to cast rays on the objects surface. The ray hit point will be the random spawn point.")]
    public Transform raySpawn;
    [Tooltip("Maximum distance the ray will go until it is considered not-hitting")]
    public float maxRaycastDistance = 100;
    [Tooltip("How many seconds to wait between casting rays")]
    public int rayInterval = 10;
    [Tooltip("Prefab which gets instantiated on the objects surface")]
    public GameObject prefab;

    private Collider _objectCollider;

    void Start()
    {
        _objectCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Time.time > 0 && ((int) Time.time) % rayInterval == 0)
        {
            var direction = Random.onUnitSphere;
            var ray = new Ray(raySpawn.position, direction);

            RaycastHit hit;
            if (GetHitpoint(out hit))
            {
                var go = Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
                go.transform.SetParent(this.transform);
            }
        }
    }

    bool GetHitpoint(out RaycastHit hit)
    {
        var direction = Random.onUnitSphere;
        var ray = new Ray(raySpawn.position, direction);

        // we intentionally dont use _objectCollider.Raycast() because we want to honor intersections with other objects
        return Physics.Raycast(ray, out hit, maxRaycastDistance) && hit.collider == _objectCollider;
    }
}
