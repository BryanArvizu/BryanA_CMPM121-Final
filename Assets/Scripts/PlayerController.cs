using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    const float GRAVITY = 9.81f;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera uiCam;

    [Space(10)]

    [Header("Look Settings")]
    [SerializeField] private bool invertMouse = false;
    [SerializeField] private float sensitivity = 1f;

    [Space(10)]

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 0.0625f;

    // [Header("Weapon Settings")]

    private CharacterController controller;
    private Health hp;
    private WeaponController weapon;
    private Vector3 movement = Vector3.zero;

    private bool isDead = false;
    private bool sprinting = false;
    private float exitTimer = 3f;

    void Start()
    {
        EventManager.StartListening("Health", CheckDeath);

        controller = GetComponent<CharacterController>();
        hp = GetComponent<Health>();
        weapon = GetComponent<WeaponController>();
        Cursor.lockState = CursorLockMode.Locked;

        if (playerCam == null)
        {
            Debug.Log("Player Camera not set: Finding Main Camera...");
            GameObject obj = GameObject.Find("Main Camera");
            obj.transform.parent = this.transform;
            playerCam = obj.GetComponent<Camera>();
        }
    }

    void Update()
    {
        UpdateRotation();

        if (!isDead)
            UpdateMovement();
        else if(exitTimer > 0f)
        {
            exitTimer -= Time.deltaTime;
            if(exitTimer < 0)
            {
                exitTimer = 0;
                SceneManager.LoadScene("MainMenu");
            }
        }

        if(Input.GetButtonUp("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyUp(KeyCode.P))
            invertMouse = !invertMouse;
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

            if (Input.GetButton("Sprint") && Input.GetAxis("Vertical") > 0)
            {
                print("SPRINTING");
                sprinting = true;
                movement.x *= sprintMultiplier;
                movement.z *= sprintMultiplier;
            }
            else
            {
                sprinting = false;
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
            sprinting = false;
            movement.y = movement.y - (GRAVITY * Time.deltaTime * gravityScale);
        }

        // Move Player
        controller.Move(movement);
    }

    public Camera GetPlayerCamera()
    {
        return playerCam;
    }

    public Camera GetUICamera()
    {
        return uiCam;
    }

    public bool IsSprinting()
    {
        return sprinting;
    }

    private void CheckDeath(float hp)
    {
        if (hp <= 0 && !isDead)
        {
            uiCam.enabled = false;
            if (weapon != null)
                weapon.disable = true;
            GetComponent<CapsuleCollider>().enabled = false;
            playerCam.transform.position += new Vector3(0f, -1f, 0f);
            isDead = true;
        }
    }
}
