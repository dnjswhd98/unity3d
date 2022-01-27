using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public float[] CoolTime;

    public float[] maxCool;

    [SerializeField]private Image[] CoolDown = new Image[3];

    [SerializeField] private Text[] Txt = new Text[3];

    public bool[] CoolAct;

    void Start()
    {
        CoolTime = new float[] { 0, 0, 0 };
        maxCool = new float[] { 0, 0, 0 };
        CoolAct = new bool[] { false, false, false };

        CoolDown[0] = transform.Find("Rb/CoolTime").GetComponent<Image>();
        CoolDown[1] = transform.Find("Shift/CoolTime").GetComponent<Image>();
        CoolDown[2] = transform.Find("R/CoolTime").GetComponent<Image>();

        Txt[0] = transform.Find("Rb/CoolText").GetComponent<Text>();
        Txt[1] = transform.Find("Shift/CoolText").GetComponent<Text>();
        Txt[2] = transform.Find("R/CoolText").GetComponent<Text>();
    }

    void Update()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (CoolAct[i])
            {
                maxCool[i] = CoolTime[i];
                CoolAct[i] = false;
            }

            if (CoolTime[i] > 0.0f)
            {
                CoolTime[i] -= Time.deltaTime;
            }
            else
                CoolTime[i] = 0.0f;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (CoolTime[i] > 0.0f)
                CoolDown[i].rectTransform.sizeDelta = new Vector2(CoolDown[i].rectTransform.sizeDelta.x, (CoolTime[i] / maxCool[i]) * 100);

            if (CoolTime[i] > 0.0f)
                Txt[i].text = "" + Mathf.Ceil(CoolTime[i]);
            else
                Txt[i].text = "";
        }
    }
}
