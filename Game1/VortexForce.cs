using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexForce : MonoBehaviour
{

    public GameObject g;
    public float speedCoeff;
    public float TwirlRatio;

    float angle;
    float speed;
    public float radius;
    public float springCoef;
    float Spring = 1;
    float posX;
    float startingcharPosX;
    motionController motController;
    public GameObject cam;
    public GameObject greenEmbers;

    //[Header("Flash")]
    //public GameObject flashBackgroundObject;
    //private Material FlashMat;
    //bool flashActive = false;
    //public float flashWait;
    //public GameObject Background;
    //private Material BackgroundMat;
    
    private void Start()
    {
        speed = speedCoeff * (2 * Mathf.PI);
        Vector2 PositionVector = new Vector2(g.transform.position.x - transform.position.x, g.transform.position.y - transform.position.y);
        angle = Mathf.Atan(PositionVector.y / PositionVector.x);
        if (PositionVector.y < 0 && PositionVector.x < 0)
            angle += Mathf.PI;
        //print(PositionVector.ToString());
        radius = PositionVector.magnitude;
        //print(angle);
        //angle = -Mathf.PI/2;
        startingcharPosX = g.transform.position.x;
        motController = g.GetComponent<motionController>();
        //FlashMat = flashBackgroundObject.GetComponent<MeshRenderer>().material;
        //BackgroundMat = Background.GetComponent<MeshRenderer>().material;

    }
    void Update()
    {
        if (startingcharPosX < 0)
            angle += speed * Time.deltaTime;
        else
            angle -= speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius / Spring;
        float y = Mathf.Sin(angle) * radius;

        if (radius > 0)
            Spring += springCoef * Time.deltaTime;
        else
            Spring -= springCoef * Time.deltaTime;

        radius -= TwirlRatio * Time.deltaTime;
        /*
        if (transform.position.x + x >= 0.53828f)
            posX = 0.53828f;

        else if (transform.position.x + x <= - 0.5346593)
            posX = -0.5346593f;

        else*/
            posX = transform.position.x + x;

        if (g.transform.position.x >= 0.53828f && radius < 0)
        {
            g.transform.position = new Vector3(posX, transform.position.y, g.transform.position.z);
            enabled = false;
            //transform.gameObject.SetActive(false);
            motController.inPortal = false;
            //print(Time.time);
        }

        else if (g.transform.position.x <= -0.5346593f && radius < 0)
        {
            g.transform.position = new Vector3(posX, transform.position.y, g.transform.position.z);
            enabled = false;
            //transform.gameObject.SetActive(false);
            motController.inPortal = false;

            //print(Time.time);
        }

        g.transform.position = new Vector3(posX, transform.position.y+y, g.transform.position.z);

        if (radius < 0)
        {
            greenEmbers.SetActive(true);
            //transform.GetChild(1).gameObject.SetActive(false);
            
        }

    }

    /* Wont be used can be deleted
    private IEnumerator flashEffect()
    {
        flashBackgroundObject.SetActive(true);
        for (int i = 0; i < 101; i++)
        {
            Color customColor = new Color(1f, 1f, 1f, i/100f);
            //yield return new WaitForSeconds(flashWait);
            FlashMat.color = customColor;
        }
        float colorRandomerR = Random.Range(0f, 1f);
        float colorRandomerG = Random.Range(0f, 1f);
        float colorRandomerB = Random.Range(0f, 1f);
        Color backColor = new Color(colorRandomerR, colorRandomerG, colorRandomerB, 1f);
        BackgroundMat.color = backColor;
        BackgroundMat.SetFloat("_Metallic", 1f);
        //yield return new WaitForSeconds(5);
        for (int i = 100; i >= 0; i--)
        {
            Color customColor = new Color(1f, 1f, 1f, i / 100f);
            FlashMat.color = customColor;
            yield return new WaitForSeconds(flashWait);

        }

        flashBackgroundObject.SetActive(false);
        yield return new WaitForEndOfFrame();

    }*/
}
