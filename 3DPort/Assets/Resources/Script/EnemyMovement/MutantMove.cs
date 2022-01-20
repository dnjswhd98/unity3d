using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMove : MonoBehaviour
{
    private float Hp;
    private float Power;

    private bool Atk;

    private Animator Anime;

    private GameObject HpBar;

    void Start()
    {
        Hp = 80 + (24.0f * Singleton.GetInstance().DifficultyLv);
        Power = 12 + (2.4f * Singleton.GetInstance().DifficultyLv);

        HpBar = Instantiate(Resources.Load("Prefab/EnemyHpBar") as GameObject);
        HpBar.transform.Find("Bg").GetComponent<EnemyHpBar>().TargetMaxHp = Hp;
        HpBar.transform.parent = GameObject.Find("EnemyHpParent").transform;

        Atk = false;

        Anime = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
