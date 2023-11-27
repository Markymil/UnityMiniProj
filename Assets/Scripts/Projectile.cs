using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f; // Lifetime of the projectile before it's destroyed

    private GameObject shooter; // Reference to the shooter (player)

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player") && collision.gameObject != shooter)
        {
            Destroy(gameObject);
        }
    }

    public void SetShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }
}
