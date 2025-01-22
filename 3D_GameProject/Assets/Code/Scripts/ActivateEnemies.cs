using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    void Start()
    {
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        foreach (var enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
}
