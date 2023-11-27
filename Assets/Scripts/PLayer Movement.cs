using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public float jumpForce = 10f; // Force applied when jumping

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        float rotateY = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, rotateY, 0f) * rotationSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check if the player is grounded before allowing a jump
            if (IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    // Function to check if the player is grounded
    private bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 1.1f; // Adjust this value to match your player's height from the ground

        // Check if a raycast from the player's position downwards hits the ground
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distance))
        {
            return true;
        }
        return false;
    }
}
