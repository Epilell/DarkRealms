using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource, efSource;  // ����� �ҽ�
    public Image image1, image2;
    public Sprite music_off, music_on, effect_off, effect_on;

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;  // ������� ���� ����
        if (musicSource.volume <= 0)
        {
            image1.sprite = music_off;  // ������� ������ 0�̸� OFF�� �̹��� ����
        }
        else
        {
            image1.sprite = music_on;  // 0�� �ƴϸ� ON���� �̹��� ����
        }
    }

    public void SetEffectVolume(float volume)
    {
        efSource.volume = volume;  // ȿ���� ���� ����
        if (efSource.volume <= 0)
        {
            image2.sprite = effect_off;
        }
        else
        {
            image2.sprite = effect_on;
        }
    }

    public void OnSfx() // ȿ���� ��� �Լ�
    {
        efSource.Play();
    }
}