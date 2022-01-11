using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private float MoveSpeed;
    private float Fire;
    private float FireSpeed;
    private bool Falling;
    private bool Jump;
    private bool Dash;

    private Vector3 LastPosition;

    private Animator Anime;

    private Rigidbody Rigid;

    private GameObject Bullet;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Anime = GetComponent<Animator>();
        Bullet = Resources.Load("Prefab/Bullet") as GameObject;
        
    }

    void Start()
    {
        MoveSpeed = 4.0f;
        Falling = false;
        Fire = 0.0f;
        FireSpeed = 1.0f;
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

        if(Input.GetKey(KeyCode.LeftControl))
        {
            Dash = true;
        }

        if (Input.GetMouseButton(0))
        {
            Fire = 1.0f;
            StartCoroutine("ShootBullet");
        }
        if (Input.GetMouseButtonUp(0))
        {
            Fire = 0.0f;
            StopCoroutine("ShootBullet");
        }

        if (Dash)
        {
            MoveSpeed = 4.0f * 2.0f;
            if (Hor == 0 && Ver == 0)
                Dash = false;
        }
        else
            MoveSpeed = 4.0f;


        if (transform.position.y < 50)
        {
            //transform.position = GameObject.Find("PlayerSpawn").transform.position;

            transform.position = LastPosition;
        }

        Anime.SetFloat("Hor", Hor);
        Anime.SetFloat("Ver", Ver);
        Anime.SetFloat("Fire", Fire);
        Anime.SetBool("Jump", Jump);
        Anime.SetBool("Falling", Falling);

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

    IEnumerator ShootBullet()
    {
        yield return new WaitForSeconds(Time.deltaTime + FireSpeed);

        Bullet.transform.position = transform.position;
        Instantiate(Bullet);
    }
}
