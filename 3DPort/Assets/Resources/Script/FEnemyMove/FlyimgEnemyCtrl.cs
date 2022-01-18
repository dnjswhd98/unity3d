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

    private GameObject Player;
    private GameObject Muzzle;


    void Start()
    {
        Hp = 35 + (10.5f * Singleton.GetInstance().DifficultyLv);
        Power = 4 + (0.8f * Singleton.GetInstance().DifficultyLv);

        Player = GameObject.FindWithTag("Player");
        Muzzle = transform.Find("AtkRange/Muzzle").gameObject;
        Fire = false;

        Dead = false;
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
        
    }

    private void LateUpdate()
    {
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
            GameObject temp = Resources.Load("JMO Asset/WarFX/_Effects/Explosions/WFX_ExplosiveSmoke Small") as GameObject;
            temp.transform.position = transform.position;
            Instantiate(temp);

            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            Hp -= collision.transform.GetComponent<BulletCtrl>().Power;
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
