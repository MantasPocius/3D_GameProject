using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healthAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthTest playerHealth = other.GetComponent<HealthTest>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}
