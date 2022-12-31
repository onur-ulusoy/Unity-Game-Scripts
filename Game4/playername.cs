using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class playername : Photon.MonoBehaviour
{
    public InputField _text;

    public GameObject canvas0, canvas1;
    public void Click()
    {
        PhotonNetwork.playerName = _text.text;
        canvas0.SetActive(false);
        canvas1.SetActive(true);
    }

}
