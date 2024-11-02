using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Destructable")
        {
            GameObject myClone = Instantiate(collision.gameObject, collision.gameObject.transform.position + Vector3.up * 5, Quaternion.identity);
            myClone.name = "myCloneObject";
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destructable")
        {
            Destroy(other.gameObject);
        }
    }

}