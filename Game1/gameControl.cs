using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour
{
    public GameObject circle;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            circle.GetComponent<motionController>().enabled = true; //yeniden yazýlacak
            //transform.gameObject.GetComponent<wallControl>().enabled = true;
            transform.gameObject.GetComponent<gameControl>().enabled = false;

        }
    }
}
