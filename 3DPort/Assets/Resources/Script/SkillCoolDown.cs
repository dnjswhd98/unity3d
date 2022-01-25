using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public float CoolTime1;
    public float CoolTime2;
    public float CoolTime3;

    public float maxCool1;
    public float maxCool2;
    public float maxCool3;

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

        CoolDown1.rectTransform.sizeDelta = new Vector2(CoolDown1.rectTransform.sizeDelta.x, (maxCool1 / CoolTime1) * 100);

        if(CoolTime1 > 0.0f)
        {
            CoolTime1 -= Time.deltaTime;
        }
    }
}
