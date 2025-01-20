using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfFireAttack : MonoBehaviour
{
    public GameObject firePrefab;  
    public Transform fireOrigin;  

    public void TriggerFire()
    {
        if (firePrefab != null && fireOrigin != null)
        {
            GameObject fireEffect = Instantiate(firePrefab, fireOrigin.position, fireOrigin.rotation);

            Destroy(fireEffect, 2f);
        }
    }
}
