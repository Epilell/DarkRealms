using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMenu : MonoBehaviour
{
    public GameObject optionMenu;

    protected bool activeOptionMenu = false;

    private void Start()
    {
        optionMenu.SetActive(activeOptionMenu);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activeOptionMenu = !activeOptionMenu;
            optionMenu.SetActive(activeOptionMenu);
            if (activeOptionMenu == true) { Time.timeScale = 0; }
            else { Time.timeScale = 1; }
        }
    }

    /*public GameObject inventoryPanel;  // 인벤토리UI
    [SerializeField] private GameObject SkillGroup;  // 스킬UI
    [SerializeField] private GameObject profile_in, profile_out;  // 프로필UI

    // UI가 표시되는지 여부
    protected bool activeInventory = false;
    protected bool activeProfile = false;
    protected bool activeSkillGroup = false;

    private void Start()
    {
        profile_out.SetActive(!activeProfile); // 처음에 프로필 표시
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab을 누르면
        {
            // active 반전(닫혀있으면 열고, 열려있으면 닫기)
            activeInventory = !activeInventory;
            activeProfile = !activeProfile;
            activeSkillGroup = !activeSkillGroup;

            inventoryPanel.SetActive(activeInventory);
            profile_in.SetActive(activeProfile);
            profile_out.SetActive(!activeProfile);
            SkillGroup.SetActive(!activeSkillGroup);
        }
    }*/
}