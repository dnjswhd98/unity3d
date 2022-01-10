using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private GameObject Enemy;

    private void Awake()
    {
        Enemy = Resources.Load("Delivery Robot/Prefabs/Standard/Robot_Blue Variant") as GameObject;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "GraundObj")
        {
            Enemy.transform.position = transform.position;
            Instantiate(Enemy);
            Destroy(this);
        }
    }
}
