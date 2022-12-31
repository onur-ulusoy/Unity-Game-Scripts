using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class motionController : MonoBehaviour
{
    int loc = 0;
    public Rigidbody rg;
    public float game_velocity;
    public float jump_velocity;
    public Camera cam;
    int ray_yonu = -1;
    //public Transform parent;

    Vector3 game_velocity_vector;
    Vector3 jump_velocity_vector;

    Vector3 raycastStartingPosition;

    int total_score;

    public TextMeshProUGUI tmp;

    Vector3 startingTransformPosition;
    Vector3 startingCameraPosition;

    public gameControl gamecontrol;
    public wallControl wallcontrol;
    public GameObject buton;

    int midObsScore = 5;
    int baseObsScore = 2;

    public GameObject speedBonusBuffer;
    buffTimerAnimation buffTimerScript;
    public GameObject thunder;
    public ParticleSystem thunderPS;
    public Transform thPos;
    public bool isEnergetic = false;

    public Transform startingPortal;
    public GameObject wall0;
    public bool inPortal = false;
    void Start()
    {
        //Time.timeScale = 0.01f;
        game_velocity_vector = new Vector3(0f, game_velocity,0f);
        jump_velocity_vector = new Vector3(jump_velocity, 0f,0f);
        
        total_score = 0;
        startingTransformPosition = transform.position;
        startingCameraPosition = cam.transform.position;
        buffTimerScript = speedBonusBuffer.GetComponent<buffTimerAnimation>();
        thunderPS = thunder.GetComponent<ParticleSystem>();

        //rg.velocity = jump_velocity_vector;
    }

    void Update()
    {
        //print(Time.time - time9);
        //parent.Translate(game_velocity_vector * Time.deltaTime);
        if (!inPortal)
        {
            float difference = cam.transform.position.y - transform.position.y;
            if (!(difference <= 0.6905 && difference >= 0.690))
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, transform.position.y + 0.69f, cam.transform.position.z), 0.1f);
            
            cam.transform.Translate(game_velocity_vector * Time.deltaTime);
            transform.Translate(game_velocity_vector * Time.deltaTime);
            //print(cam.transform.position.y - transform.position.y);
        }

        else
        {
            cam.transform.Translate(new Vector3(0f, 1.69f, 0f) / 2f * Time.deltaTime);
        }
            
        //transform.Rotate(new Vector3(0, 0, 150f) * Time.deltaTime);
        //transform.position = parent.position;

        if (Input.GetKey(KeyCode.Space) && rg.velocity == Vector3.zero)
        {
            if (loc == 0)
            {
                loc = 1;
                rg.velocity = jump_velocity_vector;

            }

            else
            {
                loc = 0;
                rg.velocity = jump_velocity_vector * -1;
            }

        }

        obstacleDetect();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEnergetic)
        {
            thunder.transform.position = thPos.position;
            if (thunderPS.isStopped)
                thunderPS.randomSeed = (uint) Random.Range(-1302325944, 1302325944);

            thunderPS.Play();
        }
       

        if (collision.transform.CompareTag("wall"))
        {
            rg.velocity = Vector3.zero;

            if (loc == 0)
                ray_yonu = 1;
            else
                ray_yonu = -1;
        }

        else if (collision.transform.CompareTag("middleObs") || collision.transform.CompareTag("baseObs"))
        {
            
            rg.velocity = Vector3.zero;
            //Time.timeScale = 0f;
            buton.SetActive(true);
            
            enabled = false;

            buffTimerScript.enabled = false;
            speedBonusBuffer.SetActive(false);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("starGem"))
        {
            other.GetComponent<BoxCollider>().enabled = false;
            Transform destroyEffect = other.transform.GetChild(5);
            int length = other.transform.childCount;
            for (int i = 0; i < length; i++)
            {
                if (i == 5)
                    break;
                other.transform.GetChild(i).gameObject.SetActive(false);
            }
            destroyEffect.gameObject.SetActive(true);
            destroyEffect.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(other.transform.gameObject, 2);
            StartCoroutine(SpeedBonus());

        }

        else if (other.transform.CompareTag("Gem"))
        {
            other.GetComponent<BoxCollider>().enabled = false;
            Transform destroyEffect = other.transform.GetChild(2);
            int length = other.transform.childCount;
            for (int i = 0; i < length; i++)
            {
                if (i == 2)
                    break;
                other.transform.GetChild(i).gameObject.SetActive(false);
            }
            destroyEffect.gameObject.SetActive(true);
            destroyEffect.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(other.transform.gameObject, 2);
        }

        else if (other.transform.CompareTag("Portal") && !inPortal)
        {
            rg.velocity = Vector3.zero;
            var Vf = other.transform.GetComponent<VortexForce>();
            Vf.enabled = true;
            //enabled = false;
            inPortal = true;
            //print(Time.time);
        }



    }

    public void PlayAgain()
    {
        
        foreach (var item in GameObject.FindGameObjectsWithTag("middleObs"))
        {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("baseObs"))
        {
            Destroy(item);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("starGem"))
        {
            Destroy(item);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("Gem"))
        {
            Destroy(item);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("heartGem"))
        {
            Destroy(item);
        }

        //Time.timeScale = 1f;
        total_score = 0;
        transform.position = startingTransformPosition;
        cam.transform.position = startingCameraPosition;
        tmp.text = total_score.ToString();

        wallcontrol.buffTimer = Time.time;
        wallcontrol.buffRandomer = Random.Range(20, 35);

        gamecontrol.enabled = true;
        wallcontrol.enabled = false;
        buton.SetActive(false);
        isEnergetic = false;
        returnDefault();
    }

    void obstacleDetect()
    {
        raycastStartingPosition = new Vector3(transform.position.x - 20f, transform.position.y, transform.position.z);
        //int layerMask = 1 << 0;
        //layerMask = ~layerMask;
        Debug.DrawRay(transform.position, jump_velocity_vector * ray_yonu * 100, Color.green);
        if (Physics.Raycast(transform.position, jump_velocity_vector * ray_yonu, out RaycastHit hit, 100, 1<<6))
        {
            
            hit.transform.gameObject.layer = 0;

            ray_yonu *= -1;
            StartCoroutine(Score(midObsScore));

        }

        if (Physics.Raycast(raycastStartingPosition, Vector3.right, out hit, 100, 1<<8))
        {

            hit.transform.gameObject.layer = 0;
            StartCoroutine(Score(baseObsScore));
            
        }
    }

    private IEnumerator Score(int point)
    {
        for (float i = 1f; i <= 1.2f; i+=0.05f)
        {
            tmp.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        tmp.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        total_score += point;
        tmp.text = total_score.ToString();

        for (float i = 1.2f; i >= 1f; i -= 0.05f)
        {
            tmp.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        tmp.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    private IEnumerator SpeedBonus()
    {
        if (!thunder.activeSelf)
            thunder.SetActive(true);

        isEnergetic = true;
        jump_velocity_vector = new Vector3(jump_velocity + 2, 0f, 0f);
        baseObsScore *= 2;
        midObsScore *= 2;
        speedBonusBuffer.SetActive(true);
        speedBonusBuffer.GetComponent<buffTimerAnimation>().enabled = true;
        yield return new WaitForSeconds(10);
        

        jump_velocity_vector = new Vector3(jump_velocity, 0f, 0f);
        baseObsScore /= 2;
        midObsScore /= 2;
        yield return new WaitForSeconds(0.5f);
        speedBonusBuffer.SetActive(false);
        
        buffTimerScript.enabled = false;
        isEnergetic = false;
    }

    private void returnDefault()
    {
        StopAllCoroutines();
        
        jump_velocity_vector = new Vector3(jump_velocity, 0f, 0f);
        baseObsScore = 2;
        midObsScore = 5;
    }
    private void increaseGameSpeed()
    {
        game_velocity_vector = new Vector3(0f, game_velocity*2, 0f);
    }

}
