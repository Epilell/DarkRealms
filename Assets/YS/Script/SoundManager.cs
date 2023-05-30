using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource, efSource;  // 오디오 소스
    public Image image1, image2;
    public Sprite music_off, music_on, effect_off, effect_on;

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;  // 배경음악 볼륨 조절
        if (musicSource.volume <= 0)
        {
            image1.sprite = music_off;  // 배경음악 볼륨이 0이면 OFF로 이미지 변경
        }
        else
        {
            image1.sprite = music_on;  // 0이 아니면 ON으로 이미지 변경
        }
    }

    public void SetEffectVolume(float volume)
    {
        efSource.volume = volume;  // 효과음 볼륨 조절
        if (efSource.volume <= 0)
        {
            image2.sprite = effect_off;
        }
        else
        {
            image2.sprite = effect_on;
        }
    }

    public void OnSfx() // 효과음 재생 함수
    {
        efSource.Play();
    }
}