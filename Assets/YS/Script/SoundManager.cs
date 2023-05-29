using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("AudioSource")] // ����� �ҽ�
    public AudioSource bgm;
    public List<AudioSource> efSources;

    [Header("ImgSource")] // �̹��� �ҽ�
    public Sprite bgm_on;
    public Sprite bgm_off;
    public Sprite effect_on;
    public Sprite effect_off;

    [Header("IconObj")] // ������ ������Ʈ
    public Image bgmIcon;
    public Image efIcon;

    public void SetMusicVolume(float volume) // ������� ���� ����
    {
        bgm.volume = volume;
        if (bgm.volume <= 0) { bgmIcon.sprite = bgm_off; } // ������ 0�̸� OFF�� �̹��� ����
        else { bgmIcon.sprite = bgm_on; } // 0�� �ƴϸ� ON���� �̹��� ����
    }

    public void SetEffectVolume(float volume)  // ȿ���� ���� ����
    {
        foreach (AudioSource efSource in efSources)
        {
            efSource.volume = volume;
            if (efSource.volume <= 0) { efIcon.sprite = effect_off; } // ������ 0�̸� OFF�� �̹��� ����
            else { efIcon.sprite = effect_on; }
        }
    }

    public void PlaySound(string sourceName) // ȿ���� ���
    {
        AudioSource targetSource = null; // ��� ȿ���� �ҽ�

        foreach (AudioSource audioSource in efSources) // ��ġ�ϴ� �� ã��
        {
            if (audioSource.name == sourceName)
            {
                targetSource = audioSource;
                break;
            }
        }

        if (targetSource != null) { targetSource.Play(); } // ã���� ����
        else { }
    }
}