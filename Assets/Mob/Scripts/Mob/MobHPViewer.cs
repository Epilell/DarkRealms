using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�����ÿ� ���� ȣ��
public class MobHPViewer : MonoBehaviour
{
    private MobHP mobHP;
    private Slider hpSlider;
    [Header("cc�����ִ� Image")]
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
    /// CC_HPViewer�� ǥ�õǰ� �ϴ� �Լ�
    /// </summary>
    /// <param name="CC_Num">CC���� 1=���ο�,2=����,3=ȭ��,4=���</param>
    /// <param name="CC_Duration">CC���ӽð�</param>
    public void CC_HPViewer(int CC_Num, float CC_Duration)
    {
        if (CC_Num == 1)//���ο�
        {
            ImgChange(slowImg, CC_Duration);
        }
        else if (CC_Num == 2)//����
        {
            ImgChange(stunImg, CC_Duration);
        }
        else if (CC_Num == 3)//ȭ��
        {
            ImgChange(burnImg, CC_Duration);
        }
        else if (CC_Num == 4)//���
        {
            ImgChange(reducedDefenseImg, CC_Duration);
        }
        else
        {
            Debug.Log("CC_Changed�� �߸��� CC_Num�� �Էµ�");
        }


    }
    private void ImgChange(Sprite sp, float CC_Duration)
    {
        //CC_Count�� ���� ���° �� Ȱ��ȭ ���� �����ϴ� �Լ�
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
            Debug.Log("ImgChange �� CC_count���� �����߻�");
        }
    }
    /// <summary>
    /// color.a �ٲ��ִ� �Լ�
    /// </summary>
    /// <param name="go">���ӿ�����Ʈ</param>
    /// <param name="sp">������Sprite</param>
    /// <param name="CC_Duration">���ӽð�</param>
    IEnumerator Sprite_changer(GameObject go, Sprite sp, float CC_Duration)
    {
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            CC_Count++;
            sr.sprite = sp; // ��������Ʈ ����
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
