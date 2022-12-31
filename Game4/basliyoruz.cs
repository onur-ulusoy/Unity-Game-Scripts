using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Realtime;

public class basliyoruz : Photon.MonoBehaviour
{
    public float zaman = 4;
    public bool basladi;
    public Transform Cr;

    void Update()
    {
        if (photonView.isMine)
        {
            try
            {
                basladi = this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.Find("Started").GetComponent<oyunuBaslat>().HasStarted;
                Cr = this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.Find("Started").GetComponent<oyunuBaslat>().contentRoom.transform;

                if (basladi && Cr == this.transform.parent)
                {
                    zaman -= Time.deltaTime;
                    GameObject.Find("LoadingCanvas").GetComponent<Canvas>().enabled = true;
                    if (zaman <= 0)
                    {
                        GameObject.Find("LoadingCanvas").GetComponent<Canvas>().enabled = false;
                        SceneManager.LoadScene("2 2");
                    }
                }
            }

            catch
            {
                ;
            }
        }
        
    }
}
