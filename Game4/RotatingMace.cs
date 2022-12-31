using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingMace : MonoBehaviour
{
    public Transform first, second;

    public float speed;


    Quaternion nextPos;
    private void Start()
    {
        nextPos = first.rotation;
    }

    private void Update()
    {
        if (transform.rotation == first.rotation)
        {
            nextPos = second.rotation;
        }

        if (transform.rotation == second.rotation)
        {
            nextPos = first.rotation;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(first.position, second.position);
    }
}
