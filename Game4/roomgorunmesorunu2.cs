using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class roomgorunmesorunu2 : Photon.MonoBehaviour
{

    public Transform _parent;
    public LayoutElement le;
    public Transform p;
    public GameObject enterRoom;

    
    private void Start()
    {

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("canvas3"))
        {
            if (item.GetComponent<roomInfo>().odaismi == enterRoom.GetComponent<enteringRoom>().oda)
            {
                _parent = item.transform.Find("teamListings").Find("Scroll View").Find("Viewport").Find("ContentRoom");
            }
        }

            

    }
    void FixedUpdate()
    {
        //Debug.Log(transform.GetComponent<roomgorunmesorunu2>().odaismi);
        p = transform.parent;
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
