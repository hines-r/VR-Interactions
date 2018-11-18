using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PullManager : MonoBehaviour
{
    public Rigidbody body;

    public HandPull left;
    public HandPull right;

    [SteamVR_DefaultAction("GrabGrip")]
    public SteamVR_Action_Boolean grabGripAction;

    private bool gripDownLeft;
    private bool gripDownRight;

    private bool gripUpLeft;
    private bool gripUpRight;

    private bool isGripped;

    void FixedUpdate()
    {
        gripDownLeft = grabGripAction.GetState(SteamVR_Input_Sources.LeftHand);
        gripDownRight = grabGripAction.GetState(SteamVR_Input_Sources.RightHand);

        gripUpLeft = grabGripAction.GetStateUp(SteamVR_Input_Sources.LeftHand);
        gripUpRight = grabGripAction.GetStateUp(SteamVR_Input_Sources.RightHand);

        isGripped = left.canGrip || right.canGrip;

        if (isGripped)
        {
            // LEFT
            if (left.canGrip && gripDownLeft)
            {
                body.useGravity = false;
                body.isKinematic = true;
                body.transform.position += (left.prevPos - left.transform.position);
            }
            else if (left.canGrip && gripUpLeft)
            {
                body.useGravity = true;
                body.isKinematic = false;
                body.velocity = (left.prevPos - left.transform.position) / Time.deltaTime;
            }

            // RIGHT
            if (right.canGrip && gripDownRight)
            {
                body.useGravity = false;
                body.isKinematic = true;
                body.transform.position += (right.prevPos - right.transform.position);
            }
            else if (right.canGrip && gripUpRight)
            {
                body.useGravity = true;
                body.isKinematic = false;
                body.velocity = (right.prevPos - right.transform.position) / Time.deltaTime;
            }
        }
        else
        {
            body.useGravity = true;
            body.isKinematic = false;
        }

        left.prevPos = left.transform.position;
        right.prevPos = right.transform.position;
    }
}
