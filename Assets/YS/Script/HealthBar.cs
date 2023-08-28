using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpbar, hpbar2; // ü�¹�
    public Text textObj, textObj2; // ü�� �ؽ�Ʈ
    public Player player; // �÷��̾� ��ü

    public float maxHp;
    public float currentHp;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        // ü�¹� �ʱ�ȭ
        hpbar.value = (float)player.CurrentHp / (float)player.MaxHP;
        hpbar2.value = (float)player.CurrentHp / (float)player.MaxHP;
    }

    private void Update()
    {
        if (player != null)
        {
            maxHp = player.MaxHP;
            currentHp = player.CurrentHp;

            // ���� ü���� �ִ� ü���� ���� �ʰ�
            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }

            // ���� ü���� 0 �Ʒ��� �������� �ʰ�
            if (currentHp < 0)
            {
                currentHp = 0;
            }

            ChangeHP();
        }
        else
        {
            Debug.LogError("Player ��ũ��Ʈ ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    public void ChangeHP() // ü�¹� ����
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // ü�� �ؽ�Ʈ ����
        hpbar.value = (float)currentHp / (float)maxHp; // ü�� ����
        // hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);

        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString();
        hpbar2.value = (float)currentHp / (float)maxHp;
    }
}