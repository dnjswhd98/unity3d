using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMove : MonoBehaviour
{
    private float Hp;
    private float Power;

    [SerializeField] private bool PlayerInRange;
    private bool Atk;
    private bool Move;
    private bool Dead;
    private bool GiveEp;

    private Animator Anime;

    private GameObject Player;
    private GameObject HpBar;

    void Start()
    {
        Hp = 80 + (24.0f * Singleton.GetInstance().DifficultyLv);
        Power = 12 + (2.4f * Singleton.GetInstance().DifficultyLv);

        Player = GameObject.FindWithTag("Player");

        HpBar = Instantiate(Resources.Load("Prefab/EnemyHpBar") as GameObject);
        HpBar.transform.Find("Bg").GetComponent<EnemyHpBar>().TargetMaxHp = Hp;
        HpBar.transform.parent = GameObject.Find("EnemyHpParent").transform;

        Atk = false;
        Move = false;
        GiveEp = false;

        Anime = GetComponent<Animator>();
    }

    void Update()
    {
        if (Hp <= 0)
            Dead = true;

        if (!Move)
            Invoke("EnemySpawn", 4.15f);

        if (!Dead)
        {
            if (Move)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation((Player.transform.position - transform.position).normalized),
                    Time.deltaTime * 1.5f);
                if (!PlayerInRange)
                {
                    transform.Translate(0.0f, 0.0f, Time.deltaTime * 1.0f);
                }
            }
        }

        HpBar.transform.Find("Bg").GetComponent<EnemyHpBar>().TargetHp = Hp;
    }

    private void LateUpdate()
    {
        HpBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
            transform.position.y + 2.0f, transform.position.z));

        if (Dead)
        {
            if (!GiveEp)
            {
                GameObject.Find("PlayerStat").GetComponent<PlayerStatUi>().PlayerExp += 5;
                GiveEp = true;
            }

            //if (Singleton.GetInstance().GetEnableList.Count > 200)
            //{
            //    Singleton.GetInstance().GetEnableList.Remove(gameObject);
            //    gameObject.SetActive(false);
            //    Singleton.GetInstance().GetDisableEnemyList.Push(gameObject);
            //}

            HpBar.SetActive(false);
        }

        Anime.SetBool("Move", Move);
        Anime.SetBool("Attack", Atk);
        Anime.SetBool("Dead", Dead);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            float Damage = collision.transform.GetComponent<BulletCtrl>().Power;

            Hp -= Damage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
            GameObject.Find("PlayerStat").GetComponent<PlayerStatUi>().PlayerHp -= Power;
    }

    private void EnemySpawn(){ Move = true; }
}
