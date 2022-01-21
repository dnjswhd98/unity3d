using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public float CoolTime1;
    public float CoolTime2;
    public float CoolTime3;

    private Image CoolDown1;
    private Image CoolDown2;
    private Image CoolDown3;

    void Start()
    {
        CoolTime1 = 0.0f;
        CoolTime2 = 0.0f;
        CoolTime3 = 0.0f;

        CoolDown1 = transform.Find("Rb/CoolTime1").GetComponent<Image>();
        CoolDown2 = transform.Find("Shift/CoolTime2").GetComponent<Image>();
        CoolDown3 = transform.Find("R/CoolTime3").GetComponent<Image>();
    }

    void Update()
    {
        if(CoolTime1 > 0.0f)
        {
            
        }
    }
}
