using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�����ÿ� ���� ȣ��
public class MobHPViewer : MonoBehaviour
{
    private MobHP mobHP;
    private Slider hpSlider;

    public void Setup(MobHP mobHP)
    {
        this.mobHP = mobHP;
        hpSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = mobHP.CurrentHP / mobHP.MaxHP;
    }
}
