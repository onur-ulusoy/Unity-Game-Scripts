using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class PlatformMovement : Photon.MonoBehaviour
{
    public Transform first, second;

    public float speed;


    Vector3 nextPos;
    private void Start()
    {
        nextPos = first.position;
    }

    private void Update()
    {
        if (transform.position == first.position)
        {
            nextPos = second.position;
        }

        if (transform.position == second.position)
        {
            nextPos = first.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(first.position, second.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
