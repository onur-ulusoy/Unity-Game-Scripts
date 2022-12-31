using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemAreaCleaning : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("middleObs") || other.transform.CompareTag("Gem"))
        {
            //Destroy(other.gameObject);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.38f, transform.position.z);
        }

        else if (other.transform.CompareTag("baseObs"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
        
        //Yeni oyuna baþlayýnca eski yýldýzlarý haritadan kaldýr.
    }
}
