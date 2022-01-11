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
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 2000.0f);
    }

    void Update()
    {
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * 2000.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            transform.gameObject.SetActive(false);

        }
    }
}
