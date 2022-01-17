using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]private float MoveSpeed;
    private float Fire;
    private float FireSpeed;
    private float AtkDelay;

    private bool Falling;
    private bool Jump;
    private bool Dash;
    public bool Dead;

    private Vector3 LastPosition;
    public Vector3 TargetPos;

    private Animator Anime;

    private Rigidbody Rigid;

    private GameObject Bullet;

    private PlayerStatUi StatUi;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Anime = GetComponent<Animator>();
        
    }

    void Start()
    {
        Bullet = Resources.Load("Prefab/Bullet") as GameObject;
        MoveSpeed = 4.0f;
        Falling = false;
        Fire = 0.0f;
        FireSpeed = 0.1f;
        AtkDelay = 0.2f;

        StatUi = GameObject.Find("PlayerStat").GetComponent<PlayerStatUi>();
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

        if(Input.GetKeyDown(KeyCode.Space) && !Jump)
        {
            Rigid.AddForce(Vector3.up * 500.0f);
            Jump = true;
        }

        if(Input.GetKey(KeyCode.LeftControl))
        {
            Dash = true;
        }

        if (Input.GetMouseButtonDown(0) && Fire == 0.0f)
        {
            Fire = 1.0f;
            AtkDelay = 0.2f;

            ShotBullet();

            StartCoroutine("ShootBullet");
        }

        if(Input.GetKeyDown(KeyCode.R) && Fire == 0)
        {
            Fire = 3.0f;
            AtkDelay = 0.6f;

            StartCoroutine("ShootBullet");
        }

        if(Fire == 3.0f)
        {
            ShotBullet();
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
        yield return new WaitForSeconds(FireSpeed);

        ShotBullet();

        StartCoroutine("FireDelay");
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(AtkDelay);

        Fire = 0.0f;

    }

    private void ShotBullet()
    {
        if (Singleton.GetInstance().GetDisableList.Count == 0)
        {
            Singleton.GetInstance().GetDisableList.Push(gameObject);
            for (int i = 0; i < 10; ++i)
            {
                GameObject Obj = Instantiate(Bullet);
                Obj.transform.parent = GameObject.Find("BulletParent").transform;
                Obj.SetActive(false);
                Singleton.GetInstance().GetDisableList.Push(Obj);
            }
        }

        GameObject BObj = Singleton.GetInstance().GetDisableList.Pop();

        BObj.transform.position = transform.Find("Hips/ArmPosition_Right/Muzzle").position;

        //BObj.transform.LookAt(TargetPos);
        BObj.transform.rotation = GameObject.Find("CameraObj").transform.rotation;

        BObj.SetActive(true);

        Singleton.GetInstance().GetEnableList.Add(BObj);
    }
}
