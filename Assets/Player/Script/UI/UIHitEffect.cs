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
    private bool isTakingDamage = false; // 추가된 변수
    private float hitEffectDuration = 0.2f; // 피격 이펙트 지속시간
    private float timer = 0f; // 타이머
    #endregion

    //Method
    #region

    /// <summary>
    /// 체력에 따른 화면 붉음정도 변경
    /// </summary>
    private void AdjustUIHitColor()
    {
        if (isTakingDamage)
        {
            timer += Time.deltaTime;

            // 피격 이펙트 지속시간 동안 붉은 색상을 적용
            Img.color = Color.LerpUnclamped(AlignColor, BaseColor, timer / hitEffectDuration);

            if (timer >= hitEffectDuration)
            {
                isTakingDamage = false;
                timer = 0f;
            }
        }
        else
        {
            // 원래 색상으로 복구
            Img.color = Color.LerpUnclamped(AlignColor, BaseColor, Player.Instance.CurrentHp / MaxHP);
        }
    }

    // 피격 이펙트를 트리거하는 메서드
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
