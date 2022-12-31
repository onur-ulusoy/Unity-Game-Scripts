using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class parentYazi : Photon.MonoBehaviour
{
    public Transform _parent;
    int vid;

    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            transform.SetParent(_parent);
            this.GetComponent<PhotonView>().viewID = vid;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.parent);
            stream.SendNext(this.GetComponent<PhotonView>().viewID);
        }

        else
        {
            _parent = (Transform)stream.ReceiveNext();
            vid = (int)stream.ReceiveNext();

        }
    }
}
