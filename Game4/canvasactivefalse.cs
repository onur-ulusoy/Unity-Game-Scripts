using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasactivefalse : MonoBehaviour
{
    public GameObject canvas2;
    public void Click()
    {
        //transform.gameObject.SetActive(false);

        canvas2.SetActive(true);
    }
}
