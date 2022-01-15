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

    private GameObject HpBar;
    private GameObject ExpBar;

    private Text HpText;
    private Text LvText;

    void Start()
    {
        PlayerHp = 0;
        PlayerMaxHp = 0;
        PlayerExp = 0;
        PlayerMaxExp = 0;
        PlayerLv = 0;

        HpBar = transform.Find("Hpbar/Fill Area/Fill").gameObject;
        ExpBar = transform.Find("ExpBar").gameObject;

        HpText = transform.Find("Hpbar/Hp").GetComponent<Text>();
        LvText = transform.Find("PlayerLv").GetComponent<Text>();
    }

    void Update()
    {


        HpText.text = PlayerMaxHp + " / " + PlayerHp;
        LvText.text = "Level : " + PlayerLv;
    }
}
