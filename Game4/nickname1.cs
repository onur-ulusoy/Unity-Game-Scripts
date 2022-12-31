using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class nickname1 : Photon.MonoBehaviour
{

    public TextMeshProUGUI tmp;
    public string text;


    public void Start()
    {


        if (!photonView.isMine)
        {
            tmp.text = text;
        }

        else
        {
            //tmp.text = PhotonNetwork.playerName;
        }
        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(tmp.text);

        }

        else
        {
            text = (string)stream.ReceiveNext();
        }
        Start();
    }


}
