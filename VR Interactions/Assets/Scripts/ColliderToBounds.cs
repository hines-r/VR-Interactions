using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToBounds : MonoBehaviour
{
    public BoxCollider colli;
    public GameObject bound;
    private Bounds newbound;
    private Transform self;

    public PhysicsResolver resolver;

    void FixedUpdate()
    {
        self = colli.transform;
        newbound = bound.GetComponent<Collider>().bounds;
        colli.size = new Vector3((newbound.size.x / self.localScale.x), (newbound.size.y / self.localScale.y), (newbound.size.z / self.localScale.z));

        if (!resolver.isStuckOnSurface && !resolver.isInsideObject)
        {
            Vector3 newPos = new Vector3(bound.transform.position.x, transform.position.y, bound.transform.position.z);
            colli.center = bound.transform.localPosition;
        }
    }
}
