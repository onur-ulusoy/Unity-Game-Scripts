using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{   
    public Animator animator;
    public CapsuleCollider collider1;
    public BoxCollider collider2;
    
    [Header("Attack")]
    public Transform bullet_transform;
    public GameObject bullet0; //prefab
    private GameObject Bullet;

    public GameObject muzzle;
    private GameObject Muzzle;
    public Transform muzzle_transform;

    public Vector3 hizvektoru;
    public float ammoSpeed;

    public Transform gun;

    private float random_value;
    private float Timer;

    public float destroyTime;

    private void Start()
    {
        random_value = Random.Range(1, 8);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet_Player"))
        {
            animator.SetBool("died", true);
            collider1.enabled = false;
            collider2.enabled = true;


            Destroy(gameObject, 10);
            Destroy(collision.gameObject);
        }
    }

    void Shoot()
    {
        Bullet = Instantiate(bullet0, bullet_transform.position, gun.transform.rotation);
        Bullet.GetComponent<Rigidbody>().velocity = hizvektoru * ammoSpeed;
        Bullet.tag = "Bullet_Enemy";
        //Bullet.GetComponent<Rigidbody>().AddForce(0f, -150f, 0f);
        Destroy(Bullet, destroyTime);

        Muzzle = Instantiate(muzzle, muzzle_transform.position, gun.transform.rotation);
        Destroy(Muzzle, 0.2f);
    }

    private void Update()
    {
        /*
        if ((Mathf.Round(Time.time) % random_value == 0) && (Time.time > random_value))
        {
            random_value = Random.Range(1, 10);
            Shoot();
        }
        */

        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, Vector3.forward * -1, out hit, 15))
        {
            //Debug.Log(hit.transform.name);
            //Debug.DrawRay(gun.transform.position, Vector3.forward * -1 * hit.distance, Color.yellow);
            
            if (hit.transform.CompareTag("Player") && Time.time - Timer > random_value)
            {
                Timer = Time.time;
                Shoot();
                random_value = Random.Range(1, 8);
            }
            
        }

    }
}
