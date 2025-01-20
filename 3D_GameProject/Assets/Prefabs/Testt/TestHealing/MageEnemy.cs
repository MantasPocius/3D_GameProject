using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : MonoBehaviour
{
    public GameObject bandagePrefab; 
    public float health = 60f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (bandagePrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0); 
            Instantiate(bandagePrefab, spawnPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
