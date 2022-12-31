using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var item in collision.GetComponents<BoxCollider2D>())
        {
            if (!item.enabled)
                item.enabled = true;
        }
    }
}
