using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using System.IO;

public class KasmaProblemBicak : Photon.MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    float gecikme = 5;
    public string plname;

    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, position, gecikme * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, gecikme * Time.deltaTime);
            transform.GetComponent<bicakscript>().playerName = plname;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.GetComponent<bicakscript>().playerName);

        }

        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            plname = (string)stream.ReceiveNext();
        }
    }
    
}
