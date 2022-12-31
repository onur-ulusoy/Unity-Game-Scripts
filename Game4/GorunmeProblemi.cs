using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class GorunmeProblemi : Photon.MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    float gecikme = 8;
    void Start()
    {
        



    }
    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, position, gecikme * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, gecikme * Time.deltaTime);
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }

        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
        }

    }
}
