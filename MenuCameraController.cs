using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity for mouse movement
    public Transform playerBody; // Reference to the camera's parent object (usually the "head")
    public Vector2 verticalClamp = new Vector2(-45f, 45f); // Limits for up-down rotation

    private float xRotation = 0f; // Tracks up-down rotation

    void Start()
    {
        // Ensure the cursor is visible and unlocked
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust vertical rotation (up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, verticalClamp.x, verticalClamp.y); // Apply vertical bounds

        // Apply vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body for horizontal movement (360 degrees allowed)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
