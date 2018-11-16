using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ObjectPickup : MonoBehaviour
{
    public GameObject obj;
    public float pickupRadius = 0.05f;

    public SteamVR_Input_Sources handType;

    private SphereCollider sphereCol;
    public FixedJoint fJoint;

    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;

    [SteamVR_DefaultAction("GrabPinch")]
    public SteamVR_Action_Boolean grabPinchAction;

    void Start()
    {
        sphereCol = GetComponent<SphereCollider>();
        sphereCol.radius = pickupRadius;

        fJoint = GetComponent<FixedJoint>();
    }

    void Update()
    {
        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);

        if (triggerValue == 1f)
        {
            PickupObj();
        }

        if (triggerValue < 1f)
        {
            DropObj();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pickup")
        {
            obj = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        obj = null;
    }

    void PickupObj()
    {
        if (obj != null)
        {
            fJoint.connectedBody = obj.GetComponent<Rigidbody>();
        }
        else
        {
            fJoint.connectedBody = null;
        }
    }

    void DropObj()
    {
        if (fJoint != null)
        {
            fJoint.connectedBody = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
