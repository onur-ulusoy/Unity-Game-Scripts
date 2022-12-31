using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;
public class fightobjScript : Photon.MonoBehaviour
{
    
    public string Vuran;
    public string Vurulan;

    private void Start()
    {
        Destroy(this.gameObject, 5);
    }


}

