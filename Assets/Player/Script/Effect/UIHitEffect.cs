using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHitEffect : MonoBehaviour
{
    //Field
    #region
    public static UIHitEffect Instance;
    public Image PermanentColor, TemporaryColor;    //ü�� 30%���� ���� ���, �ǰݽ� ���� ���

    [SerializeField]
    private Color BaseColor = new(255 / 255f, 0 / 255f, 0 / 255f, 0 / 255f),    //�⺻ ����
        AlignColor = new(255 / 255f, 0 / 255f, 0 / 255f, 60 / 255f),            //30%���� ����
        HitColor = new(255 / 255f, 0 / 255f, 0 / 255f, 10 / 255f);              //�ǰݽ� ����

    private float MaxHP;
    private float timer = 0f, hitEffectDuration = 0.2f;
    #endregion

    //Method
    #region

    /// <summary>
    /// ü�� 30% �̸��� ȭ�� �������� ����
    /// </summary>
    private void AdjustColor()
    {
        //ü���� 30% �����϶�
        if (Player.Instance.GetPlayerstate() != 0)
        {
            PermanentColor.color = Color.LerpUnclamped(AlignColor, BaseColor, Player.Instance.CurrentHp / (MaxHP * 0.3f));
        }

        //�ǰݽ�
        TemporaryColor.color = Color.LerpUnclamped(BaseColor, HitColor, timer / hitEffectDuration);
    }

    public void IsDamaged()
    {
        timer = hitEffectDuration;
    }

    #endregion

    //Unity Event
    #region
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        MaxHP = Player.Instance.MaxHP;
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
        AdjustColor();
    }

    #endregion
}
