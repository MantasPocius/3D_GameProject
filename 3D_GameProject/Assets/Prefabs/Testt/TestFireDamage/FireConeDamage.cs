using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireConeDamage : MonoBehaviour
{
    public float damageAmount = 5f; 
    public float damageInterval = 1f; 

    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float currentTime = Time.time;

            if (damageTimers.ContainsKey(other.gameObject))
            {
                if (currentTime >= damageTimers[other.gameObject])
                {
                    ApplyDamage(other);
                    damageTimers[other.gameObject] = currentTime + damageInterval;
                }
            }
            else
            {
                ApplyDamage(other);
                damageTimers[other.gameObject] = currentTime + damageInterval;
            }
        }
    }

    private void ApplyDamage(Collider other)
    {
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (damageTimers.ContainsKey(other.gameObject))
        {
            damageTimers.Remove(other.gameObject);
        }
    }
}

