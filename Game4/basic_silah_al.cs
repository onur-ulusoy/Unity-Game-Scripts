using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Realtime;

public class basic_silah_al : Photon.MonoBehaviour
{
    public GameObject buton;
    public GameObject hand_gun;
    bool aktif = false;



    private void Start()
    {
        buton = GameObject.Find("Button");
        hand_gun = GameObject.Find("gun_blue1");
        buton.transform.parent.GetComponent<Canvas>().enabled = false;
        this.hand_gun.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "basic_silah")
        {
            buton.transform.parent.GetComponent<Canvas>().enabled = true;
            collision.gameObject.SetActive(false);

            if(photonView.isMine)
            {
                this.hand_gun.SetActive(true);
                aktif = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            this.hand_gun.SetActive(aktif);
            //silah.SetActive(!aktif);

        }

        if (!aktif)
        {
            buton.transform.parent.GetComponent<Canvas>().enabled = false;

        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(hand_gun.gameObject.activeSelf);
        }

        else
        {
            aktif = (bool)stream.ReceiveNext();
        }
    }

}
