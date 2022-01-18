using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Player;
    private GameObject EnemySpawnParent;
    private GameObject BulletParent;
    private GameObject[] EnemySpawnPoint = new GameObject[5];
    private GameObject[] FlyingEnemySpawnPoint = new GameObject[5];
    [SerializeField] private float EnemySpawnPercentage;
    private float EnemyFlying;

    public int DifficultyLv;

    private void Awake()
    {
        Player = Resources.Load("Prefab/Player") as GameObject;
        EnemySpawnPercentage = 0.25f;
        EnemyFlying = 1.0f;
        EnemySpawnParent = new GameObject("ESpawnParent");
        EnemySpawnParent.transform.parent = GameObject.Find("Spawns").transform;
        BulletParent = new GameObject("BulletParent");
        DifficultyLv = 0;

        for (int i = 0; i < 5; ++i)
        {
            //EnemySpawnPoint[i] = new GameObject("ESpawnPoint" + i);
            //EnemySpawnPoint[i].transform.parent = EnemySpawnParent.transform;
            //EnemySpawnPoint[i].AddComponent<BoxCollider>();
            //EnemySpawnPoint[i].AddComponent<Rigidbody>();
            //EnemySpawnPoint[i].AddComponent<SpawnEnemy>();
            //EnemySpawnPoint[i].SetActive(false);

            FlyingEnemySpawnPoint[i] = new GameObject("FESpawnPoint" + i);
            FlyingEnemySpawnPoint[i].transform.parent = EnemySpawnParent.transform;
            FlyingEnemySpawnPoint[i].AddComponent<SpawnEnemy>();
            FlyingEnemySpawnPoint[i].SetActive(false);
        }
    }

    void Start()
    {
        Player.transform.position = GameObject.Find("PlayerSpawn").transform.position;
        Instantiate(Player);
    }

    void Update()
    {
        for (int i = 0; i < 5; ++i)
        {
            if (Random.Range(0.0f, 100.0f) < EnemySpawnPercentage)
            {
                if (Random.Range(0.0f, 2.0f) < EnemyFlying)
                {
                    if (!FlyingEnemySpawnPoint[i].activeSelf)
                    {
                        FlyingEnemySpawnPoint[i].SetActive(true);
                        //EnemySpawnPoint[i].GetComponent<SpawnEnemy>().Fly = true;
                        FlyingEnemySpawnPoint[i].GetComponent<SpawnEnemy>().Fly = true;
                    }
                }
                else
                {
                    //if (!EnemySpawnPoint[i].activeSelf)
                    //{
                    //    EnemySpawnPoint[i].SetActive(true);
                    //    EnemySpawnPoint[i].GetComponent<SpawnEnemy>().Fly = false;
                    //}
                }
            }
        }

    }

    private void LateUpdate()
    {
        Singleton.GetInstance().DifficultyLv = DifficultyLv;
    }
}
