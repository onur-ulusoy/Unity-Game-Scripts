using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;

public class bombascript : Photon.MonoBehaviour
{
    PlayerMovement pm;
    public string playerName;
    public GameObject FightPrefab;
    public GameObject fightObject;
    private fightobjScript fs;
    bool bt = true;
    public CircleCollider2D buyuk, kucuk;
    public float süre = 5;
    GameObject patliyor;
    bool patladi = false;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    void Start()
    {

    }

    private void Update()
    {
        süre -= Time.deltaTime;

        if (süre <= 2 && süre > 0 && !patladi)
        {
            rb.bodyType = RigidbodyType2D.Static;
            sr.enabled = false;
            patliyor = PhotonNetwork.Instantiate("bomba_patliyor", this.transform.position, Quaternion.Euler(0, 0, 0), 0);
            patladi = true;
        }

        if (süre <= 0)
        {
            PhotonNetwork.Destroy(patliyor);
            buyuk.enabled = true;
            GameObject patlama = PhotonNetwork.Instantiate("patlama", new Vector3(this.transform.position.x, this.transform.position.y + 4, this.transform.position.z), Quaternion.Euler(0, 0, 0), 0);
            Destroy(patlama, 3);
            Destroy(this.gameObject,0.5f);
            süre = 9000;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Destroy(this.gameObject,0.5f);
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.HP -= 800;

            foreach (GameObject item in GameObject.FindGameObjectsWithTag("fight"))
            {
                if (item.GetComponent<fightobjScript>().Vurulan == collision.transform.Find("Canvas").transform.Find("Nickname").gameObject.GetComponent<TextMeshProUGUI>().text)
                {
                    bt = false;
                }
            }

            if (bt)
            {
                fightObject = Instantiate(FightPrefab);
                fightObject.GetComponent<fightobjScript>().Vurulan = collision.transform.Find("Canvas").transform.Find("Nickname").gameObject.GetComponent<TextMeshProUGUI>().text;
                fightObject.GetComponent<fightobjScript>().Vuran = playerName;
            }
            
            

        }
        /*
        if (collision.transform.tag == "tilemap" || collision.transform.tag == "Mace" || collision.transform.tag == "Platform" || collision.transform.tag == "Spike" || collision.transform.tag == "Stone")
        {
            Destroy(this.gameObject, 0.25f);
        }*/
    }

}
