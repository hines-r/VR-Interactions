using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Gun : MonoBehaviour
{
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;

    [SteamVR_DefaultAction("GrabPinch")]
    public SteamVR_Action_Boolean grabPinchAction;

    public float range = 100f;

    public GameObject firePoint;
    public GameObject projectile;
    private float timeToFire;

    private Interactable interactable;

    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    void Update()
    {
        Ray gunRay = new Ray(firePoint.transform.position, firePoint.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(gunRay, out hitInfo, range))
        {
            Debug.DrawLine(gunRay.origin, hitInfo.point, Color.red);

            if (hitInfo.rigidbody != null)
            {

            }
        }
        else
        {
            Debug.DrawLine(gunRay.origin, gunRay.origin + gunRay.direction * range, Color.green);
        }

        if (interactable != null && interactable.attachedToHand != null)
        {
            if (grabPinchAction.GetState(SteamVR_Input_Sources.Any) && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / projectile.GetComponent<ProjectileMove>().fireRate;
                Shoot();
            }

            float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);

            if (triggerValue >= 1f)
            {
                //Shoot();
            }
        }            
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, firePoint.transform.position, transform.rotation);
        Destroy(bullet, 10f);
    }
}
