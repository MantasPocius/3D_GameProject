using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Knight : MonoBehaviour
{
    public GameObject Key;   
    public GameObject heartPrefab; 
    public int knightHealth = 100; 

    public void TakeDamage(int damage)
    {
        knightHealth -= damage;

        if (knightHealth <= 0)
        {
            Debug.Log("Knight died!");  
            Die();
        }
    }

    public void Die()
    {
        if (Key != null)
        {
            Key.SetActive(true);            
        }
        else
        {
            Debug.LogWarning("Key object is not assigned!");
        }

        if (heartPrefab != null)
        {
            GameObject heart = Instantiate(heartPrefab, transform.position + Vector3.up * 1.5f  - transform.forward * 3f, Quaternion.identity);

        }

        Destroy(gameObject); 
    }
}

