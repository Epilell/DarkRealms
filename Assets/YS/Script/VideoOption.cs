using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode; // ��üȭ�� ��� ������ ����
    public Dropdown resolutionDropdown;  // �ػ� ��Ӵٿ� UI
    public Toggle fullscreenBtn; // ��üȭ�� ��� UI
    List<Resolution> resolutions = new List<Resolution>(); // �ػ� ����Ʈ
    public int resolutionNum; // ������ �ػ� �ε���

    void Start()
    {
        InitUI(); // UI �ʱ�ȭ �Լ� ȣ��
    }

    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 144) // 144hz �����ϴ� �ػ󵵸� ����Ʈ�� �߰�
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        // resolutions.AddRange(Screen.resolutions); // ��� �����ϴ� �ػ󵵸� ����Ʈ�� �߰�
        resolutionDropdown.options.Clear(); // ��Ӵٿ� �ɼ� �ʱ�ȭ

        int optionNum = 0;

        foreach (Resolution item in resolutions) // �����ϴ� �ػ� �ɼ� �߰�
        {
            Dropdown.OptionData option = new Dropdown.OptionData(); // Dropdown.OptionData Ŭ������ �ν��Ͻ��� ����
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz"; // text �Ӽ��� item�� width, height, refreshRate ������ ���ڿ��� ���ļ� ����
            resolutionDropdown.options.Add(option); // ����Ʈ�� �߰�

            if (item.width == Screen.width && item.height == Screen.height) // ���� �ػ󵵿� ��ġ�ϴ� �ɼ� ����
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue(); // ��Ӵٿ� UI ����

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false; // ��üȭ�� ��� �ʱ�ȭ
    }

    public void DropboxOptionChange(int x) // �ػ� ��Ӵٿ� ���� �� ȣ��
    {
        resolutionNum = x; // ������ �ػ��� �ε��� ����
    }

    public void FullScrennBtn(bool isFull) // ��üȭ�� ��� ���� �� ȣ��
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed; // ��üȭ�� ��� ����
        Debug.Log(screenMode); // ��üȭ�� ��� ���
    }

    public void OkBtnClick() // Ȯ�� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    {
        Debug.Log("ȭ�� ��ȯ"); // �׽�Ʈ �α� ���
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode); // ������ �ػ󵵿� ��üȭ�� ���� ȭ�� ��ȯ
    }
}