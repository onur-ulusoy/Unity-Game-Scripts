using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;


public class bomba_al : Photon.MonoBehaviour
{
    public GameObject buton;
    public Button butoncomp;
    public GameObject starting;
    public GameObject starting2;
    public GameObject absStart;
    public float bulletRot;
    public SpriteRenderer SR;
    public float forceX, forceY;
    public GameObject sayi;
    public int sayisi = 0;


    void Start()
    {
        sayi = GameObject.Find("sayi2");
        sayi.GetComponent<TextMeshProUGUI>().text = sayisi.ToString();
        bulletRot = 0;
        starting = GameObject.Find("atmayeri3");
        starting2 = GameObject.Find("atmayeri4");
        absStart = starting;
        buton = GameObject.Find("Buttonbomba");
        buton.transform.parent.GetComponent<Canvas>().enabled = false;
        butoncomp = buton.GetComponent<Button>();
        butoncomp.onClick.AddListener(() => click());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "bomb")
        {
            buton.transform.parent.GetComponent<Canvas>().enabled = true;
            collision.gameObject.SetActive(false);
            sayisi += 2;
            sayi.GetComponent<TextMeshProUGUI>().text = sayisi.ToString();

        }
    }

    void click()
    {
        if (sayisi > 0)
        {
            if (SR.flipX)
            {
                forceX *= -1;
                bulletRot = 180;
                absStart = starting2;

            }
            GameObject bomba = PhotonNetwork.Instantiate("bomb", absStart.transform.position, Quaternion.Euler(0, 0, bulletRot), 0);
            bomba.GetComponent<Rigidbody2D>().AddForce(new Vector3(forceX, forceY, 0));
            bomba.GetComponent<bombascript>().playerName = transform.Find("Canvas").transform.Find("Nickname").GetComponent<TextMeshProUGUI>().text;

            forceX = Mathf.Abs(forceX);
            bulletRot = 0;
            absStart = starting;

            sayisi -= 1;
            sayi.GetComponent<TextMeshProUGUI>().text = sayisi.ToString();
        }
        
    }

}
