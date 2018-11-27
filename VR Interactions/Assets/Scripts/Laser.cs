using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public float laserLength = 100f;
    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
    }

    void Update()
    {
        lr.SetPosition(0, transform.position);

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
            if (hitInfo.collider != null)
            {
                lr.SetPosition(1, hitInfo.point);
            }
        }
        else
        {
            lr.SetPosition(1, transform.forward * laserLength);
        }
    }
}
