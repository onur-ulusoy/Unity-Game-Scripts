using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using System.Linq;

public class OnlineMenager : Photon.MonoBehaviour
{
    List<Vector3> konumlar = new List<Vector3>() { new Vector3(-62.44f, 5.57f, -4.63f), new Vector3(-72.35f, 33.24f, -4.63f), new Vector3(56.28f, 30.12f, -4.63f), new Vector3(56.28f, 30.12f, -4.63f) };
    void Start()
    {
        //konumlar.Add(new Vector3(-7.7f, -2.54f, -4.63f));
        //konumlar.Add(new Vector3(-9.73f, -2.43f, -4.63f));

        PhotonNetwork.Instantiate("AstroJump_0", konumlar[Random.Range(0,2)], Quaternion.identity, 0);
    }

}
