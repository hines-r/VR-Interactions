using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhysicsResolver : MonoBehaviour
{
    public bool isGrounded;
    public bool isStuckOnSurface;
    public float rayLength = 0.5f;
    public float inset = 0.1f;
    public LayerMask mask;

    private Collider coll;

    void Awake()
    {
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        Vector3 originPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + .15f);
        Ray rayDown = new Ray(transform.position + coll.bounds.extents.y * Vector3.down + inset * Vector3.up, -transform.up);
        RaycastHit hitInfo;


        if (Physics.Raycast(rayDown, out hitInfo, rayLength, mask))
        {
            Debug.Log(hitInfo.collider);
            Debug.DrawLine(rayDown.origin, hitInfo.point, Color.red);
            isGrounded = true;
        }
        else
        {
            Debug.DrawLine(rayDown.origin, rayDown.origin + rayDown.direction * rayLength, Color.green);
            isGrounded = false;
        }

        Ray rayForward = new Ray(transform.position + coll.bounds.extents.y * Vector3.forward, transform.forward);

        if (Physics.Raycast(rayForward, out hitInfo, rayLength, mask))
        {
            Debug.Log(hitInfo.collider);
            Debug.DrawLine(rayForward.origin, hitInfo.point, Color.red);
            isStuckOnSurface = true;
        }
        else
        {
            Debug.DrawLine(rayForward.origin, rayForward.origin + rayForward.direction * rayLength, Color.green);
            isStuckOnSurface = false;
        }
    }
}
