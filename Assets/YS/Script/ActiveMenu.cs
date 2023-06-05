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

    /*public GameObject inventoryPanel;  // �κ��丮UI
    [SerializeField] private GameObject SkillGroup;  // ��ųUI
    [SerializeField] private GameObject profile_in, profile_out;  // ������UI

    // UI�� ǥ�õǴ��� ����
    protected bool activeInventory = false;
    protected bool activeProfile = false;
    protected bool activeSkillGroup = false;

    private void Start()
    {
        profile_out.SetActive(!activeProfile); // ó���� ������ ǥ��
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab�� ������
        {
            // active ����(���������� ����, ���������� �ݱ�)
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