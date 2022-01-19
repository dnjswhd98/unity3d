using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    private Image RealHpBar;
    private Image MinusHpBar;

    public GameObject Target;

    private int Per;

    public float TargetHp;
    public float TargetMaxHp;
    public float Damaged;

    void Start()
    {
        RealHpBar = transform.Find("RealHp").GetComponent<Image>();
        MinusHpBar = transform.Find("MinusHp").GetComponent<Image>();
        Per = 100;
    }

    void Update()
    {
        if(Damaged > 0)
        {
            float temp = TargetMaxHp / 100;
            int tempi;
            for (tempi = 0; (temp * tempi) > (Damaged - temp); ++tempi) ;

            Per -= tempi;

            RealHpBar.rectTransform.sizeDelta = new Vector2(Per, RealHpBar.rectTransform.sizeDelta.y);

            Damaged = 0;
        }
        /*
         
            float ImgSx = HpBar.transform.Find("Bg/RealHp").transform.localScale.x / 100.0f;
            float Hpx = Hp / 100;
            int tempi;

            for (tempi = 0; Hpx * tempi >= Damage - Hpx; ++tempi) ;

            HpBar.transform.Find("Bg/RealHp").transform.localScale
                = new Vector3(HpBar.transform.Find("Bg/RealHp").transform.localScale.x - (ImgSx * tempi),
                HpBar.transform.Find("Bg/RealHp").transform.localScale.y,
                HpBar.transform.Find("Bg/RealHp").transform.localScale.z);
         */
    }
}
