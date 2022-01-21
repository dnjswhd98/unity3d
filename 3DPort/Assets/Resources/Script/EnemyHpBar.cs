using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    private Image RealHpBar;
    private Image MinusHpBar;

    [SerializeField]private float Per;

    public float TargetHp;
    public float TargetMaxHp;

    void Start()
    {
        RealHpBar = transform.Find("RealHp").GetComponent<Image>();
        MinusHpBar = transform.Find("MinusHp").GetComponent<Image>();
        Per = 100;
    }

    void Update()
    {
        Per = (TargetHp / TargetMaxHp) * 100;

        RealHpBar.rectTransform.sizeDelta = new Vector2(Per, RealHpBar.rectTransform.sizeDelta.y);

        if (MinusHpBar.rectTransform.sizeDelta.x != RealHpBar.rectTransform.sizeDelta.x)
            MinusHpBar.rectTransform.sizeDelta = 
                new Vector2(MinusHpBar.rectTransform.sizeDelta.x - 1.0f, MinusHpBar.rectTransform.sizeDelta.y);
    }
}
