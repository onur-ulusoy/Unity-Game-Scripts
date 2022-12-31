using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;
using TMPro;

public class createRoom : Photon.MonoBehaviour
{
    public Transform content;
    //public GameObject roomListingPrefab;

    void Start()
    {
        //GameObject room = Instantiate(roomListingPrefab, content);
        GameObject room = PhotonNetwork.Instantiate("room", Vector3.zero, Quaternion.identity, 0);
        room.transform.Find("Canvas").transform.Find("roomName").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.playerName;
        room.transform.SetParent(content);

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("room"))
        {
            PhotonNetwork.Destroy(item);
            break;
        }
        
    }
   
    
}
