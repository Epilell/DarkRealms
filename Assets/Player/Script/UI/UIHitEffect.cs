using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHitEffect : MonoBehaviour
{
    //Field
    #region
    public static UIHitEffect Instance;

    [SerializeField]
    private Color BaseColor = new Color(255 / 255f, 0 / 255f, 0 / 255f, 0 / 255f),
                  AlignColor = new Color(255 / 255f, 0 / 255f, 0 / 255f, 80 / 255f);
    private Image Img;
    private float MaxHP;
    private bool isTakingDamage = false; // �߰��� ����
    private float hitEffectDuration = 0.2f; // �ǰ� ����Ʈ ���ӽð�
    private float timer = 0f; // Ÿ�̸�
    #endregion

    //Method
    #region

    /// <summary>
    /// ü�¿� ���� ȭ�� �������� ����
    /// </summary>
    private void AdjustUIHitColor()
    {
        if (isTakingDamage)
        {
            timer += Time.deltaTime;

            // �ǰ� ����Ʈ ���ӽð� ���� ���� ������ ����
            Img.color = Color.LerpUnclamped(AlignColor, BaseColor, timer / hitEffectDuration);

            if (timer >= hitEffectDuration)
            {
                isTakingDamage = false;
                timer = 0f;
            }
        }
        else
        {
            // ���� �������� ����
            Img.color = Color.LerpUnclamped(AlignColor, BaseColor, Player.Instance.CurrentHp / MaxHP);
        }
    }

    // �ǰ� ����Ʈ�� Ʈ�����ϴ� �޼���
    public void TriggerHitEffect()
    {
        isTakingDamage = true;
    }

    #endregion

    //Unity Event
    #region
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        MaxHP = Player.Instance.MaxHP;
        Img = this.GetComponent<Image>();
    }

    private void Update()
    {
        AdjustUIHitColor();
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            Player.Instance.P_TakeDamage(10);
        }*/
    }

    #endregion
}
