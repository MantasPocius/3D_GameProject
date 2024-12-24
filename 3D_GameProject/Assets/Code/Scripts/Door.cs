using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{   
    public Transform player;
    public float openRange = 2f;
    //public string inventoryItemName = "The key";
    public GameObject findHint;
    public GameObject openHint;
    public GameObject findBackground;
    public GameObject openBackground;
    [SerializeField] private ItemPickup Key;

    private bool isInRange = false;
    private bool DoorIsOpen=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        DoorIsOpen = Key.KeyPickedUp;
        

        if (Vector3.Distance(player.position, transform.position) < openRange)
        {
            if (!isInRange && DoorIsOpen)
            {
                openHint.SetActive(true);
                openBackground.SetActive(true);
                isInRange = true;
            }
            if (!isInRange && !DoorIsOpen)
            {
                findHint.SetActive(true);
                findBackground.SetActive(true);
                isInRange = true;
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Door is open");

                Destroy(gameObject);

                openHint.SetActive(false);
                openBackground.SetActive(false);
                findHint.SetActive(false);
                findBackground.SetActive(false);
            }
        }
        else
        {
            if (isInRange)
            {
                findHint.SetActive(false);
                findBackground.SetActive(false);
                isInRange = false;
            }
            if (isInRange)
            {
                openHint.SetActive(false);
                openBackground.SetActive(false);
                isInRange = false;
            }
        }
    }
}
