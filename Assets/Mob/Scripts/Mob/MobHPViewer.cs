using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//스폰시에 같이 호출
public class MobHPViewer : MonoBehaviour
{
    private MobHP mobHP;
    private Slider hpSlider;
    [Header("cc보여주는 Image")]
    [SerializeField]
    private GameObject CC_HPbar_viewer1;
    [SerializeField]
    private GameObject CC_HPbar_viewer2;
    [SerializeField]
    private GameObject CC_HPbar_viewer3;
    [SerializeField]
    private GameObject CC_HPbar_viewer4;
    [Header("CC ImageSprite")]
    [SerializeField]
    private Sprite slowImg;
    [SerializeField]
    private Sprite stunImg;
    [SerializeField]
    private Sprite burnImg;
    [SerializeField]
    private Sprite reducedDefenseImg;

    private int CC_Count = 0;
    public void Setup(MobHP mobHP)
    {
        this.mobHP = mobHP;
        mobHP.hpViewer = this;
        hpSlider = GetComponent<Slider>();
    }
    /// <summary>
    /// CC_HPViewer에 표시되게 하는 함수
    /// </summary>
    /// <param name="CC_Num">CC종류 1=슬로우,2=스턴,3=화상,4=방깎</param>
    /// <param name="CC_Duration">CC지속시간</param>
    public void CC_HPViewer(int CC_Num, float CC_Duration)
    {
        if (CC_Num == 1)//슬로우
        {
            ImgChange(slowImg, CC_Duration);
        }
        else if (CC_Num == 2)//스턴
        {
            ImgChange(stunImg, CC_Duration);
        }
        else if (CC_Num == 3)//화상
        {
            ImgChange(burnImg, CC_Duration);
        }
        else if (CC_Num == 4)//방깎
        {
            ImgChange(reducedDefenseImg, CC_Duration);
        }
        else
        {
            Debug.Log("CC_Changed에 잘못된 CC_Num이 입력됨");
        }


    }
    private void ImgChange(Sprite sp, float CC_Duration)
    {
        //CC_Count에 따라서 몇번째 뷰어를 활성화 할지 결정하는 함수
        if (CC_Count == 0)
        {
            StartCoroutine(Sprite_changer(CC_HPbar_viewer1, sp, CC_Duration));
        }
        else if (CC_Count == 1)
        {
            StartCoroutine(Sprite_changer(CC_HPbar_viewer2, sp, CC_Duration));
        }
        else if (CC_Count == 2)
        {
            StartCoroutine(Sprite_changer(CC_HPbar_viewer3, sp, CC_Duration));
        }
        else if (CC_Count == 3)
        {
            StartCoroutine(Sprite_changer(CC_HPbar_viewer4, sp, CC_Duration));
        }
        else
        {
            Debug.Log("ImgChange 의 CC_count에서 오류발생");
        }
    }
    /// <summary>
    /// color.a 바꿔주는 함수
    /// </summary>
    /// <param name="go">게임오브젝트</param>
    /// <param name="sp">변경할Sprite</param>
    /// <param name="CC_Duration">지속시간</param>
    IEnumerator Sprite_changer(GameObject go, Sprite sp, float CC_Duration)
    {
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            CC_Count++;
            sr.sprite = sp; // 스프라이트 변경
            Color color = sr.color;
            color.a = 1f;
            sr.color = color;
            yield return new WaitForSeconds(CC_Duration);
            color.a = 0f;
            sr.color = color;
            CC_Count--;
        }
        else
        {
            Debug.LogWarning("Sprite Renderer component not found.");
        }

    }

    private void Update()
    {
        hpSlider.value = mobHP.CurrentHP / mobHP.MaxHP;
    }
}
