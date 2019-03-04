using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float GRAVITY = 9.8f;
    [SerializeField] private Camera playerCam;

    [Space(10)]

    [SerializeField] private bool invertMouse = false;
    [SerializeField] private float sensitivity = 1f;

    [Space(10)]

    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 0.0625f;


    private CharacterController controller;
    private Vector3 movement = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateRotation();
        UpdateMovement();
    }

    private void UpdateRotation()
    {
        Vector3 rotation = Vector3.zero;
        float rotX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity * 10f;
        float rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity * 10f;

        if (!invertMouse)
            rotY *= -1;

        rotX += transform.eulerAngles.y;               // Player rotates with X movement
        rotY += playerCam.transform.eulerAngles.x;     // Camera rotates with Y movement

        if (rotY > 180)     // Translate to negative degrees
            rotY -= 360;

        rotY = Mathf.Clamp(rotY, -90, 90);  // Clamp rotY so player can't "somersault" with Camera

        // Apply rotations to player and camera
        transform.rotation = Quaternion.Euler(0, rotX, 0);
        playerCam.transform.rotation = Quaternion.Euler(rotY, rotX, 0);
    }

    private void UpdateMovement()
    {
        if (controller.isGrounded)
        {
            movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            movement.z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

            movement = transform.rotation * movement; // Rotate movement vector to account for player direction

            if (Input.GetButton("Sprint"))
            {
                print("SPRINTING");
                movement.x *= sprintMultiplier;
                movement.z *= sprintMultiplier;
            }

            if (Input.GetButtonDown("Jump"))
            {
                print("JUMP");
                movement.y = Time.deltaTime * jumpForce;
            }
        }
        else
        {
            print("inAir");
            movement.y = movement.y - (GRAVITY * Time.deltaTime * gravityScale);
        }
        controller.Move(movement);
    }
}
