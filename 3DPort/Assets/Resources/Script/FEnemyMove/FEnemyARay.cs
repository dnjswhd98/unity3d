using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEnemyARay : MonoBehaviour
{
    public bool shotL;
    public bool shotRL;

    private RaycastHit THit;

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
            if(shotL)
            {
                if(THit.transform.tag == "Player")
                {
                    Debug.Log("Hit!");
                }
            }
        }
    }
}
