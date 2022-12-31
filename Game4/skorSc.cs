using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;

public class skorSc : Photon.MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public int skor = 0;
    public string mytext;
    public void Start()
    {

        tmp = GameObject.Find("skor").GetComponent<TextMeshProUGUI>();
        mytext = PhotonNetwork.playerName + " " + skor + "p";
        tmp.text += mytext;
        tmp.text += "\n";
        //tmp.text += item.transform.Find("Canvas").transform.Find("Nickname").GetComponent<TextMeshProUGUI>().text;

    }
    
    public void temizle()
    {
        tmp.text = "";
    }
}
