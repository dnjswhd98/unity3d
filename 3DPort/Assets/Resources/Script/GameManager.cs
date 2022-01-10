using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Player;
    private GameObject EnemySpawnPoint;
    [SerializeField] private float EnemySpawnPercentage;

    private void Awake()
    {
        Player = Resources.Load("Prefab/Player") as GameObject;
        EnemySpawnPercentage = 0.25f;
    }

    void Start()
    {
        Player.transform.position = GameObject.Find("PlayerSpawn").transform.position;
        Instantiate(Player);
    }

    void Update()
    {
        if(Random.Range(0.0f, 100.0f) < EnemySpawnPercentage)
        {
            EnemySpawnPoint.AddComponent<BoxCollider>();
            EnemySpawnPoint.AddComponent<Rigidbody>();
            EnemySpawnPoint.AddComponent<SpawnEnemy>();

            EnemySpawnPoint.transform.position = new Vector3(Random.Range(100.0f, 1000.0f), 200.0f, Random.Range(100.0f, 1000.0f));
            Instantiate(EnemySpawnPoint = new GameObject("ESpawnPoint"));
        }

        if(EnemySpawnPoint.transform.position.y < 10)
        {
            EnemySpawnPoint.transform.position = new Vector3(Random.Range(100.0f, 1000.0f), 200.0f, Random.Range(100.0f, 1000.0f));
        }
    }
}
