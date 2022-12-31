using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectDetectController : MonoBehaviour
{
    public List<GameObject> neighborObjects;
    GameObject collisionGameObject;
    GameObject BoxManager;
    public int colorNo;
    public int column;
    public float stabilityPeriod;

    public BoxCollider2D[] colliders;
    private void Start()
    {
        neighborObjects = new List<GameObject>();
        BoxManager = GameObject.FindGameObjectWithTag("BoxManager");   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var item in collision.GetComponents<BoxCollider2D>())
        {
            if (!item.enabled)
                item.enabled = true;
        }

        if (collision.transform.CompareTag("Box"))
        {
            if (collision.name == transform.name)
            {
                collisionGameObject = collision.transform.gameObject;

                if (!neighborObjects.Contains(collisionGameObject))
                    neighborObjects.Add(collisionGameObject);
            }
        }

        List<GameObject> neighborTempObjects = new List<GameObject>();
        
        foreach (var item1 in neighborObjects)
        {
            foreach (var item2 in item1.GetComponent<objectDetectController>().neighborObjects)
                neighborTempObjects.Add(item2);
        }

        foreach (var item in neighborTempObjects)
        {
            if (!neighborObjects.Contains(item))
                neighborObjects.Add(item);
        }

        CancelInvoke();
        Invoke("ChangeSprites", stabilityPeriod);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Box"))
        {
            collisionGameObject = collision.transform.gameObject;

            if (neighborObjects.Contains(collisionGameObject))
            {
                neighborObjects.Clear();
                ColliderRefresh();
            }
        }

        if (neighborObjects.Count == 0)
            neighborObjects.Add(gameObject);

        CancelInvoke();
        Invoke("ChangeSprites", stabilityPeriod);
    }

    public void ColliderRefresh()
    {
        colliders[0].enabled = false;
        colliders[0].enabled = true;

        colliders[1].enabled = false;
        colliders[1].enabled = true;
    }

    public void onDestroy(int rowIndex)
    {
        int[] columns = { column };
        BoxManager.GetComponent<BoxController>().createBoxes(rowIndex, columns);
    }

    private void ChangeSprites()
    {
        BoxManager.GetComponent<BoxController>().ObjectsCountControl(neighborObjects, colorNo);
    }
}
