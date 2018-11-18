//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Collider dangling from the player's head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(CapsuleCollider))]
    public class BodyCollider : MonoBehaviour
    {
        public Transform head;

        private CapsuleCollider capsuleCollider;

        //-------------------------------------------------

        void Awake()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        //-------------------------------------------------

        void FixedUpdate()
        {
            float distanceFromFloor = Vector3.Dot(head.localPosition, Vector3.up);
            capsuleCollider.height = Mathf.Max(capsuleCollider.radius, distanceFromFloor);
            transform.localPosition = head.localPosition - 0.5f * distanceFromFloor * Vector3.up;

            Vector3 eulerRotation = new Vector3(0, head.transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }
}