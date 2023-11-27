using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickup : MonoBehaviour
{
    private bool canPickUp = false; // Flag to check if the player can pick up a box
    private Rigidbody boxRb; // Reference to the Rigidbody of the box being picked up
    private Transform playerTransform; // Reference to the player's Transform

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            if (boxRb != null && boxRb.transform.parent == playerTransform) // Check if a box is already held
            {
                ReleaseBox();
            }
            else if (boxRb != null)
            {
                PickUpBox();
            }
        }
    }

    void PickUpBox()
    {
        // Attach the box to the player by making it a child of the player's transform
        boxRb.isKinematic = true;
        boxRb.transform.SetParent(playerTransform);

        // Position the box in front of the player (you can adjust this position as needed)
        boxRb.transform.localPosition = new Vector3(0f, 1f, 1.5f);

        // Disable box collider while held to prevent immediate re-triggering
        Collider boxCollider = boxRb.GetComponent<Collider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }

    void ReleaseBox()
    {
        // Release the box from the player and make it independent
        boxRb.isKinematic = false;
        boxRb.transform.SetParent(null);

        // Re-enable box collider
        Collider boxCollider = boxRb.GetComponent<Collider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }

        boxRb = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box")) // Assuming the tag "Box" is assigned to the boxes
        {
            canPickUp = true;
            boxRb = other.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            canPickUp = false;
        }
    }
}

