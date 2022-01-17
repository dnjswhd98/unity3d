using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyimgEnemyCtrl : MonoBehaviour
{
    [SerializeField]private bool PlayerInRange;
    private bool Fire;

    private GameObject Player;
    private GameObject Muzzle;


    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Muzzle = transform.Find("AtkRange/Muzzle").gameObject;
        Fire = false;
    }

    void Update()
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
            //if (!transform.Find("AtkRange/Ray").gameObject.activeSelf)
            //    transform.Find("AtkRange/Ray").gameObject.SetActive(true);
            Fire = true;
        }

        if(Fire)
        {
            StartCoroutine("Atk");
        }
        else
        {
            //StopCoroutine("Atk");
            Muzzle.SetActive(false);
            transform.Find("AtkRange").GetComponent<FEnemyARay>().shotL = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.transform.tag == "Player")
        //    PlayerInRange = true;
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

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.tag == "Player")
            GameObject.Find("PlayerStat").GetComponent<PlayerStatUi>().PlayerHp -= 10;
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
