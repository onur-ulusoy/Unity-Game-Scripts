using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon;
using Photon.Realtime;
using UnityEngine.PlayerLoop;

public class roomInfo : Photon.MonoBehaviour
{
    public string odaismi;
    public string _odaismi;
    public TextMeshProUGUI _textodaismi;
    public string yaratan;
    public string _yaratan;
    public Canvas canvas;

    void Awake()
    {
        canvas.enabled = false;
    }
    private void Start()
    {

        if (!photonView.isMine)
        {
            odaismi = _odaismi;
            yaratan = _yaratan;
        }
        _textodaismi.text = odaismi + " adlı oda";
    }
    /*
    void Update()
    {
        if (yaratan != PhotonNetwork.playerName)
        {
            canvas.enabled = false;
        }
    }*/

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.GetComponent<roomInfo>().odaismi);
            stream.SendNext(this.GetComponent<roomInfo>().yaratan);

        }

        else
        {
            _odaismi = (string)stream.ReceiveNext();
            _yaratan = (string)stream.ReceiveNext();
        }
        Start();
    }
}
