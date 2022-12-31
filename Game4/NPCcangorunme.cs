using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class NPCcangorunme : Photon.MonoBehaviour
{

    public SkeletonMovement sm;
    public float _hp;


    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            sm.HP = _hp;
        }
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(sm.HP);


        }

        else
        {
            _hp = (float)stream.ReceiveNext();

        }
    }
}
