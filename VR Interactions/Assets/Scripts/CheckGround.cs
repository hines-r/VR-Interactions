using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckGround : MonoBehaviour
{
    public float distanceToGround;
    public bool isGrounded = false;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        Debug.DrawLine(transform.position, -Vector3.up, Color.red);

        if (!Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f))
        {
            isGrounded = false;
            print("Grounded is False!");
        }
        else
        {
            isGrounded = true;
            print("Grounded is True!");
        }
    }
}
