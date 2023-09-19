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
    public List<AudioSource> bossSources;

    [Header("AudioClip")] // 오디오 클립(파일)
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

    [Header("FireTrap")] // 불꽃 함정
    public List<GameObject> fireTrapList = new List<GameObject>();
    public GameObject fireTrapParentObject;

    GameObject closestFire = null; // 가장 가까운 불꽃
    float minDistance; // 거리값 저장

    private bool isPortalPlaying = false, isFireTrapPlaying = false;
    public bool isFire = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            portalBgm.volume = 0;

            foreach (Transform child in fireTrapParentObject.transform)
            {
                fireTrapList.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            // 포탈
            float distance = Vector3.Distance(portal.transform.position, player.transform.position); // 포탈과 플레이어의 거리 계산

            if (distance <= 20 && !isPortalPlaying) // 거리가 20 안이면 음악 재생
            {
                portalBgm.Play();
                isPortalPlaying = true;
            }
            else if (distance > 20 && isPortalPlaying) // 밖이면 중지
            {
                portalBgm.Stop();
                isPortalPlaying = false;
            }

            if (isPortalPlaying) portalBgm.volume = Mathf.Lerp(1, 0, distance / 20); // 거리에 비례해 가까워질수록 볼륨업

            // 불꽃 함정
            minDistance = float.MaxValue;

            foreach (GameObject fireTrap in fireTrapList)
            {
                float distance2 = Vector3.Distance(fireTrap.transform.position, player.transform.position);

                if (distance2 < minDistance)
                {
                    minDistance = distance2;
                    closestFire = fireTrap;
                }
            }

            if (closestFire != null)
            {
                if (isFire)
                {
                    if (minDistance <= 10 && !isFireTrapPlaying)
                    {
                        FindObjectOfType<SoundManager>().PlaySound("FireTrap");
                        isFireTrapPlaying = true;
                    }
                    else if (minDistance > 10 && isFireTrapPlaying)
                    {
                        FindObjectOfType<SoundManager>().StopSound("FireTrap");
                        isFireTrapPlaying = false;
                    }
                }
                else
                {
                    FindObjectOfType<SoundManager>().StopSound("FireTrap");
                    isFireTrapPlaying = false;
                }
            }
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
        foreach (AudioSource efSource in mobSources) efSource.volume = volume;
        foreach (AudioSource efSource in bossSources) efSource.volume = volume;

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

    public void StopSound(string sourceName) // 효과음 정지
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

        if (targetSource != null) { targetSource.Stop(); } // 찾으면 정지
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

    public void BossPlaySound(string sourceName) // 보스 효과음 재생
    {
        AudioSource targetSource = null; // 대상 효과음 소스

        foreach (AudioSource audioSource in bossSources) // 일치하는 거 찾기
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

    public void ChangeBossBgm() // 보스맵
    {
        bgm.clip = bossAudioClip;
        bgm.Play();
    }
}