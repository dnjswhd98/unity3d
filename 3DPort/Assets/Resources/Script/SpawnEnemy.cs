using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public bool Fly;
    private bool InList;

    private GameObject Player;
    private GameObject Enemy;
    private GameObject FEnemy;

    private void Awake()
    {
        Fly = false;
        Enemy = Resources.Load("Prefab/Lemurian") as GameObject;
        FEnemy = Resources.Load("Prefab/FlyingEnemy1") as GameObject;
    }

    private void OnEnable()
    {
        Player = GameObject.FindWithTag("Player");
        if(!Fly)
            transform.position = new Vector3(Random.Range(Player.transform.position.x - 500.0f, Player.transform.position.x + 500.0f), 
                Player.transform.position.y + 100.0f, Random.Range(Player.transform.position.z - 100.0f, Player.transform.position.z + 100.0f));
        else
            transform.position = new Vector3(Random.Range(Player.transform.position.x - 500.0f, Player.transform.position.x + 500.0f),
                Random.Range(Player.transform.position.y + 100.0f, Player.transform.position.y + 300.0f),
                Random.Range(Player.transform.position.z - 500.0f, Player.transform.position.z + 500.0f));
    }

    private void Update()
    {
        if (transform.position.y < 50)
        {
            transform.position = new Vector3(Random.Range(Player.transform.position.x - 500.0f, Player.transform.position.x + 500.0f),
                Player.transform.position.y + 100.0f, Random.Range(Player.transform.position.z - 100.0f, Player.transform.position.z + 100.0f));
        }

        if(Fly)
        {
            if (Singleton.GetInstance().GetDisableEnemyList.Count == 0)
            {
                FEnemy.transform.position = transform.position;
                Instantiate(FEnemy);
                Singleton.GetInstance().GetEnableList.Add(FEnemy);
                gameObject.SetActive(false);
            }
            else
            {
                FEnemy = Singleton.GetInstance().GetDisableEnemyList.Pop();
                FEnemy = Resources.Load("Prefab/FlyingEnemy1") as GameObject;
                FEnemy.transform.position = transform.position;
                Instantiate(FEnemy);
                Singleton.GetInstance().GetEnableList.Add(FEnemy);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Fly)
        {
            if (collision.transform.tag == "GraundObj")
            {
                if (Singleton.GetInstance().GetDisableEnemyList.Count == 0)
                {
                    Enemy.transform.position = transform.position;
                    Instantiate(Enemy);
                    Singleton.GetInstance().GetEnableList.Add(Enemy);
                    gameObject.SetActive(false);
                }
                else
                {
                    Enemy = Singleton.GetInstance().GetDisableEnemyList.Pop();
                    Enemy = Resources.Load("Prefab/RobEnemy") as GameObject;
                    Enemy.transform.position = transform.position;
                    Instantiate(Enemy);
                    Singleton.GetInstance().GetEnableList.Add(Enemy);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
