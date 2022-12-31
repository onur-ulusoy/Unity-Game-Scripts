using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class roomgorunmesorunu : Photon.MonoBehaviour
{

    public Transform _parent;
    public LayoutElement le;

    private void Start()
    {
        if (GameObject.Find("JoinScript").GetComponent<FindaRoom1>().odayagirdi)
        {
            _parent = GameObject.Find("ContentRoom").transform;
        }

    }
    
    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            try
            {
                transform.SetParent(_parent);
                transform.gameObject.GetComponent<LayoutElement>().layoutPriority = le.layoutPriority;

            }

            catch
            {
                ;
            }
            
        }

        else
        {
            transform.gameObject.GetComponent<LayoutElement>().layoutPriority = Random.Range(2, 10);
        }
    }
    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.parent);
            stream.SendNext(transform.gameObject.GetComponent<LayoutElement>());
        }

        else
        {
            _parent = (Transform)stream.ReceiveNext();
            le = (LayoutElement)stream.ReceiveNext();
        }
    }
}
