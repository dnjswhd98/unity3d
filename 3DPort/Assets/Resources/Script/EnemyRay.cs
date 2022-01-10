using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    private Collider[] InTargets;

    [SerializeField] private LayerMask TargetMask;
    [SerializeField] private LayerMask OnstacleMask;

    private float Radius;
    private float Angle;

    public bool FindTarget;

    void Start()
    {
        FindTarget = false;

        Radius = 25.0f;
        Angle = 95.0f;

        TargetMask = LayerMask.NameToLayer("Player");
        TargetMask = LayerMask.NameToLayer("GraundObj");
    }

    void Update()
    {
        InTargets = Physics.OverlapSphere(transform.position, Radius, TargetMask);

        if (InTargets != null)
        {
            for (int i = 0; i < InTargets.Length; ++i)
            {
                Transform Target = InTargets[i].transform;

                Vector3 TargetDirection = (Target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, TargetDirection) < Angle / 2)
                {
                    float TargetDistance = Vector3.Distance(transform.position, Target.position);

                    if (!Physics.Raycast(transform.position, TargetDirection, TargetDistance, OnstacleMask))
                        FindTarget = true;
                }
            }
        }
    }
}
