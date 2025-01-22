using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEnemyCheck : MonoBehaviour
{
    private int EnemyKilled = 0;
    private const int RequiredKills = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnEnemyKilled(GameObject enemy)
    {
        // Check if the enemy has the tag "KeyEnemy"
        if (enemy.CompareTag("KeyEnemy"))
        {
            EnemyKilled++;
            Debug.Log("Key enemy killed. Total kills: " + keyEnemiesKilled);

            // Check if the required number of kills is reached
            if (keyEnemiesKilled >= RequiredKills)
            {
                OpenGate();
            }
        }
    }
}
