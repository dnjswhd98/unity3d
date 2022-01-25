using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEnemyARay : MonoBehaviour
{
    [SerializeField]public bool shotL;
    public bool shotRL;

    [SerializeField]private RaycastHit THit;

    private GameObject RayLine;
    private GameObject Target;

    private LineRenderer RayLineRander;

    void Start()
    {
        shotL = false;
        shotRL = false;

        Target = GameObject.FindWithTag("Player");
        RayLine = transform.Find("Ray").gameObject;
        RayLineRander = RayLine.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(shotRL)
        {
            if (!RayLine.activeSelf)
                RayLine.SetActive(true);
        }

        if(Physics.Raycast(transform.position, transform.forward, out THit))
        {
            float TargetDistance = Vector3.Distance(transform.position, THit.transform.position);

            if (TargetDistance <= 20.0f)
            {
                shotRL = true;
            }

            if(shotRL)
                RayLine.transform.localScale = new Vector3(1, 1, TargetDistance);

            if (shotL)
            {
                if(THit.transform.tag == "Player")
                {
                    GameObject.Find("PlayerStat").GetComponent<PlayerStatUi>().PlayerHp -= 0.5f;
                }

                shotRL = false;
                shotL = false;
            }

            Debug.DrawLine(transform.position, THit.point, Color.red);
        }
    }
}
