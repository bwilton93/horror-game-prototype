using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 1.2f;
    [SerializeField] float walkSpeed = 4.0f;
    [SerializeField] float sprintModifier = 2.0f;
    [SerializeField] float gravity = -25.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.1f;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    [SerializeField] GameObject flashlightLight;
    private bool flashlightActive = false;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if(lockCursor) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        flashlightLight = flashlightLight.gameObject;
        flashlightLight.SetActive(false);
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        ToggleFlashlight();
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement() 
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkSpeed += sprintModifier;
            Debug.Log("walk speed: " + walkSpeed);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            walkSpeed -= sprintModifier;
            Debug.Log("Walk speed: " + walkSpeed);
        }

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY; 

        controller.Move(velocity * Time.deltaTime);
    }

    void ToggleFlashlight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            flashlightActive = !flashlightActive;
            if(!flashlightActive) {
                Debug.Log(flashlightActive);
                flashlightLight.SetActive(true);
            } 
            else 
            {
                flashlightLight.SetActive(false);    
            }
        }
    }
}