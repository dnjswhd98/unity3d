using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float Power;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 2000.0f);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "GraundObj")
        {
            DisaBullet();
        }

        if (collision.transform.tag == "Enemy")
        {
            DisaBullet();
        }
    }

    private void DisaBullet()
    {
        Singleton.GetInstance().GetEnableList.Remove(gameObject);
        gameObject.SetActive(false);
        Singleton.GetInstance().GetDisableList.Push(gameObject);
    }
}
