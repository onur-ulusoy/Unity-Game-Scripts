using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using System.IO;

public class KasmaProblemBullet : Photon.MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    float gecikme = 5;
    bool aktifmi;
    public string plname;

    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, position, gecikme * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, gecikme * Time.deltaTime);
            //transform.gameObject.SetActive(aktifmi);
            transform.GetComponent<mermiscript>().playerName = plname;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(transform.gameObject.activeSelf);
            stream.SendNext(transform.GetComponent<mermiscript>().playerName);

        }

        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            //aktifmi = (bool)stream.ReceiveNext();
            plname = (string)stream.ReceiveNext();
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);           
        }
    }*/
}
