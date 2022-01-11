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

    void Start()
    {
        MoveSpeed = 4.0f;
        FindTarget = false;
        Melee = false;
        Move = false;
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

            //Vector3 EnemyAngle = new Vector3(transform.position.x - Player.transform.position.x,
            //    transform.position.y - Player.transform.position.y, transform.rotation.z).normalized;
            transform.rotation = Quaternion.Euler(transform.position.x - Player.transform.position.x,
                transform.position.y, transform.rotation.z - Player.transform.position.z);
        }
    }
}
