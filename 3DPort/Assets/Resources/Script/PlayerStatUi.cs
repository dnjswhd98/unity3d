using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUi : MonoBehaviour
{
    public float PlayerHp;
    public float PlayerMaxHp;
    public float PlayerPower;
    private float Per;
    [SerializeField]private float EPer;

    public int PlayerExp;
    public int PlayerMaxExp;
    public int PlayerLv;

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
        Per = 1.0f;
        EPer = 0.0f;
        PlayerMaxHp = 110.0f;
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
            PlayerExp -= PlayerMaxExp;
            PlayerMaxExp += 20;

            PlayerMaxHp += 33.0f;
            PlayerHp = PlayerMaxHp;

            PlayerPower += 2.4f;
        }


        if (PlayerHp <= 0.0f)
            GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>().Dead = true;
    }

    private void LateUpdate()
    {
        HpText.text = Mathf.Round(PlayerHp) + " / " + Mathf.Round(PlayerMaxHp);
        LvText.text = "Level : " + PlayerLv;

        Per = (PlayerHp / PlayerMaxHp);
        EPer = (PlayerExp / PlayerMaxExp);

        HpBar.transform.localScale = new Vector3(Per, HpBar.transform.localScale.y, HpBar.transform.localScale.z);
        ExpBar.transform.localScale = new Vector3(EPer, ExpBar.transform.localScale.y, ExpBar.transform.localScale.z);
    }
}
