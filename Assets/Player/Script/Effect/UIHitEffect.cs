using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHitEffect : MonoBehaviour
{
    //Field
    #region
    public static UIHitEffect Instance;
    public Image PermanentColor, TemporaryColor;    //체력 30%이하 변경 대상, 피격시 변경 대상

    [SerializeField]
    private Color BaseColor = new(255 / 255f, 0 / 255f, 0 / 255f, 0 / 255f),    //기본 색상
        AlignColor = new(255 / 255f, 0 / 255f, 0 / 255f, 60 / 255f),            //30%이하 색상
        HitColor = new(255 / 255f, 0 / 255f, 0 / 255f, 10 / 255f);              //피격시 색상

    private float MaxHP;
    private float timer = 0f, hitEffectDuration = 0.2f;
    #endregion

    //Method
    #region

    /// <summary>
    /// 체력 30% 미만시 화면 붉음정도 변경
    /// </summary>
    private void AdjustColor()
    {
        //체력이 30% 이하일때
        if (Player.Instance.GetPlayerstate() != 0)
        {
            PermanentColor.color = Color.LerpUnclamped(AlignColor, BaseColor, Player.Instance.CurrentHp / (MaxHP * 0.3f));
        }

        //피격시
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
