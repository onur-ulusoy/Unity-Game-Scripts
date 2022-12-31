using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class enteringRoom : Photon.MonoBehaviour
{
    public string _odaismi;
    public string oda;
    public GameObject plyr;



    private void Update()
    {
        
        if (!photonView.isMine)
        {
            transform.GetComponent<enteringRoom>().oda = _odaismi;


        }




        foreach (GameObject item in GameObject.FindGameObjectsWithTag("canvas3"))
        {
            if (item.GetComponent<roomInfo>().odaismi == oda)
            {
                plyr.transform.SetParent(item.transform.Find("teamListings").Find("Scroll View").Find("Viewport").Find("ContentRoom"));
            }
        }
    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.GetComponent<enteringRoom>().oda);
        }

        else
        {
            _odaismi = (string)stream.ReceiveNext();
        }
    }
}
