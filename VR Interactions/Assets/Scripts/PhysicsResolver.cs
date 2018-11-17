using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhysicsResolver : MonoBehaviour
{
    public bool isGrounded;
    public bool isStuckOnSurface;
    public bool isInsideObject;

    public float rayDownLength = 0.5f;
    public float rayForwardLength = 0.5f;
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

        // Downward raycast
        Ray rayDown = new Ray(transform.position + coll.bounds.extents.y * Vector3.down + inset * Vector3.up, -transform.up);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayDown, out hitInfo, rayDownLength, mask))
        {
            //Debug.Log(hitInfo.collider);
            Debug.DrawLine(rayDown.origin, hitInfo.point, Color.red);
            isGrounded = true;
        }
        else
        {
            Debug.DrawLine(rayDown.origin, rayDown.origin + rayDown.direction * rayDownLength, Color.green);
            isGrounded = false;
        }

        // Forward raycast
        Ray rayForward = new Ray(transform.position + coll.bounds.extents.y * Vector3.down, transform.forward);

        if (Physics.Raycast(rayForward, out hitInfo, rayForwardLength, mask))
        {
            //Debug.Log(hitInfo.collider);
            Debug.DrawLine(rayForward.origin, hitInfo.point, Color.red);
            isStuckOnSurface = true;
        }
        else
        {
            Debug.DrawLine(rayForward.origin, rayForward.origin + rayForward.direction * rayForwardLength, Color.green);
            isStuckOnSurface = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            isInsideObject = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            isInsideObject = false;
        }
    }
}
