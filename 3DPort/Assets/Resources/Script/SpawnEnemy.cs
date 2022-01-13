using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public bool Fly;

    private GameObject Player;
    private GameObject Enemy;
    private GameObject FEnemy;

    private void Awake()
    {
        Fly = false;
        Enemy = Resources.Load("Prefab/RobEnemy") as GameObject;
        FEnemy = Resources.Load("Prefab/FlyingEnemy1") as GameObject;
    }

    private void OnEnable()
    {
        Player = GameObject.FindWithTag("Player");
        if(!Fly)
            transform.position = new Vector3(Random.Range(100.0f, 1000.0f), 200.0f, Random.Range(100.0f, 1000.0f));
        else
            transform.position = new Vector3(Random.Range(Player.transform.position.x - 1000.0f, Player.transform.position.x + 1000.0f),
                Random.Range(Player.transform.position.y + 100.0f, Player.transform.position.y + 300.0f),
                Random.Range(Player.transform.position.z - 1000.0f, Player.transform.position.z + 1000.0f));
    }

    private void Update()
    {
        if (transform.position.y < 50)
        {
            transform.position = new Vector3(Random.Range(150.0f, 1000.0f), 200.0f, Random.Range(150.0f, 1000.0f));
        }

        if(Fly)
        {
            FEnemy.transform.position = transform.position;
            Instantiate(FEnemy);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Fly)
        {
            if (collision.transform.tag == "GraundObj")
            {
                Enemy.transform.position = transform.position;
                Instantiate(Enemy);
                gameObject.SetActive(false);
            }
        }
    }
}
