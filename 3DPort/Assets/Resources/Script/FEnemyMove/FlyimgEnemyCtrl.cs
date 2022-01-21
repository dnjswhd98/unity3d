using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyimgEnemyCtrl : MonoBehaviour
{
    [SerializeField]private float Hp;
    private float Power;

    [SerializeField]private bool PlayerInRange;
    [SerializeField]private bool Fire;
    [SerializeField]private bool Dead;
    private bool Explo;
    private bool GiveEp;

    private GameObject Player;
    private GameObject Muzzle;
    private GameObject HpBar;

    void Start()
    {
        Hp = 35 + (10.5f * Singleton.GetInstance().DifficultyLv);
        Power = 4 + (0.8f * Singleton.GetInstance().DifficultyLv);

        Player = GameObject.FindWithTag("Player");
        Muzzle = transform.Find("AtkRange/Muzzle").gameObject;
        HpBar = Instantiate(Resources.Load("Prefab/EnemyHpBar") as GameObject);
        HpBar.transform.Find("Bg").GetComponent<EnemyHpBar>().TargetMaxHp = Hp;
        HpBar.transform.parent = GameObject.Find("EnemyHpParent").transform;

        Fire = false;

        Dead = false;

        Explo = false;

        GiveEp = false;
    }

    void Update()
    {
        if (Hp <= 0)
            Dead = true;

        if (!Dead)
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
                StartCoroutine("AtR");
            }
        }

        HpBar.transform.Find("Bg").GetComponent<EnemyHpBar>().TargetHp = Hp;
    }

    private void LateUpdate()
    {
        HpBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
            transform.position.y + 2.0f, transform.position.z));

        if (Fire)
        {
            StartCoroutine("Atk");
        }
        else
        {
            StopCoroutine("Atk");
            transform.Find("AtkRange").GetComponent<FEnemyARay>().shotL = false;
            Muzzle.SetActive(false);

        }

        if(Dead)
        {
            if (!GiveEp)
            {
                GameObject.Find("PlayerStat").GetComponent<PlayerStatUi>().PlayerExp += 3;
                GiveEp = true;
            }

            GameObject temp = Resources.Load("JMO Assets/WarFX/_Effects/Explosions/WFX_ExplosiveSmoke Small") as GameObject;
            temp.transform.localScale = new Vector3(3, 3, 3);
            temp.transform.position = transform.position;
            if (!Explo)
            {
                Instantiate(temp);
                Explo = true;
            }

            GetComponent<Rigidbody>().useGravity = true;

            Singleton.GetInstance().GetEnableList.Remove(gameObject);
            gameObject.SetActive(false);
            Singleton.GetInstance().GetDisableEnemyList.Push(gameObject);

            HpBar.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            float Damage = collision.transform.GetComponent<BulletCtrl>().Power;

            //HpBar.transform.Find("Bg").GetComponent<EnemyHpBar>().Damaged = Damage;

            Hp -= Damage;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
            PlayerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
            PlayerInRange = false;
    }

    IEnumerator AtR()
    {
        yield return new WaitForSeconds(1.0f);

        Fire = true;
    }

    IEnumerator Atk()
    {
        yield return new WaitForSeconds(1.0f);

        if(!Muzzle.activeSelf)
        {
            transform.Find("AtkRange/Ray").gameObject.SetActive(false);
            Muzzle.SetActive(true);
        }

        transform.Find("AtkRange").GetComponent<FEnemyARay>().shotL = true;
        Fire = false;
    }
}
