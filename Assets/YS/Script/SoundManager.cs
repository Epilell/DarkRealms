using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("AudioSource")] // ����� �ҽ�
    public AudioSource bgm;
    public AudioSource portalBgm;
    public List<AudioSource> sfxSources;
    public List<AudioSource> mobSources;
    public List<AudioSource> bossSources;

    [Header("AudioClip")] // ����� Ŭ��(����)
    public AudioClip bossAudioClip; // ������

    [Header("ImgSource")] // �̹��� �ҽ�
    public Sprite bgm_on;
    public Sprite bgm_off;
    public Sprite sfx_on;
    public Sprite sfx_off;

    [Header("IconObj")] // ������ ������Ʈ
    public Image bgmIcon;
    public Image sfxIcon;

    [Header("Player")] // �÷��̾�
    public Player player;

    [Header("Portal")] // ��Ż
    public Transform portal;

    [Header("FireTrap")] // �Ҳ� ����
    public List<GameObject> fireTrapList = new List<GameObject>();
    public GameObject fireTrapParentObject;

    GameObject closestFire = null; // ���� ����� �Ҳ�
    float minDistance; // �Ÿ��� ����

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
            // ��Ż
            float distance = Vector3.Distance(portal.transform.position, player.transform.position); // ��Ż�� �÷��̾��� �Ÿ� ���

            if (distance <= 20 && !isPortalPlaying) // �Ÿ��� 20 ���̸� ���� ���
            {
                portalBgm.Play();
                isPortalPlaying = true;
            }
            else if (distance > 20 && isPortalPlaying) // ���̸� ����
            {
                portalBgm.Stop();
                isPortalPlaying = false;
            }

            if (isPortalPlaying) portalBgm.volume = Mathf.Lerp(1, 0, distance / 20); // �Ÿ��� ����� ����������� ������

            // �Ҳ� ����
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

    public void SetBgmVolume(float volume) // ������� ���� ����
    {
        bgm.volume = volume;
        if (bgm.volume <= 0) { bgmIcon.sprite = bgm_off; } // ������ 0�̸� OFF�� �̹��� ����
        else { bgmIcon.sprite = bgm_on; } // 0�� �ƴϸ� ON���� �̹��� ����
    }

    public void SetSfxVolume(float volume)  // ȿ���� ���� ����
    {
        foreach (AudioSource efSource in sfxSources)
        {
            efSource.volume = volume;
            if (efSource.volume <= 0) { sfxIcon.sprite = sfx_off; } // ������ 0�̸� OFF�� �̹��� ����
            else { sfxIcon.sprite = sfx_on; }
        }
        foreach (AudioSource efSource in mobSources) efSource.volume = volume;
        foreach (AudioSource efSource in bossSources) efSource.volume = volume;

    }

    public void PlaySound(string sourceName) // ȿ���� ���
    {
        AudioSource targetSource = null; // ��� ȿ���� �ҽ�

        foreach (AudioSource audioSource in sfxSources) // ��ġ�ϴ� �� ã��
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

    public void StopSound(string sourceName) // ȿ���� ����
    {
        AudioSource targetSource = null; // ��� ȿ���� �ҽ�

        foreach (AudioSource audioSource in sfxSources) // ��ġ�ϴ� �� ã��
        {
            if (audioSource.name == sourceName)
            {
                targetSource = audioSource;
                break;
            }
        }

        if (targetSource != null) { targetSource.Stop(); } // ã���� ����
        else { }
    }

    public void MobSound(string sourceName) // ���� ȿ���� ���
    {
        AudioSource targetSource = null; // ��� ȿ���� �ҽ�

        foreach (AudioSource audioSource in mobSources) // ��ġ�ϴ� �� ã��
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

    public void BossPlaySound(string sourceName) // ���� ȿ���� ���
    {
        AudioSource targetSource = null; // ��� ȿ���� �ҽ�

        foreach (AudioSource audioSource in bossSources) // ��ġ�ϴ� �� ã��
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

    public void ChangeBossBgm() // ������
    {
        bgm.clip = bossAudioClip;
        bgm.Play();
    }
}