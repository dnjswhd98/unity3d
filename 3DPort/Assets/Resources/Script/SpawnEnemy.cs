using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private GameObject Enemy;

    private void Awake()
    {
        Enemy = Resources.Load("Prefab/RobEnemy") as GameObject;
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
            Enemy.AddComponent<EnemyRay>();
            Instantiate(Enemy);
            this.gameObject.SetActive(false);
            //Destroy(this);
        }
    }
}
