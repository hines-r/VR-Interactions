using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(FixedJoint))]
public class ObjectGrab : MonoBehaviour
{
    public GameObject objectToGrab;
    public Hand hand;

    [SteamVR_DefaultAction("GrabPinch")]
    public SteamVR_Action_Boolean grabPinchAction;

    [SteamVR_DefaultAction("GrabGrip")]
    public SteamVR_Action_Boolean grabGripAction;

    private FixedJoint fixedJoint;

    void Start()
    {
        fixedJoint = GetComponent<FixedJoint>();

        if (grabPinchAction == null)
        {
            Debug.LogError("Grab pinch action not assigned!");
            return;
        }

        if (grabGripAction == null)
        {
            Debug.LogError("Grab grip action not assigned!");
            return;
        }

        grabPinchAction.AddOnChangeListener(OnGrabPinchChange, hand.handType);
        grabGripAction.AddOnChangeListener(OnGrabGripChange, hand.handType);
    }

    void OnGrabPinchChange(SteamVR_Action_In actionIn)
    {
        if (grabPinchAction.GetStateDown(hand.handType))
        {
            PickupObj();
        }
    }

    void OnGrabGripChange(SteamVR_Action_In actionIn)
    {
        if (grabGripAction.GetStateDown(hand.handType))
        {
            DropObj();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            objectToGrab = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        objectToGrab = null;
    }

    void PickupObj()
    {
        if (objectToGrab != null)
        {
            Rigidbody objectRigidbody = objectToGrab.GetComponent<Rigidbody>();

            if (objectRigidbody != null)
            {
                fixedJoint.connectedBody = objectRigidbody;
            }      
        }
        else
        {
            if (fixedJoint != null)
            {
                fixedJoint.connectedBody = null;
            }
        }
    }

    void DropObj()
    {
        if (fixedJoint != null && fixedJoint.connectedBody != null)
        {
            fixedJoint.connectedBody = null;
        }
    }
}
