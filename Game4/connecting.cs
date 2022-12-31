using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connecting : MonoBehaviour
{
    public GameObject roompanel;
    void Start()
    {
        roompanel.GetComponent<PhotonView>().RPC("olustur", PhotonTargets.All);

        
    }

    

}
