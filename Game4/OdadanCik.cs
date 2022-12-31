using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;

public class OdadanCik : Photon.MonoBehaviour
{
    public Canvas canvas;
    public GameObject canvas1;
    public roomInfo RmInfo;

    void Start()
    {
        canvas1 = GameObject.Find("Canvas");
    }
    public void Click ()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("roomplayer"))
        {
            if (item.transform.Find("plName").GetComponent<TextMeshProUGUI>().text == PhotonNetwork.playerName)
            {
                PhotonNetwork.Destroy(item);
                GameObject.Find("JoinScript").GetComponent<FindaRoom1>().odayagirdi = false;

            }
        }
        
        canvas.enabled = false;
        if (PhotonNetwork.playerName == RmInfo.yaratan)
        {
            PhotonNetwork.Destroy(this.gameObject);

            foreach (GameObject item in GameObject.FindGameObjectsWithTag("room"))
            {
                if (item.transform.Find("Canvas").transform.Find("owner").GetComponent<TextMeshProUGUI>().text == PhotonNetwork.playerName)
                {
                    PhotonNetwork.Destroy(item);
                    GameObject dagildi = PhotonNetwork.Instantiate("zDagildi", Vector3.zero, Quaternion.identity, 0);
                }
            }

        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("room"))
        {
            item.transform.Find("Canvas").GetComponent<Canvas>().enabled = true;
        }
        //GameObject.FindWithTag("room").transform.Find("Canvas").GetComponent<Canvas>().enabled = true;
        canvas1.GetComponent<Canvas>().enabled = true;
    }
}
