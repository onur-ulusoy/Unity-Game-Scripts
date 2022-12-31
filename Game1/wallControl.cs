using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallControl : MonoBehaviour
{
    public GameObject solduvar;
    public GameObject sagduvar;

    public int timeRange1;
    public int timeRange2;

    public float wallScaleRange1;
    public float wallScaleRange2;

    float x1;
    float x2;

    float timer = 0;
    int randomNumber;

    public GameObject obstacleLeftPrefab;
    public GameObject obstacleRightPrefab;
    GameObject obstaclePrefab;
    public GameObject middleObstaclePrefab;
    public Camera cam;

    float randomScaleX;

    Vector3 raycastStartingPosition;

    int yon = 0;
    float X;
    float Z;
    float middleX;
    public float offSet;
    public GameObject buff0Prefab;
    public int buffRandomer;
    public float buffTimer;

    public GameObject Gem0Prefab;
    public Mesh[] GemMeshes;
    public float gemRandomer;
    public float gemTimer;
    public int gemMeshRandomer;
    public int gemMeshLength;
    public int gemDirectionRandomer;
    public float gemPositionX;

    public GameObject backWall;
    Rigidbody backWallRg;
    bool backWallFlying = false;

    void Start()
    {
        x1 = solduvar.transform.position.x;
        x2 = sagduvar.transform.position.x;
        Z = solduvar.transform.position.z;
        randomNumber = Random.Range(timeRange1, timeRange2);
        buffRandomer = Random.Range(15, 25);
        gemRandomer = Random.Range(2, 5);
        buffTimer = 0;
        gemTimer = 0;
        middleX = (x1 + x2) / 2;
        gemMeshLength = GemMeshes.Length;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            backWall.transform.SetParent(null);
            backWallFlying = true;
            //backWallRg.useGravity = true;
            //backWallRg.velocity = new Vector3(0f, -20f, 0f);
        }

        if (Input.GetKey(KeyCode.E))
        {
            //backWall.transform.SetParent(cam.transform);
            backWallFlying = false;
            backWall.transform.position = new Vector3(backWall.transform.position.x, cam.transform.position.y + 22, backWall.transform.position.z);

        }

        if (cam.transform.position.y < backWall.transform.position.y)
        {
            backWall.transform.Translate(Vector3.down * Time.deltaTime * 5);
        }

        else
        {
            if (backWall.transform.parent == null)
                backWall.transform.SetParent(cam.transform);
        }

        if (backWallFlying)
        {
            backWall.transform.Translate(Vector3.down*Time.deltaTime*5);
        }
        if (Time.time > buffRandomer + buffTimer)
        {
            buffTimer = Time.time;
            buffRandomer = Random.Range(20, 35);
            GameObject buff = Instantiate(buff0Prefab, new Vector3(middleX, cam.transform.position.y + 2, Z), Quaternion.identity);
            Destroy(buff, 8);
        }

        if (Time.time > gemRandomer + gemTimer)
        {
            gemTimer = Time.time;
            gemRandomer = Random.Range(2, 5);
            gemDirectionRandomer = Random.Range(1, 3);
            if (gemDirectionRandomer == 1)
                gemPositionX = -0.541f;// new Vector3(-0.541f, cam.transform.position.y + 2, Z);
            else
                gemPositionX = 0.391f;// new Vector3(0.391f, cam.transform.position.y + 2, Z);

            int gemAmount = Random.Range(1, 4);
            gemMeshRandomer = Random.Range(0, gemMeshLength);

            for (int i = 0; i < gemAmount; i++)
            {
                
                GameObject gem = Instantiate(Gem0Prefab, new Vector3(gemPositionX, cam.transform.position.y + 2 + 0.38f*i, Z), Quaternion.identity);
                Transform coin = gem.transform.GetChild(0);
                //coin.gameObject.SetActive(false);
                coin.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = GemMeshes[gemMeshRandomer];
                
                coin = gem.transform.GetChild(2).transform.GetChild(0);
                coin.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = GemMeshes[gemMeshRandomer];
                Destroy(gem, 8);


            }
  
        }

        if (Time.time > timer + randomNumber)
        {

            timer = Time.time;
            yon = Random.Range(0, 2);
            if (yon == 0)
            {
                X = x1;
                obstaclePrefab = obstacleLeftPrefab;
            }

            else
            {
                X = x2;
                obstaclePrefab = obstacleRightPrefab;
            }


            randomScaleX = Random.Range(wallScaleRange1, wallScaleRange2);
            GameObject engel = Instantiate(obstaclePrefab, new Vector3(X, cam.transform.position.y + offSet, Z), Quaternion.identity);
            engel.transform.localScale = new Vector3(randomScaleX, engel.transform.localScale.y, engel.transform.localScale.z);

            Destroy(engel, 6);
            cleanTheArea(engel, 6);
            //engel.transform.GetComponentInChildren<BoxCollider>().enabled = false;

            int middleChange = Random.Range(0, 3);

            if (middleChange == 0)
            {
                int middleBlockAmount = Random.Range(1, 5);
                for (int i = 0; i < middleBlockAmount; i++)
                {
                    GameObject engel2 = Instantiate(middleObstaclePrefab, new Vector3(middleX, cam.transform.position.y + 2 + i, Z), Quaternion.identity);
                    Destroy(engel2, 6);

                    cleanTheArea(engel2, 8);

                }

            }

            
            randomNumber = Random.Range(timeRange1, timeRange2);


            
        }

    }

    void cleanTheArea(GameObject engel, int Layer)
    {
        raycastStartingPosition = new Vector3(engel.transform.position.x - 20f, engel.transform.position.y, engel.transform.position.z);
        if (Physics.Raycast(raycastStartingPosition, Vector3.right, out RaycastHit hit, 100, (1 << Layer)))
        {
            Destroy(hit.transform.gameObject); 
        }
    }
}
