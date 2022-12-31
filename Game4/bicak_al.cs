using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;


public class bicak_al : Photon.MonoBehaviour
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
    public int sayisi;


    void Start()
    {
        sayi = GameObject.Find("sayi");
        sayisi = 3;
        sayi.GetComponent<TextMeshProUGUI>().text = sayisi.ToString();
        bulletRot = 0;
        starting = GameObject.Find("atmayeri3");
        starting2 = GameObject.Find("atmayeri4");
        absStart = starting;
        buton = GameObject.Find("Buttonbicak");
        buton.transform.parent.GetComponent<Canvas>().enabled = false;
        butoncomp = buton.GetComponent<Button>();
        butoncomp.onClick.AddListener(() => click());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "knife")
        {
            buton.transform.parent.GetComponent<Canvas>().enabled = true;
            collision.gameObject.SetActive(false);

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
            GameObject bicak = PhotonNetwork.Instantiate("knife_blue", absStart.transform.position, Quaternion.Euler(0, 0, bulletRot), 0);
            bicak.GetComponent<Rigidbody2D>().AddForce(new Vector3(forceX, forceY, 0));
            bicak.GetComponent<bicakscript>().playerName = transform.Find("Canvas").transform.Find("Nickname").GetComponent<TextMeshProUGUI>().text;

            forceX = Mathf.Abs(forceX);
            bulletRot = 0;
            absStart = starting;

            sayisi -= 1;
            sayi.GetComponent<TextMeshProUGUI>().text = sayisi.ToString();
        }
        
    }

}
