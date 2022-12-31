using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    private PlayerMovement plM_classi;
    public Animator animator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            plM_classi = collision.gameObject.GetComponent<PlayerMovement>();
            animator = collision.gameObject.GetComponent<Animator>();
            plM_classi.laddCond = true;
            animator.SetBool("climbing", true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            plM_classi.laddCond = false;
            animator.SetBool("climbing", false);
        }
    }
}
