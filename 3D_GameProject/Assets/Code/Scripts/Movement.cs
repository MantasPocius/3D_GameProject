using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private CharacterController controller;

    public GameObject objectToRotate;
    public float topClamp = -90f, bottomClamp = 90f, mouseSensitivity = 100;
    float xRotation = 0f, yRotation = 0f;
    
    public float speed = 6f, gravity = -18.62f, jumpHeight = 1f, groundDistance = 0.1f;
    public bool isGrounded = true, isMoving = true;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 Velocity;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    public Animator armsAnimator;
    public Animator gunAnimator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        yRotation += mouseX;

        objectToRotate.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        gameObject.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && Velocity.y < 0)
        {
            Velocity.y = -1f * Time.deltaTime;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(Vector3.ClampMagnitude(transform.forward * z + transform.right * x, 1.0f) * speed * Time.deltaTime);

        bool isWalking = move.magnitude > 0;
        armsAnimator.SetBool("Walk", isWalking);
        gunAnimator.SetBool("Walk", isWalking);

        if (Input.GetButton("Jump") && isGrounded)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
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
