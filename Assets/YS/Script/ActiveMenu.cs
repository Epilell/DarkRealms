using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveMenu : MonoBehaviour
{
    public GameObject manuObj, optionMenu;
    public CanvasGroup main, option, sound, display, warning;

    private void Start() { if (SceneManager.GetActiveScene().name == "InGame") optionMenu.SetActive(false); }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "InGame")
            {
                if (optionMenu.activeSelf == false && sound.alpha == 0 && display.alpha == 0 && warning.alpha == 0) // ����â�� ��� �������� �� ���� �ɼ�â �ѱ�
                {
                    optionMenu.SetActive(true);
                }
                else if (optionMenu.activeSelf == true && sound.alpha == 0 && display.alpha == 0 && warning.alpha == 0) // ���� �ɼ�â�� ���������� ����
                {
                    optionMenu.SetActive(false);
                }
                else if (option.alpha == 0 && sound.alpha == 1 && display.alpha == 0 && warning.alpha == 0) // ���� â�� ���������� ���� ���� �ɼ�â �ѱ�
                {
                    CanvasGroupOff(sound);
                    CanvasGroupOn(option);
                }
                else if (option.alpha == 0 && sound.alpha == 0 && display.alpha == 1 && warning.alpha == 0) // �ػ� â�� ���������� ���� ���� �ɼ�â �ѱ�
                {
                    CanvasGroupOff(display);
                    CanvasGroupOn(option);
                }
                else if (option.alpha == 0 && sound.alpha == 0 && display.alpha == 0 && warning.alpha == 1) // ��� â�� ���������� ���� ���� �ɼ�â �ѱ�
                {
                    CanvasGroupOff(warning);
                    CanvasGroupOn(option);
                }
                else { }
            }
            else if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                if (main.alpha == 0 && option.alpha == 1 && sound.alpha == 0 && display.alpha == 0)
                {
                    CanvasGroupOff(option);
                    CanvasGroupOn(main);
                }
                else if (main.alpha == 0 && option.alpha == 0 && sound.alpha == 1 && display.alpha == 0)
                {
                    CanvasGroupOff(sound);
                    CanvasGroupOn(option);
                }
                else if (main.alpha == 0 && option.alpha == 0 && sound.alpha == 0 && display.alpha == 1)
                {
                    CanvasGroupOff(display);
                    CanvasGroupOn(option);
                }
                else { }
            }
        }
    }

    public void CanvasGroupOn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void SetMenuLastSibling()
    {
        manuObj.transform.SetAsLastSibling();
    }
}