using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MasterLogoGozukme : Photon.MonoBehaviour
{
    public GameObject MLogo;
    public bool gozukuyor;
    private void Update()
    {

        if (!photonView.isMine)
        {
            this.GetComponent<RawImage>().enabled = gozukuyor;
        }
    }

    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.GetComponent<RawImage>().enabled);
        }

        else
        {
            gozukuyor = (bool)stream.ReceiveNext();

        }
    }
}
