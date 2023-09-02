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

    [Header("AudioClip")] // ����� Ŭ��(����)
    public AudioClip normalAudioClip; // �Ϲݸ�
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
            float distance = Vector3.Distance(portal.transform.position, player.transform.position); // ��Ż�� �÷��̾��� �Ÿ� ���

            if (distance <= 20 && !isPlaying) // �Ÿ��� 20 ���̸� ���� ���
            {
                portalBgm.Play();
                isPlaying = true;
            }
            else if (distance > 20 && isPlaying) // ���̸� ����
            {
                portalBgm.Stop();
                isPlaying = false;
            }

            if (isPlaying) portalBgm.volume = Mathf.Lerp(0.7f, 0, distance / 20); // �Ÿ��� ����� ����������� ������
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

    public void StopSound(string sourceName) // ȿ���� ���
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

    public void ChangeBgm() // ������
    {
        bgm.clip = bossAudioClip;
        bgm.Play();
    }

    public void ChangeOriginalBgm() // �Ϲݸ�
    {
        bgm.clip = normalAudioClip;
        bgm.Play();
    }
}