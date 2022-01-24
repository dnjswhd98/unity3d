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
    public bool hit;

    void Start()
    {
        RealHpBar = transform.Find("RealHp").GetComponent<Image>();
        MinusHpBar = transform.Find("MinusHp").GetComponent<Image>();
        Per = 100;
    }

    private void OnEnable()
    {
        Per = (TargetHp / TargetMaxHp) * 100;

        RealHpBar.rectTransform.sizeDelta = new Vector2(Per, RealHpBar.rectTransform.sizeDelta.y);
    }

    void Update()
    {
        Per = (TargetHp / TargetMaxHp) * 100;

        RealHpBar.rectTransform.sizeDelta = new Vector2(Per, RealHpBar.rectTransform.sizeDelta.y);

        if (MinusHpBar.rectTransform.sizeDelta.x != RealHpBar.rectTransform.sizeDelta.x)
            MinusHpBar.rectTransform.sizeDelta = 
                new Vector2(MinusHpBar.rectTransform.sizeDelta.x - 1.0f, MinusHpBar.rectTransform.sizeDelta.y);

        if (MinusHpBar.rectTransform.sizeDelta.x < RealHpBar.rectTransform.sizeDelta.x)
            new Vector2(RealHpBar.rectTransform.sizeDelta.x, MinusHpBar.rectTransform.sizeDelta.y);
    }

    private void LateUpdate()
    {
        if (MinusHpBar.rectTransform.sizeDelta.x == RealHpBar.rectTransform.sizeDelta.x)
            Invoke("SActive", 0.5f);
    }

    void SActive() 
    {
        if (MinusHpBar.rectTransform.sizeDelta.x == RealHpBar.rectTransform.sizeDelta.x)
            transform.parent.gameObject.SetActive(false); 
    }

}
