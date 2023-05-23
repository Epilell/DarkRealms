using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider hpbar, hpbar2; // ü�¹�
    public float maxHp;
    public float currentHp;
    public Text textObj, textObj2; // ü�� �ؽ�Ʈ
    public Player player; // �÷��̾� ��ü

    private void Start()
    {
        // ü�¹� �ʱ�ȭ
        hpbar.value = (float)currentHp / (float)maxHp;
        hpbar2.value = (float)currentHp / (float)maxHp;
    }

    private void Update()
    {
        // maxHp = player.MaxHP;
        maxHp = 100f; // �ӽ÷� 100���� ����
        currentHp = player.CurrentHp;

        if (currentHp > maxHp) // ���� ü���� �ִ� ü���� ���� �ʰ�
        {
            currentHp = maxHp;
        }

        if (currentHp < 0) // ���� ü���� 0 �Ʒ��� �������� �ʰ�
        {
            currentHp = 0;
        }

        ChangeHP();
    }

    public void ChangeHP() // ü�¹� ����
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // ü�� �ؽ�Ʈ ����
        hpbar.value = (float)currentHp / (float)maxHp; // ü�� ����
        //hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // �� ���� �̻��ؼ� �ּ� ó��

        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString(); // ü�� �ؽ�Ʈ ����
        hpbar2.value = (float)currentHp / (float)maxHp; // ü�� ����
        //hpbar2.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // �� ���� �̻��ؼ� �ּ� ó��
    }
}