using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobEnemyCtal : MonoBehaviour
{
    public GameObject Player;

    private float MoveSpeed;

   [SerializeField] private bool FindTarget;
    private bool Melee;
    private bool Move;
    private bool Dead;
    public int Hp;

    private Animator Anime;

    void Start()
    {
        MoveSpeed = 4.0f;
        FindTarget = false;
        Melee = false;
        Move = false;
        Dead = false;
        Hp = 80;

        Anime = GetComponent<Animator>();
    }

    void Update()
    {
        if(this.GetComponent<EnemyRay>().FindTarget)
        {
            Player = GameObject.FindWithTag("Player");
            FindTarget = true;
        }

        if(FindTarget)
        {
            if (!Move)
                Move = true;

            transform.rotation = Quaternion.Euler(transform.position.x - Player.transform.position.x,
                transform.position.y, transform.rotation.z - Player.transform.position.z);
        }

        if(Move)
        {
            transform.Translate(Vector3.forward * MoveSpeed);
        }

        if(Dead)
        {
            GetComponent<EnemyRay>().enabled = false;
            FindTarget = false;
            Melee = false;
            Move = false;
            Dead = false;
        }

        Anime.SetBool("Dead", Dead);
    }
}
