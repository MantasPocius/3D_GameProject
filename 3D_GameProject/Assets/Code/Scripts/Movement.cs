using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject objectToRotate;
    public float slowdownMovement = 1, slowdownRotation = 1, shootingStrength = 5, movementStrength = 6, jumpStrength = 4;
    Vector2 movementData, rotData;
    public GameObject objectToSpawn;
    public GameObject shootingPoint;
    public bool isGrounded = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector2 vector2 = new Vector2(1, 1);
        Vector2 vector2norm = vector2.normalized;
        Debug.Log(vector2norm);
        Debug.Log(vector2.magnitude);
        Debug.Log(vector2norm.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newnormVector = movementData.normalized;
        //movementData.Normalize();
        /*transform.Translate(movementData.x / slowdownMovement * Time.deltaTime, 0, movementData.y / slowdownMovement * Time.deltaTime);*/

        transform.Rotate(0, rotData.x / slowdownRotation * Time.deltaTime, 0);
        objectToRotate.transform.Rotate(-rotData.y / slowdownRotation * Time.deltaTime, 0, 0);
    }

    private void FixedUpdate()
    {
        //movementData.Normalize();
        //GetComponent<Rigidbody>().AddRelativeForce(new Vector3(movementData.x, 0, movementData.y) * Time.fixedDeltaTime * movementStrength, ForceMode.Force);
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.TransformDirection(new Vector3(movementData.x, 0, movementData.y)) * Time.fixedDeltaTime * movementStrength);
    }
    public void OnMove(InputAction.CallbackContext callback)
    {
        movementData = callback.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext callback)
    {
        rotData = callback.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext callback)
    {

        if (callback.started && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }

    public void OnShoot(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            GameObject mySpawnedObject = Instantiate(objectToSpawn, shootingPoint.transform.position, Quaternion.identity);
            mySpawnedObject.GetComponent<Rigidbody>().AddForce((shootingPoint.transform.up) * shootingStrength, ForceMode.Impulse);
            Destroy(mySpawnedObject, 3);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
            { isGrounded = true; }
    }
    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        { isGrounded = false; }
    }
}
