using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class connect : Photon.MonoBehaviour
{
    void Start()
    {
        Debug.Log("Connecting to server");
        PhotonNetwork.gameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings(PhotonNetwork.gameVersion);

    }

    public void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
    }

}
