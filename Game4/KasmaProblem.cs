using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class KasmaProblem : Photon.MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    float gecikme = 24;
    bool flip;
    SpriteRenderer SR;
    public Animator animator;
    public float _speed;
    public float _forcey;
    public bool srEN;
    public bool nckEN;
    public bool gunEN;
    public GameObject nck, gun;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
       
    }
    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, position, gecikme * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, gecikme * Time.deltaTime);
            SR.flipX = flip;
            animator.SetFloat("speed", _speed);
            animator.SetFloat("forcey", _forcey);
            SR.enabled = srEN;
            nck.SetActive(nckEN);
            gun.SetActive(gunEN);

        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(SR.flipX);
            stream.SendNext(animator.GetFloat("speed"));
            stream.SendNext(animator.GetFloat("forcey"));
            stream.SendNext(SR.enabled);
            stream.SendNext(nck.activeSelf);
            stream.SendNext(gun.activeSelf);
        }

        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            flip = (bool)stream.ReceiveNext();
            _speed = (float)stream.ReceiveNext();
            _forcey = (float)stream.ReceiveNext();
            srEN = (bool)stream.ReceiveNext();
            nckEN = (bool)stream.ReceiveNext();
            gunEN = (bool)stream.ReceiveNext();

        }

    }
}
