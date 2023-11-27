using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float fireRate = 30f; // Rate of fire (in shots per second)
    public float weaponRange = 50f; // Range of the weapon
    public LineRenderer laserRenderer; // Reference to the Line Renderer component
    public Transform barrel; // Reference to the gun barrel (or the point where the laser should start)
    private float timeBetweenShots; // Time between shots

    void Start()
    {
        // Ensure the Line Renderer component is initially disabled
        if (laserRenderer != null)
        {
            laserRenderer.enabled = false;
        }

        timeBetweenShots = 1f / fireRate; // Calculate time between shots based on fire rate
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeBetweenShots)
        {
            Shoot();
            timeBetweenShots = Time.time + 1f / fireRate; // Update time for the next shot
        }

        if (laserRenderer.enabled && Time.time >= timeBetweenShots)
        {
            laserRenderer.enabled = false; // Disable the laser beam after timeBetweenShots
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponRange))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Show the laser beam from gun barrel to the hit point
            if (laserRenderer != null && barrel != null)
            {
                laserRenderer.enabled = true;
                laserRenderer.SetPosition(0, barrel.position); // Set start position at gun barrel
                laserRenderer.SetPosition(1, hit.point); // Set end position at the hit point
            }

            // Perform actions based on the hit object (e.g., apply damage, trigger effects, etc.)
            // Example: if (hit.collider.CompareTag("Enemy")) { /* Damage enemy */ }
        }
        else
        {
            Debug.Log("Missed");

            // Hide the laser if there is no hit
            if (laserRenderer != null)
            {
                laserRenderer.enabled = false;
            }
        }
    }
}

