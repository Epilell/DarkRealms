using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialObj, mainMenuObj;
    public CanvasGroup main, option, inventory, map;

    public void OnOpenClick()
    {
        tutorialObj.SetActive(true);
        mainMenuObj.SetActive(false);
    }

    public void OnCloseClick()
    {
        tutorialObj.SetActive(false);
        mainMenuObj.SetActive(true);

        main.alpha = 1;
        option.alpha = 0;
        inventory.alpha = 0;
        map.alpha = 0;
    }

    public void OnLeftClick()
    {
        if (main.alpha == 0 && option.alpha == 1 && inventory.alpha == 0 && map.alpha == 0)
        {
            CanvasGroupOn(main);
            CanvasGroupOff(option);
        }
        else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 1 && map.alpha == 0)
        {
            CanvasGroupOn(option);
            CanvasGroupOff(inventory);
        }
        else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 1)
        {
            CanvasGroupOn(inventory);
            CanvasGroupOff(map);
        }
        else if (main.alpha == 1 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 0)
        {
            CanvasGroupOn(map);
            CanvasGroupOff(main);
        }
        else { }
    }

    public void OnRightClick()
    {
        if (main.alpha == 1 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 0)
        {
            CanvasGroupOn(option);
            CanvasGroupOff(main);
        }
        else if (main.alpha == 0 && option.alpha == 1 && inventory.alpha == 0 && map.alpha == 0)
        {
            CanvasGroupOn(inventory);
            CanvasGroupOff(option);
        }
        else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 1 && map.alpha == 0)
        {
            CanvasGroupOn(map);
            CanvasGroupOff(inventory);
        }
        else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 1)
        {
            CanvasGroupOn(main);
            CanvasGroupOff(map);
        }
        else { }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (main.alpha == 0 && option.alpha == 1 && inventory.alpha == 0 && map.alpha == 0)
                {
                    CanvasGroupOn(main);
                    CanvasGroupOff(option);
                }
                else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 1 && map.alpha == 0)
                {
                    CanvasGroupOn(option);
                    CanvasGroupOff(inventory);
                }
                else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 1)
                {
                    CanvasGroupOn(inventory);
                    CanvasGroupOff(map);
                }
                else if (main.alpha == 1 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 0)
                {
                    CanvasGroupOn(map);
                    CanvasGroupOff(main);
                }
                else { }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (main.alpha == 1 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 0)
                {
                    CanvasGroupOn(option);
                    CanvasGroupOff(main);
                }
                else if (main.alpha == 0 && option.alpha == 1 && inventory.alpha == 0 && map.alpha == 0)
                {
                    CanvasGroupOn(inventory);
                    CanvasGroupOff(option);
                }
                else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 1 && map.alpha == 0)
                {
                    CanvasGroupOn(map);
                    CanvasGroupOff(inventory);
                }
                else if (main.alpha == 0 && option.alpha == 0 && inventory.alpha == 0 && map.alpha == 1)
                {
                    CanvasGroupOn(main);
                    CanvasGroupOff(map);
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
}