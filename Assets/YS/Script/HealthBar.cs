using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider hpbar, hpbar2; // ü�¹�
    public float maxHp = 100;
    public float currentHp = 50;
    public Text textObj, textObj2; // ü�� �ؽ�Ʈ

    private void Start()
    {
        // ü�¹� �ʱ�ȭ
        hpbar.value = (float)currentHp / (float)maxHp;
        hpbar2.value = (float)currentHp / (float)maxHp;
    }

    private void Update()
    {
        // �׽�Ʈ��: �����̽��� ������ ü�� 10�� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentHp > 0)
            {
                currentHp -= 10;
            }
            else
            {
                currentHp = 0;
            }
        }
        ChangeHP();
    }

    public void ChangeHP()
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // ü�� �ؽ�Ʈ ����
        hpbar.value = (float)currentHp / (float)maxHp; // ü�� ����
        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString(); // ü�� �ؽ�Ʈ ����
        hpbar2.value = (float)currentHp / (float)maxHp; // ü�� ����
        // hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);
    }

    public void IncreaseHp(float amount) // ü�� ���� �Լ�
    {
        currentHp += amount;
        if (currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
        hpbar.value = currentHp;
        hpbar2.value = currentHp;
    }

    public void DecreaseHp(float amount) // ü�� ���� �Լ�
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