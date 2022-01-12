using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Player;
    private GameObject EnemySpawnParent;
    private GameObject BulletParent;
    private GameObject[] EnemySpawnPoint = new GameObject[5];
    [SerializeField] private float EnemySpawnPercentage;

    private void Awake()
    {
        Player = Resources.Load("Prefab/Player") as GameObject;
        EnemySpawnPercentage = 0.25f;
        EnemySpawnParent = new GameObject("ESpawnParent");
        BulletParent = new GameObject("BulletParent");

        for (int i = 0; i < 5; ++i)
        {
            EnemySpawnPoint[i] = new GameObject("ESpawnPoint" + i);
            EnemySpawnPoint[i].transform.parent = EnemySpawnParent.transform;
            EnemySpawnPoint[i].AddComponent<BoxCollider>();
            EnemySpawnPoint[i].AddComponent<Rigidbody>();
            EnemySpawnPoint[i].AddComponent<SpawnEnemy>();
            EnemySpawnPoint[i].SetActive(false);
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
                if (!EnemySpawnPoint[i].activeSelf)
                {
                    EnemySpawnPoint[i].SetActive(true);
                }
            }
        }
    }
}
