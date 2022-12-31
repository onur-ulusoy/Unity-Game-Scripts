using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class FindaRoom : Photon.MonoBehaviour
{
    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("V2");
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
    public void OnJoinedRoom()
    {
        PhotonNetwork.playerName = input.text;
        PhotonNetwork.LoadLevel("2 2");
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
