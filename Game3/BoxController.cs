using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject boxPrefab;
    public float distance; //from a transform a box to another
    public Transform boxParent;

    [Header("Sprites")]
    public Sprite[] blueBoxes;
    public Sprite[] greenBoxes;
    public Sprite[] pinkBoxes;
    public Sprite[] purpleBoxes;
    public Sprite[] redBoxes;
    public Sprite[] yellowBoxes;

    static List<Sprite[]> Sprites;
    Vector3 boxPositionVector;
    int colorRandomNumber;

    void Start()
    {
        Sprites = new List<Sprite[]>() { blueBoxes, greenBoxes, pinkBoxes, purpleBoxes, redBoxes, yellowBoxes };
        int[] columns = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        for (int i = 0; i < 9; i++)
            createBoxes(i, columns);
 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit;

            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                if (hit.transform.CompareTag("Box"))
                {
                    List<GameObject> neighborTempObjects = new List<GameObject>(hit.transform.GetComponent<objectDetectController>().neighborObjects);
                    int rawNumber = 0;
                    if (neighborTempObjects.Count > 1)
                    {
                        foreach (var item in neighborTempObjects)
                        {
                            var odc = item.GetComponent<objectDetectController>();
                            odc.neighborObjects.Clear();
                            odc.onDestroy(rawNumber++);
                            Destroy(item);
                        }
                    }
                }
            }
        }     
    }

    public void ObjectsCountControl(List<GameObject> neighborObjects, int colorNo)
    {
        int objectCount = neighborObjects.Count;

        if (objectCount < 3)
            ChangeSprite(3);

        else if (objectCount > 2 && objectCount < 6)
            ChangeSprite(0);

        else if (objectCount > 5 && objectCount < 8)
            ChangeSprite(1);

        else if (objectCount > 8)
            ChangeSprite(2);

        void ChangeSprite(int spriteNo)
        {
            foreach (var item in neighborObjects)
            {
                item.GetComponent<SpriteRenderer>().sprite = Sprites[colorNo][spriteNo];
            }
        }

    }

    public void createBoxes(int rawNo, int[] columns)
    {
        foreach (var columnNo in columns)
        {
            boxPositionVector = new Vector3(transform.position.x + columnNo * distance, transform.position.y + rawNo * .6f, 0f);
            GameObject box = Instantiate(boxPrefab, boxPositionVector, Quaternion.identity);
            colorRandomNumber = Random.Range(0, 6);
            box.GetComponent<SpriteRenderer>().sprite = Sprites[colorRandomNumber][3];

            switch (colorRandomNumber)
            {
                case 0:
                    box.name = "blue";
                    break;
                case 1:
                    box.name = "green";
                    break;
                case 2:
                    box.name = "pink";
                    break;
                case 3:
                    box.name = "purple";
                    break;
                case 4:
                    box.name = "red";
                    break;
                case 5:
                    box.name = "yellow";
                    break;
                default:
                    break;
            }

            var odc = box.GetComponent<objectDetectController>();
            odc.colorNo = colorRandomNumber;
            odc.column = columnNo;
            box.transform.SetParent(boxParent);
        }
    }
}
