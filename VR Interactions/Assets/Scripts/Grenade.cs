using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect;

    public float minMagnitudeToExplode = 1f;

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
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(explosion, 5f);

            Destroy(gameObject); // Destroy grenade itself
        }
    }
}
