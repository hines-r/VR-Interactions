using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public ParticleSystem impactEffect;
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
                
                if (!impactEffect.isPlaying)
                {
                    impactEffect.Play();
                }

                Vector3 dir = transform.position - hitInfo.point;
                impactEffect.transform.position = hitInfo.point;
                impactEffect.transform.rotation = Quaternion.LookRotation(dir);
            }
        }
        else
        {
            lr.SetPosition(1, transform.forward * laserLength);
            impactEffect.Stop();
        }
    }
}
