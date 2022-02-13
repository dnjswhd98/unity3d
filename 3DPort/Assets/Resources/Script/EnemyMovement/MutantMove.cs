using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMove : MonoBehaviour
{
    private float Hp;
    private float Power;

    [SerializeField]private bool PlayerInRange;
    [SerializeField]private bool Atk;
    [SerializeField]private bool Move;
    [SerializeField]private bool Dead;
    [SerializeField]private bool GiveEp;

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
        HpBar.SetActive(false);

        Atk = false;
        Move = false;
        GiveEp = false;
        Dead = false;

        Anime = GetComponent<Animator>();
    }

    void Update()
    {
        if (Hp <= 0)
        {
            Atk = false;
            Move = false;
            Dead = true;
        }

        if (!Move)
            Invoke("EnemySpawn", 4.15f);

        if (GetComponent<EnemyRay>().FindTarget)
            PlayerInRange = true;

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
                else
                {
                    if (!GetComponent<EnemyRay>().AttackRange)
                    {
                        transform.Translate(0.0f, 0.0f, Time.deltaTime * 3.5f);
                        Atk = false;
                    }
                    else
                        Atk = true;
                }
            }
        }
        else
        {
            if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dead"))  //0 = base layer
            {
                Debug.Log("AnimationNotPlaying");
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
            Anime.Play("Dead");
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
        Anime.SetBool("Atk", Atk);
        Anime.SetBool("Dead", Dead);
        //Anime.SetBool("FindPlayer", PlayerInRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            float Damage = collision.transform.GetComponent<BulletCtrl>().Power;

            Hp -= Damage;

            HpBar.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
            GameObject.Find("PlayerStat").GetComponent<PlayerStatUi>().PlayerHp -= Power;
    }

    private void EnemySpawn(){ Move = true; }
}
