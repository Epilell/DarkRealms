using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider hpbar, hpbar2; // 체력바
    public float maxHp;
    public float currentHp;
    public Text textObj, textObj2; // 체력 텍스트
    public Player player; // 플레이어 객체

    private void Start()
    {
        // 체력바 초기화
        hpbar.value = (float)currentHp / (float)maxHp;
        hpbar2.value = (float)currentHp / (float)maxHp;
    }

    private void Update()
    {
        maxHp = player.MaxHP;
        currentHp = player.CurrentHp;

        if (currentHp > maxHp) // 현재 체력이 최대 체력을 넘지 않게
        {
            currentHp = maxHp;
        }

        if (currentHp < 0) // 현재 체력이 0 아래로 내려가지 않게
        {
            currentHp = 0;
        }

        ChangeHP();
    }

    public void ChangeHP() // 체력바 갱신
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // 체력 갱신

        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar2.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // 체력 갱신
    }
}