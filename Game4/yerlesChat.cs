using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yerlesChat : MonoBehaviour
{


    void Update()
    {
        try
        {
            if (transform.parent != GameObject.Find("ContentChat").transform)
            {
                transform.SetParent(GameObject.Find("ContentChat").transform);
            }
        }
       
        catch
        {
            ;
        }
        

    }


}
