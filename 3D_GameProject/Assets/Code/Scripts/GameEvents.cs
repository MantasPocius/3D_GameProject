using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public Transform[] triggers; // Array of triggers
    public GameObject[] spawnPointGroups; // Parent objects containing spawn points for each trigger
    public GameObject enemy; // Enemy prefab

    private void SpawnEnemies(GameObject spawnGroup)
    {
        // Get all child spawn points from the parent spawn group
        Transform[] spawnPoints = spawnGroup.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != spawnGroup.transform) // Avoid the parent object itself
            {
                Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            if (other.transform == triggers[i])
            {
                SpawnEnemies(spawnPointGroups[i]);
                break;
            }
        }
    }
}