using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity for mouse movement
    public Transform playerBody; // Reference to the camera's parent object (usually the "head")
    public Vector2 verticalClamp = new Vector2(-45f, 45f); // Limits for up-down rotation
    public Vector2 horizontalClamp = new Vector2(-90f, 90f); // Limits for side-to-side rotation

    private float xRotation = 0f; // Tracks up-down rotation
    private float yRotation = 0f; // Tracks side-to-side rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust vertical rotation (up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, verticalClamp.x, verticalClamp.y); // Apply vertical bounds

        // Adjust horizontal rotation (side to side)
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, horizontalClamp.x, horizontalClamp.y); // Apply horizontal bounds

        // Apply rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply rotation to the player's "head" or body for horizontal movement
        playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
