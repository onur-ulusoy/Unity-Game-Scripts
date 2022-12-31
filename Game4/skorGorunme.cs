using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class skorGorunme : Photon.MonoBehaviour
{
    public string _text;

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.GetComponent<TextMeshProUGUI>().text = _text;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.GetComponent<TextMeshProUGUI>().text);


        }

        else
        {
            _text = (string)stream.ReceiveNext();

        }
    }
}
