using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    // Start is called before the first frame update
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
