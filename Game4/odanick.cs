using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class odanick : Photon.MonoBehaviour
{
    public TextMeshProUGUI nick;
    public string _nick;
    void Start()
    {
        if (!photonView.isMine)
        {
            nick.text = _nick;
        }
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.GetComponent<TextMeshProUGUI>().text);

        }

        else
        {
            _nick = (string)stream.ReceiveNext();
        }
        Start();
    }
}
