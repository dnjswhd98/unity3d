using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    private float Speed;

    void Start()
    {
        Speed = 8.0f;
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 2000.0f);
    }

    void Update()
    {
        if(transform.position.x > 3000 || transform.position.x < -1000 ||
            transform.position.y > 1000 || transform.position.y < -100 ||
            transform.position.z > 3000 || transform.position.z < -1000)
        {
            DisaBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "GraundObj")
        {
            DisaBullet();
        }

        if (other.transform.tag == "Enemy")
        {
            DisaBullet();
            other.gameObject.GetComponent<RobEnemyCtal>().Hp -= 10;
        }
    }

    private void DisaBullet()
    {
        Singleton.GetInstance().GetEnableList.Remove(gameObject);
        gameObject.SetActive(false);
        Singleton.GetInstance().GetDisableList.Push(gameObject);
    }
}
