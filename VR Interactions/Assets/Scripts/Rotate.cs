using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 axis;
    public float speed;
    public Transform point;


    void Update()
    {
        transform.RotateAround(point.position, axis, speed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        float size = 0.25f;
        Gizmos.DrawWireCube(point.position, new Vector3(size, size, size));
    }
}
