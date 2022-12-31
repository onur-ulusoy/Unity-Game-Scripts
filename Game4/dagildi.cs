using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dagildi : MonoBehaviour
{
    public Canvas canvas;

    void Update()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("dagildi"))
        {
            canvas.enabled = true;
            Destroy(item, 2);
        }
    }
}
