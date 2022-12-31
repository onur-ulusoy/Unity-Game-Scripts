using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class OdayaGir : Photon.MonoBehaviour
{
    public GameObject canvas1;
    public TextMeshProUGUI odaismi;

    public void Start()
    {
        canvas1 = GameObject.Find("Canvas");
    }
    Vector3 ekrandanKaydır = new Vector3(3200, 0, 0);
    public void Click ()
    {
        Debug.Log("fddf");

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("canvas3"))
        {
            if (item.GetComponent<roomInfo>().odaismi == odaismi.text)
            {
                //canvas1.SetActive(false);
                if (!GameObject.Find("JoinScript").GetComponent<FindaRoom1>().odayagirdi)
                {
                    GameObject player = PhotonNetwork.Instantiate("odadakiPlayer", Vector3.zero, Quaternion.identity, 0);
                    player.transform.Find("plName").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.playerName;
                    player.transform.SetParent(item.transform.Find("teamListings").Find("Scroll View").Find("Viewport").Find("ContentRoom"));
                    player.transform.Find("enterRoom").GetComponent<enteringRoom>().oda = item.GetComponent<roomInfo>().odaismi;

                }
                Start();
                canvas1.GetComponent<Canvas>().enabled = false;
                item.GetComponent<Canvas>().enabled = true;
                GameObject.FindWithTag("room").transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
                if (!photonView.isMine)
                {
                    item.transform.Find("ButtonStart").GetComponent<Button>().interactable = false;

                }
                /*
                canvas1.transform.Find("background").gameObject.SetActive(false);
                canvas1.transform.Find("roomname").gameObject.SetActive(false);
                canvas1.transform.Find("owner").gameObject.SetActive(false);
                canvas1.transform.Find("roomListings").transform.position = ekrandanKaydır;
                canvas1.transform.Find("Button").gameObject.SetActive(false);
                */


                //GameObject.FindWithTag("roomplayer").transform.SetParent(item.transform.Find("teamListings").Find("Scroll View").Find("Viewport").Find("ContentRoom"));



            }
        }
    }
}
