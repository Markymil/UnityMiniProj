using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 2.0f; // Speed of camera rotation
    public float verticalRotationLimit = 90f; // Limit for vertical camera rotation

    private float verticalRotation = 0f;

    void Update()
    {
        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate the player horizontally based on mouse X movement
        transform.parent.Rotate(Vector3.up * mouseX);

        // Calculate vertical rotation for the camera
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // Apply vertical rotation to the camera
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0f);
    }
}
