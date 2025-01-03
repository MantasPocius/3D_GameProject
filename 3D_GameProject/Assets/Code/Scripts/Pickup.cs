using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Transform player; 
    public float pickupRange = 2f; 
    public string inventoryItemName = "The key"; 
    public GameObject pickupHint; 
    public GameObject pickupBackground; 

    private bool isInRange = false;
    public bool KeyPickedUp = false; 

    void Update()
    {
        
        if (Vector3.Distance(player.position, transform.position) < pickupRange)
        {
            if (!isInRange)
            {
                pickupHint.SetActive(true);
                pickupBackground.SetActive(true);
                isInRange = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                KeyPickedUp = true;

                Debug.Log(inventoryItemName + " picked up!");

                gameObject.SetActive(false);

                pickupHint.SetActive(false);
                pickupBackground.SetActive(false);
            }
        }
        else
        {
            if (isInRange)
            {
                pickupHint.SetActive(false);
                pickupBackground.SetActive(false);
                isInRange = false;
            }
        }
    }
}

