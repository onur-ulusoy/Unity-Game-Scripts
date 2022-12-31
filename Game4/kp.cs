using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;

public class kp : Photon.MonoBehaviour
{
    public GameObject panelPrefab;
    public GameObject selfpanelPrefab;
    public Transform konum;
    public PlayerMovement pm;
    public fightobjScript fos;
    bool ct = false;

    [PunRPC]
    public void oldu(int k, string isim)
    {
        int i = 0;
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        //fos = GameObject.FindWithTag("fight").GetComponent<fightobjScript>();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("fight"))
        {
            i++;
            if (i == k)
            {
                fos = item.GetComponent<fightobjScript>();
                ct = true;
            }
        }

        if (ct)
        {
            Destroy(fos);
            GameObject panel = Instantiate(panelPrefab, konum);
            panel.transform.Find("Player1").GetComponent<TextMeshProUGUI>().text = fos.Vuran;
            panel.transform.Find("Player2").GetComponent<TextMeshProUGUI>().text = fos.Vurulan;
            Destroy(panel, 5);
            /*
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.transform.Find("Canvas").transform.Find("Nickname").GetComponent<TextMeshProUGUI>().text == fos.Vuran)
                {
                    item.GetComponent<skorSc>().skor += 5;
                    item.GetComponent<skorSc>().temizle();

                }
                item.GetComponent<skorSc>().Start();
            }*/

        }
        
        else
        {
            GameObject panel = Instantiate(selfpanelPrefab, konum);
            panel.transform.Find("Player2").GetComponent<TextMeshProUGUI>().text = isim;
            Destroy(panel, 5);

        }

        ct = false;


    }
}
