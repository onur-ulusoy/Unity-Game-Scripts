using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class yaziyaz : Photon.MonoBehaviour
{ //gonderenartiileti
    public InputField input;
    public void yazi_gönder()
    {
        GameObject yazi = PhotonNetwork.Instantiate("yazilar", Vector3.zero, Quaternion.Euler(0, 0, 0), 0);
        yazi.transform.Find("gonderenartiileti").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.playerName + ": " + input.text;
        yazi.transform.SetParent(this.transform);
        yazi.GetComponent<PhotonView>().viewID = 0;
        input.text = "";
    }
}
