using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public float[] CoolTime;

    public float[] maxCool;

    private Image[] CoolDown = new Image[3];

    public bool[] CoolAct;

    void Start()
    {
        CoolTime = new float[] { 0, 0, 0 };
        maxCool = new float[] { 0, 0, 0 };
        CoolAct = new bool[] { false, false, false };

        CoolDown[0] = transform.Find("Rb/CoolTime1").GetComponent<Image>();
        CoolDown[1] = transform.Find("Shift/CoolTime2").GetComponent<Image>();
        CoolDown[2] = transform.Find("R/CoolTime3").GetComponent<Image>();

    }

    void Update()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (CoolAct[i])
                maxCool[i] = CoolTime[i];

            CoolDown[i].rectTransform.sizeDelta = new Vector2(CoolDown[i].rectTransform.sizeDelta.x, (maxCool[i] / CoolTime[i]) * 100);

            if (CoolTime[i] > 0.0f)
            {
                CoolTime[i] -= Time.deltaTime;
            }
        }
    }
}
