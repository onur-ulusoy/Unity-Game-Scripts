using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yerlestirme : MonoBehaviour
{
    public Transform _transform;
    void Update()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("room"))
        {
            item.transform.SetParent(_transform);

        }
    }


}
