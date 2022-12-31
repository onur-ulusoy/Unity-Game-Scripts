using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public Transform first, second, third, fourth;
    public SpriteRenderer sprite_renderer;
    public Animator animator;
    public float speed;
    bool donus = false;
    bool oldu = false;
    public float HP = 100;
    Rigidbody2D rg;
    SpriteRenderer SRB;
    Vector2 force;
    CircleCollider2D circleCollider;


    Vector3 nextPos;
    private void Start()
    {
        nextPos = first.position;
        rg = GetComponent<Rigidbody2D>();
        force = new Vector2(0, -1);
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {

        if (HP <= 0)
        {
            animator.SetBool("dead", true);
            //gameObject.SetActive(false);
            oldu = true;
            rg.AddForce(force);
            circleCollider.enabled = false;


        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }

        if (!oldu)
        {
            if (!donus)
            {

                if (transform.position == first.position)
                {
                    nextPos = second.position;
                    sprite_renderer.flipX = false;

                }

                if (transform.position == second.position)
                {
                    nextPos = third.position;
                }

                if (transform.position == third.position)
                {
                    nextPos = fourth.position;
                    donus = true;
                }
            }

            else
            {

                if (transform.position == fourth.position)
                {
                    nextPos = third.position;
                    sprite_renderer.flipX = true;

                }

                if (transform.position == third.position)
                {
                    nextPos = second.position;
                }

                if (transform.position == second.position)
                {
                    nextPos = first.position;
                    donus = false;
                }
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(first.position, second.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            animator.SetBool("attacked", true);
        }

        if (collision.transform.tag == "Bullet")
        {
            HP -= 50;
            animator.SetBool("hit", true);
            //collision.gameObject.SetActive(false);
            SRB = collision.GetComponent<SpriteRenderer>();
            SRB.enabled = false;
        }

        if (collision.transform.tag == "knifeBullet")
        {
            HP -= 50;
            animator.SetBool("hit", true);
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            //SRB = collision.GetComponent<SpriteRenderer>();
            //SRB.enabled = false;
        }

        if (collision.transform.tag == "bombBullet")
        {
            HP -= 300;
            animator.SetBool("hit", true);
            //collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            //SRB = collision.GetComponent<SpriteRenderer>();
            //SRB.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            animator.SetBool("attacked", false);
        }

        if (collision.transform.tag == "Bullet" || collision.transform.tag == "knifeBullet" || collision.transform.tag == "bombBullet")
        {
            animator.SetBool("hit", false);
        }
    }


        
    
    /*
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
    */
}
