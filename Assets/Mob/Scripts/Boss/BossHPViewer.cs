using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    private BossHP bossHP;
    private Slider hpSlider;

    public void Setup(BossHP bossHP)
    {
        this.bossHP = bossHP;
        hpSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = bossHP.CurrentHP / bossHP.MaxHP;
    }
}
