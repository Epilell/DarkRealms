using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode; // 전체화면 모드 저장할 변수
    public Dropdown resolutionDropdown;  // 해상도 드롭다운 UI
    public Toggle fullscreenBtn; // 전체화면 토글 UI
    List<Resolution> resolutions = new List<Resolution>(); // 해상도 리스트
    public int resolutionNum; // 선택한 해상도 인덱스

    void Start()
    {
        InitUI(); // UI 초기화 함수 호출
    }

    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 144) // 144hz 지원하는 해상도만 리스트에 추가
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        // resolutions.AddRange(Screen.resolutions); // 모든 지원하는 해상도를 리스트에 추가
        resolutionDropdown.options.Clear(); // 드롭다운 옵션 초기화

        int optionNum = 0;

        foreach (Resolution item in resolutions) // 지원하는 해상도 옵션 추가
        {
            Dropdown.OptionData option = new Dropdown.OptionData(); // Dropdown.OptionData 클래스의 인스턴스를 생성
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz"; // text 속성에 item의 width, height, refreshRate 정보를 문자열로 합쳐서 삽입
            resolutionDropdown.options.Add(option); // 리스트에 추가

            if (item.width == Screen.width && item.height == Screen.height) // 현재 해상도와 일치하는 옵션 선택
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue(); // 드롭다운 UI 갱신

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false; // 전체화면 토글 초기화
    }

    public void DropboxOptionChange(int x) // 해상도 드롭다운 변경 시 호출
    {
        resolutionNum = x; // 선택한 해상도의 인덱스 저장
    }

    public void FullScrennBtn(bool isFull) // 전체화면 토글 변경 시 호출
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed; // 전체화면 모드 저장
        Debug.Log(screenMode); // 전체화면 모드 출력
    }

    public void OkBtnClick() // 확인 버튼 클릭 시 호출되는 함수
    {
        Debug.Log("화면 전환"); // 테스트 로그 출력
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode); // 선택한 해상도와 전체화면 모드로 화면 전환
    }
}