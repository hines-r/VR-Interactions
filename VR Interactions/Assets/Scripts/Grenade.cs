using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect;

    public float minMagnitudeToExplode = 1f;
    public float explosionRadius = 2f;
    public float explosionForce = 1000f;

    private Interactable interactable;

    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (interactable != null && interactable.attachedToHand != null) return;

        if (collision.impulse.magnitude > minMagnitudeToExplode)
        {
            Explode();
        }
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosion, 5f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        Destroy(gameObject); // Destroy grenade itself
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
