using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class oyunuBaslat : Photon.MonoBehaviour
{
    public GameObject contentRoom;
    public bool HasStarted = false;
    public bool _started;
    public void StartGame()
    {
        HasStarted = true;
    }

    private void Update()
    {
        if (!photonView.isMine)
        {
            this.GetComponent<oyunuBaslat>().HasStarted = _started;
        }
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.GetComponent<oyunuBaslat>().HasStarted);
        }

        else
        {
            _started = (bool)stream.ReceiveNext();
        }
    }
}
