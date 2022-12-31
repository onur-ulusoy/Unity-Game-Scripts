using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class FindaRoom1 : Photon.MonoBehaviour
{

    public Transform content, contentRoom;
    public GameObject canvas1, canvas2;
    public GameObject loadScreen;
    public Button buton;

    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("V2");
        FindGameButton();

    }
    public void FindGameButton()
    {
        StartPairing();        
        Debug.Log("FindGame");
    }
    public void StartPairing()
    {
        CancelInvoke("RequestJoind");
        requestCountLocal = 0;
        InvokeRepeating("RequestJoind", 0.1f, 0.1f);

    }
    public void StopPairing()
    {
        CancelInvoke("RequestJoind");
    }
    public int requestCount = 30;
    public int requestCountLocal;
    public void RequestJoind()
    {
        requestCountLocal++;
        Debug.Log(requestCountLocal > requestCount);
        if (requestCountLocal > requestCount)
        {
            PhotonNetwork.CreateRoom(Random.Range(0.0f, 33339.2f).ToString(), new RoomOptions() { MaxPlayers = 8 }, null);
            CancelInvoke("RequestJoind");
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public InputField input;
    public string roomName;
    public bool odayagirdi = false;
    public void OnJoinedRoom()
    {
        loadScreen.SetActive(false);
        buton.interactable = true;
        if (input.text != "")
        {
            roomName = input.text;
            input.text = "";

            GameObject room = PhotonNetwork.Instantiate("room", Vector3.zero, Quaternion.identity, 0);
            GameObject room_real = PhotonNetwork.Instantiate("Canvas3", Vector3.zero, Quaternion.identity, 0);

            room_real.GetComponent<roomInfo>().odaismi = roomName;
            room_real.GetComponent<roomInfo>().yaratan = PhotonNetwork.playerName;
            GameObject player = PhotonNetwork.Instantiate("odadakiPlayer", Vector3.zero, Quaternion.identity, 0);
            player.transform.Find("plName").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.playerName;
            contentRoom = room_real.transform.Find("teamListings").Find("Scroll View").Find("Viewport").Find("ContentRoom");
            player.transform.SetParent(contentRoom);
            player.transform.Find("enterRoom").GetComponent<enteringRoom>().oda = roomName;
            player.transform.Find("MasterLogo").GetComponent<RawImage>().enabled = true;
            odayagirdi = true;
            room.transform.Find("Canvas").transform.Find("roomName").GetComponent<TextMeshProUGUI>().text = roomName;
            room.transform.Find("Canvas").transform.Find("owner").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.playerName;
            room.transform.SetParent(content);
            
            room.GetComponent<OdayaGir>().Start();
            room.GetComponent<OdayaGir>().Click();

            //canvas1.SetActive(true);
            canvas2.SetActive(false);
        }

    }
    public void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }
    public void GeriDon()
    {
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("OdaSecme");
    }
    public void OnJoinRandomFailed(short returnCode, string message)
    {
        if (requestCountLocal >= 30)
        {
            Debug.Log("Random Error Katilamadik.");
        }
    }
    public void OnLeftLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
    public void TekrarDene()
    {
        SceneManager.GetActiveScene();
    }
    public void BackMain() {
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("SelectNumber");
    }
}
/*
	---------> Odakurma
		-> Oda kura tetiklendikden sonra 2 saniye boyunca RandomJoindRoom yapmaya calisicak 2 saniye bittikden sonra sifirdan oda kurucak
		-> Oda 2 kisi girdikden sonra odanin gorunulugunu kapatilicak.

*/
