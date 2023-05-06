using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider hpbar, hpbar2; // 체력바
    public float maxHp = 100;
    public float currentHp;
    public Text textObj, textObj2; // 체력 텍스트
    public P_Data playerData;
    private void Start()
    {
        // 체력바 초기화
        hpbar.value = (float)currentHp / (float)maxHp;
        hpbar2.value = (float)currentHp / (float)maxHp;
    }

    private void Update()
    {

        currentHp = playerData.P_CurrentHp;
        ChangeHP();
    }

    public void ChangeHP()
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar.value = (float)currentHp / (float)maxHp; // 체력 갱신
        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar2.value = (float)currentHp / (float)maxHp; // 체력 갱신
        // hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);
    }
    
    public void IncreaseHp(float amount) // 체력 증가 함수
    {
        currentHp += amount;
        if (currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
        hpbar.value = currentHp;
        hpbar2.value = currentHp;
    }

    public void DecreaseHp(float amount) // 체력 감소 함수
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            currentHp = 0;
        }
        hpbar.value = currentHp;
        hpbar2.value = currentHp;
    }
    
}