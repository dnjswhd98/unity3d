using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyimgEnemyCtrl : MonoBehaviour
{
    [SerializeField]private bool PlayerInRange;

    private GameObject Player;
    private GameObject Muzzle;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Muzzle = transform.Find("Muzzle").gameObject;
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
            StartCoroutine("SReady");
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

    IEnumerator SReady()
    {
        yield return new WaitForSeconds(1.0f);

        if(!Muzzle.activeSelf)
        {
            Muzzle.SetActive(true);
        }
    }
}
