using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    private BuildingManager buildingManager;
    private float threshold = 0.1f;

    void Start()
    {
        
        buildingManager = GameObject.Find("GameManager").GetComponent<BuildingManager>();
    }
    
    private void Update()
    {
        Collider objCollider = GetComponent<Collider>();

        Collider[] colliders = Physics.OverlapBox(objCollider.bounds.center, objCollider.bounds.extents);

        foreach (Collider other in colliders)
        {
            if (other == objCollider) continue;

            Vector3 direction;
            float distance;
            if (Physics.ComputePenetration(objCollider, transform.position, transform.rotation,
                                           other, other.transform.position, other.transform.rotation,
                                           out direction, out distance))
            {
                if (distance > threshold)
                {
                    buildingManager.canPlace = false;
                    return;
                }
            }
        }
        buildingManager.canPlace = true;
    }
}
