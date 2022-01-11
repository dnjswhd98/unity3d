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

    private void OnEnable()
    {
        transform.position = new Vector3(Random.Range(100.0f, 1000.0f), 200.0f, Random.Range(100.0f, 1000.0f));
    }

    private void Update()
    {
        if (transform.position.y < 50)
        {
            transform.position = new Vector3(Random.Range(100.0f, 1000.0f), 200.0f, Random.Range(100.0f, 1000.0f));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "GraundObj")
        {
            Enemy.transform.position = transform.position;
            Instantiate(Enemy);
            gameObject.SetActive(false);
        }
    }
}
