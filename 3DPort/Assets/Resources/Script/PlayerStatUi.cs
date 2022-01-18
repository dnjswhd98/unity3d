using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUi : MonoBehaviour
{
    public int PlayerHp;
    public int PlayerMaxHp;
    public int PlayerExp;
    public int PlayerMaxExp;
    public int PlayerLv;
    public float PlayerPower;

    private GameObject HpBar;
    private GameObject ExpBar;

    private Text HpText;
    private Text LvText;

    private void Awake()
    {
        HpBar = transform.Find("HpBar/Fill Area/Fill").gameObject;
        ExpBar = transform.Find("ExpBar").gameObject;

        HpText = transform.Find("HpBar/Hp").GetComponent<Text>();
        LvText = transform.Find("PlayerLv").GetComponent<Text>();
    }
    void Start()
    {
        PlayerMaxHp = 110;
        PlayerHp = PlayerMaxHp;
        PlayerMaxExp = 30;
        PlayerExp = 0;
        PlayerLv = 1;
        PlayerPower = 12.0f;
    }

    void Update()
    {
        
        if (PlayerExp >= PlayerMaxExp)
        {
            ++PlayerLv;
            PlayerExp = 0;
            PlayerMaxExp += 20;

            PlayerMaxHp += 33;
            PlayerHp = PlayerMaxHp;

            PlayerPower += 2.4f;
        }


        if (PlayerHp <= 0)
            GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>().Dead = true;
    }

    private void LateUpdate()
    {
        HpText.text = PlayerHp + " / " + PlayerMaxHp;
        LvText.text = "Level : " + PlayerLv;
    }
}
