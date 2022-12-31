using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using System;
using TMPro;

public class cephane_al : Photon.MonoBehaviour
{
    public GameObject mermi1;
    public GameObject mermi2;
    public GameObject mermi3;
    public GameObject mermi4;
    public GameObject mermi5;
    public GameObject mermi6;
    public bool m = true;

    int i = 6;
    public GameObject starting;
    public GameObject starting2;
    public GameObject absStart;
    public GameObject bullet;
    private PlayerMovement plM_classi;
    public GameObject plM;
    public float bulletX, bulletY;
    public float bulletRot;
    public GameObject gunblue;
    public SpriteRenderer gunblueSR;
    public GameObject yer2, yer1;
    public SpriteRenderer SR;
    public Button butoncomp;
    public bool kendim = true;


    public void Start()
    {
        if (kendim)
        {
            bulletRot = 0;
            starting = GameObject.Find("atmayeri1");
            starting2 = GameObject.Find("atmayeri2");
            mermi1 = GameObject.Find("Ammo1");
            mermi2 = GameObject.Find("Ammo2");
            mermi3 = GameObject.Find("Ammo3");
            mermi4 = GameObject.Find("Ammo4");
            mermi5 = GameObject.Find("Ammo5");
            mermi6 = GameObject.Find("Ammo6");
            mermi6.SetActive(false);
            mermi5.SetActive(false);
            mermi4.SetActive(false);
            mermi3.SetActive(false);
            mermi2.SetActive(false);
            mermi1.SetActive(false);


            plM_classi = plM.GetComponent<PlayerMovement>();
            gunblueSR = gunblue.GetComponent<SpriteRenderer>();
        }
        

    }
    public void FixedUpdate()
    {

        if (SR.flipX)
        {
            gunblueSR.flipX = true;
            gunblue.transform.position = yer2.transform.position;
        }

        else
        {
            gunblueSR.flipX = false;
            gunblue.transform.position = yer1.transform.position;
        }
    }
   
    public void click()
    {
        if (SR.flipX)
        {
            bulletX *= -1;
            bulletRot = 180;
            absStart = starting2;

        }

        if (kendim)
        {
            if (i != 0)
            {
                if (i == 6)
                {
                    mermi6.SetActive(false);
                    i -= 1;
                    shoot();
                }

                else if (i == 5)
                {
                    mermi5.SetActive(false);
                    i -= 1;
                    shoot();
                }

                else if (i == 4)
                {
                    mermi4.SetActive(false);
                    i -= 1;
                    shoot();
                }

                else if (i == 3)
                {
                    mermi3.SetActive(false);
                    i -= 1;
                    shoot();
                }

                else if (i == 2)
                {
                    mermi2.SetActive(false);
                    i -= 1;
                    shoot();
                }

                else if (i == 1)
                {
                    mermi1.SetActive(false);
                    i -= 1;
                    shoot();
                }

            }
        }
        

        //Debug.Log(i);
        bulletX = Mathf.Abs(bulletX);
        bulletRot = 0;
        absStart = starting;


    }

    public void shoot()
    {
        if (kendim)
        {
            GameObject mermi = PhotonNetwork.Instantiate("Bullet", absStart.transform.position, Quaternion.Euler(0, 0, bulletRot), 0);
            mermi.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletX, bulletY);
            //mermi.GetComponent<mermiscript>().playerName = transform.Find("Nickname").GetComponent<TextMeshProUGUI>().text;
            mermi.GetComponent<mermiscript>().playerName = transform.Find("Canvas").transform.Find("Nickname").GetComponent<TextMeshProUGUI>().text;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ammo_box")
        {
            if (kendim)
            {
                //collision.gameObject.SetActive(false);
                if (photonView.isMine)
                {
                    mermi6.SetActive(true);
                    mermi5.SetActive(true);
                    mermi4.SetActive(true);
                    mermi3.SetActive(true);
                    mermi2.SetActive(true);
                    mermi1.SetActive(true);
                    i = 6;

                }

                if (m)
                {
                    try
                    {
                        butoncomp = GameObject.Find("Button").GetComponent<Button>();
                        butoncomp.onClick.AddListener(() => click());
                        m = false;
                    }

                    catch
                    {
                        ;
                    }

                }
            }
            

        }
    }
   


}
