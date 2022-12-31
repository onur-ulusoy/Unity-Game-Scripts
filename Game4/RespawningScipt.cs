using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;
using Photon.Realtime;
using UnityEngine.PlayerLoop;

public class RespawningScipt : Photon.MonoBehaviour
{
    public float kalan_sure;
    public TextMeshProUGUI text;

    public void Start()
    {
        kalan_sure = 6;
    }
    public void Update()
    {

        if ((int)kalan_sure == 0)
        {
            kalan_sure = 6;
            this.enabled = false;
            transform.Find("respawnText").gameObject.SetActive(false);
        }

        kalan_sure -= Time.deltaTime;
        text.text = (int)kalan_sure + " saniye içinde yeniden doğulacak...";

    }
}
