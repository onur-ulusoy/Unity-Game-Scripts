using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using TMPro;

public class mermiscript : Photon.MonoBehaviour
{
    PlayerMovement pm;
    public string playerName;
    public GameObject FightPrefab;
    public GameObject fightObject;
    private fightobjScript fs;
    bool bt = true;
    public Animator animator;

    void Start()
    {
        Destroy(this.gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            animator.SetBool("carpti", true);
            Destroy(this.gameObject,0.5f);
            pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.HP -= 300;

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

        if (collision.transform.tag == "tilemap" || collision.transform.tag == "Mace" || collision.transform.tag == "Platform" || collision.transform.tag == "Spike" || collision.transform.tag == "Stone")
        {
            animator.SetBool("carpti", true);
            Destroy(this.gameObject, 0.25f);
        }
    }

}
