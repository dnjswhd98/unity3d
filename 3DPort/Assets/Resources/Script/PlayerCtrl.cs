using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private float MoveSpeed;
    private bool Falling;
    private bool Jump;
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
    }

    void Update()
    {
        var Hor = Input.GetAxis("Horizontal");
        var Ver = Input.GetAxis("Vertical");

        transform.Translate(Time.deltaTime * MoveSpeed * Hor, 0.0f, Time.deltaTime * MoveSpeed * Ver, Space.Self);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rigid.AddForce(Vector3.up * 500.0f);
            Jump = true;
        }

        Anime.SetFloat("Hor", Hor);
        Anime.SetFloat("Ver", Ver);
        Anime.SetBool("Jump", Jump);
        Anime.SetBool("Falling", Falling);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "GraundObj")
        {
            Falling = true;
            Jump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "GraundObj")
        {
            Falling = false;
        }
    }
}
