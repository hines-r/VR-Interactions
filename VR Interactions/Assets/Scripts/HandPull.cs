using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Hand))]
public class HandPull : MonoBehaviour
{
    public Hand hand;

    [HideInInspector]
    public Vector3 prevPos;

    [HideInInspector]
    public bool canGrip;

    void Start()
    {
        if (hand == null)
        {
            hand = GetComponent<Hand>();
        }

        prevPos = hand.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            canGrip = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            canGrip = false;
        }
    }
}
