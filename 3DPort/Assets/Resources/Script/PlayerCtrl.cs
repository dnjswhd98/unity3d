using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private float MoveSpeed;
    private float Fire;
    private bool Falling;
    private bool Jump;

    private Vector3 LastPosition;

    private Animator Anime;

    private Rigidbody Rigid;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Anime = GetComponent<Animator>();
    }

    void Start()
    {
        MoveSpeed = 4.0f;
        Falling = false;
        Fire = 0.0f;
    }

    void Update()
    {
        var Hor = Input.GetAxis("Horizontal");
        var Ver = Input.GetAxis("Vertical");

        transform.Translate(Time.deltaTime * MoveSpeed * Hor, 0.0f, Time.deltaTime * MoveSpeed * Ver, Space.Self);

        if(!Falling)
        {
            LastPosition = transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rigid.AddForce(Vector3.up * 500.0f);
            Jump = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Fire = 1.0f;
        }

        if(transform.position.y < 50)
        {
            //transform.position = GameObject.Find("PlayerSpawn").transform.position;

            transform.position = LastPosition;
        }

        Anime.SetFloat("Hor", Hor);
        Anime.SetFloat("Ver", Ver);
        Anime.SetFloat("Fire", Fire);
        Anime.SetBool("Jump", Jump);
        Anime.SetBool("Falling", Falling);

        Fire = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "GraundObj")
        {
            Falling = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "GraundObj")
        {
            Falling = true;
            Jump = false;
        }
    }
}
