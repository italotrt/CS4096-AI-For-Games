using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Public variable for the mouse sensitivity
    public Transform playerBody; // Reference to the player's body transform
    float xRotation = 0f; // Variable to store the rotation around the x-axis

    void Start()
    {
        // Hide the cursor and lock its position to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get the mouse input for horizontal and vertical movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the xRotation based on vertical mouse movement and clamp it to a specified range
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Set the local rotation of the camera based on the adjusted xRotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player's body horizontally based on mouse movement
        playerBody.Rotate(Vector3.up * mouseX);
    }
}