using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SophieController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Bullet")]
    public Transform bullet_transform;
    public GameObject bullet0; //prefab
    private GameObject Bullet;
    public float ammoSpeed;
    public float destroyTime;
    public float ammoTimer = -2;
    public Vector3 hizvektoru;

    [Header("Muzzle")]
    public GameObject muzzle;
    private GameObject Muzzle;
    public Transform muzzle_transform;

    [Header("Gun")]
    public Transform gun;
    public AudioSource gun_audio;
    public LineRenderer bulletTrail;


    [Header("Animator")]
    public float speed;
    public Animator animator;
    public float timer; //jump
    public float pistol_idle_timer; //

    public Rigidbody rg;
    public int jumpForce;


    [Header("Collider")]
    public CapsuleCollider collider1;
    public CapsuleCollider collider2;


    void Start()
    {
        //Time.timeScale = 0.5f;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isCrouched", true);
            collider1.enabled = false;
            collider2.enabled = true;

        }


        else
        {
            animator.SetBool("isCrouched", false);
            collider1.enabled = true;
            collider2.enabled = false;
        }
            

        if (!animator.GetBool("isCrouched"))
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");


            transform.Translate(new Vector3(moveHorizontal, 0f, moveVertical) * speed * Time.deltaTime);

            if (animator.GetBool("isJumping") && Time.time - timer > 1f)
            {
                animator.SetBool("isJumping", false);
            }

            if (Input.GetKey(KeyCode.Space) && !animator.GetBool("isJumping"))
            {
                rg.AddForce(0f, jumpForce, 0f);
                animator.ResetTrigger("walk");
                animator.ResetTrigger("walk_back");
                animator.ResetTrigger("idle");
                animator.SetBool("isCrouched", false);

                animator.SetBool("isJumping", true);
                timer = Time.time;
                //Time.timeScale = 0;

            }

            if (moveHorizontal != 0f || moveVertical != 0f)
            {
                if (moveVertical > 0)
                    animator.SetTrigger("walk");
                else if (moveVertical < 0)
                    animator.SetTrigger("walk_back");

                if (!animator.GetBool("isJumping"))
                {
                    animator.ResetTrigger("idle");
                    animator.SetBool("pistol_idle", false);
                }
                //Time.timeScale = 1;
            }

            else
            {
                //Time.timeScale = 0;
                if (!animator.GetBool("isJumping") && (Time.time - pistol_idle_timer > .3f))
                {
                    if (animator.GetBool("pistol_idle"))
                    {
                        Shoot();
                        animator.SetBool("pistol_idle", false);
                    }


                    animator.SetTrigger("idle");
                    animator.ResetTrigger("walk");
                    animator.ResetTrigger("walk_back");


                }

            }

            if (Input.GetKey(KeyCode.Mouse0) && Time.time - ammoTimer > 1.5)
            {
                if (animator.GetBool("idle"))
                {
                    animator.SetBool("pistol_idle", true);
                    animator.ResetTrigger("idle");
                    pistol_idle_timer = Time.time;
                }

                ammoTimer = Time.time;
                if (!animator.GetBool("pistol_idle"))
                {
                    Shoot();
                }

            }
        }

        else
        {
            animator.ResetTrigger("walk");
            animator.ResetTrigger("idle");
            animator.SetBool("isJumping", false);
            animator.SetBool("pistol_idle", false);
            animator.ResetTrigger("walk_back");
        }
    
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(0, 0, 0);
        }


    }

    void Shoot()
    {
        //gun.transform.TransformDirection(Vector3.forward)
        /*
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.name);

        }
        */

        //Debug.Log("fkd");

        //Debug.DrawRay(gun.transform.position, Vector3.forward * hit.distance, Color.yellow);
        //Bullet.GetComponent<Rigidbody>().AddForce(gun.transform.TransformDirection(Vector3.forward) * 10f);
        RaycastHit hit;

        Vector3 dusman_konumu = new Vector3(0,0, transform.position.z + 100);

        if (Physics.Raycast(gun.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Debug.Log(hit.transform.name);
            //Debug.DrawRay(gun.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            dusman_konumu = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
            hizvektoru = Vector3.Normalize(dusman_konumu - gun.transform.position);

        }

        else
        {
            hizvektoru = new Vector3(0, 0, 1);
        }

        //Time.timeScale = 0.1f;

        Bullet = Instantiate(bullet0, bullet_transform.position, gun.transform.rotation);
        Bullet.GetComponent<Rigidbody>().velocity = hizvektoru * ammoSpeed;
        //Debug.Log(hizvektoru);
        //Bullet.GetComponent<Rigidbody>().AddForce(0f, -150f, 0f);
        Destroy(Bullet, destroyTime);

        Muzzle = Instantiate(muzzle, muzzle_transform.position, Quaternion.Euler(0,90,0));

        GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, muzzle_transform.position, Quaternion.identity);
        LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();
        lineR.SetPosition(0, muzzle_transform.position);
        lineR.SetPosition(1, dusman_konumu);

        Destroy(bulletTrailEffect, 1.5f);

        gun_audio.Play();


        //Destroy(Muzzle, 1f);
    }
    int ct = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet_Enemy"))
        {
            ct++;
            Destroy(collision.gameObject);
            Debug.Log(ct);
        }
    }
}
