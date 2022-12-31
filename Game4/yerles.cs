using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yerles : MonoBehaviour
{


    void Update()
    {
        try
        {
            if (transform.parent != GameObject.Find("Content").transform)
            {
                transform.SetParent(GameObject.Find("Content").transform);
            }
        }
       
        catch
        {
            ;
        }
        

    }


}
