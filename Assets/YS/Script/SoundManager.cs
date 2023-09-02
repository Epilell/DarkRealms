using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("AudioSource")] // 오디오 소스
    public AudioSource bgm;
    public AudioSource portalBgm;
    public List<AudioSource> sfxSources;
    public List<AudioSource> mobSources;

    [Header("AudioClip")] // 오디오 클립(파일)
    public AudioClip normalAudioClip; // 일반맵
    public AudioClip bossAudioClip; // 보스맵

    [Header("ImgSource")] // 이미지 소스
    public Sprite bgm_on;
    public Sprite bgm_off;
    public Sprite sfx_on;
    public Sprite sfx_off;

    [Header("IconObj")] // 아이콘 오브젝트
    public Image bgmIcon;
    public Image sfxIcon;

    [Header("Player")] // 플레이어
    public Player player;

    [Header("Portal")] // 포탈
    public Transform portal;

    private bool isPlaying = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            portal = FindObjectOfType<MagneticField>().escape;
            portalBgm.volume = 0;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            float distance = Vector3.Distance(portal.transform.position, player.transform.position); // 포탈과 플레이어의 거리 계산

            if (distance <= 20 && !isPlaying) // 거리가 20 안이면 음악 재생
            {
                portalBgm.Play();
                isPlaying = true;
            }
            else if (distance > 20 && isPlaying) // 밖이면 중지
            {
                portalBgm.Stop();
                isPlaying = false;
            }

            if (isPlaying) portalBgm.volume = Mathf.Lerp(0.7f, 0, distance / 20); // 거리에 비례해 가까워질수록 볼륨업
        }
    }

    public void SetBgmVolume(float volume) // 배경음악 볼륨 조절
    {
        bgm.volume = volume;
        if (bgm.volume <= 0) { bgmIcon.sprite = bgm_off; } // 볼륨이 0이면 OFF로 이미지 변경
        else { bgmIcon.sprite = bgm_on; } // 0이 아니면 ON으로 이미지 변경
    }

    public void SetSfxVolume(float volume)  // 효과음 볼륨 조절
    {
        foreach (AudioSource efSource in sfxSources)
        {
            efSource.volume = volume;
            if (efSource.volume <= 0) { sfxIcon.sprite = sfx_off; } // 볼륨이 0이면 OFF로 이미지 변경
            else { sfxIcon.sprite = sfx_on; }
        }
    }

    public void PlaySound(string sourceName) // 효과음 재생
    {
        AudioSource targetSource = null; // 대상 효과음 소스

        foreach (AudioSource audioSource in sfxSources) // 일치하는 거 찾기
        {
            if (audioSource.name == sourceName)
            {
                targetSource = audioSource;
                break;
            }
        }

        if (targetSource != null) { targetSource.Play(); } // 찾으면 실행
        else { }
    }

    public void StopSound(string sourceName) // 효과음 재생
    {
        AudioSource targetSource = null; // 대상 효과음 소스

        foreach (AudioSource audioSource in sfxSources) // 일치하는 거 찾기
        {
            if (audioSource.name == sourceName)
            {
                targetSource = audioSource;
                break;
            }
        }

        if (targetSource != null) { targetSource.Stop(); } // 찾으면 실행
        else { }
    }

    public void MobSound(string sourceName) // 몬스터 효과음 재생
    {
        AudioSource targetSource = null; // 대상 효과음 소스

        foreach (AudioSource audioSource in mobSources) // 일치하는 거 찾기
        {
            if (audioSource.name == sourceName)
            {
                targetSource = audioSource;
                break;
            }
        }

        if (targetSource != null) { targetSource.Play(); } // 찾으면 실행
        else { }
    }

    public void ChangeBgm() // 보스맵
    {
        bgm.clip = bossAudioClip;
        bgm.Play();
    }

    public void ChangeOriginalBgm() // 일반맵
    {
        bgm.clip = normalAudioClip;
        bgm.Play();
    }
}