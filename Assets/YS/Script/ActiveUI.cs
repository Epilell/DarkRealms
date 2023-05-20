using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour
{
    public GameObject inventoryPanel;  // �κ��丮UI
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
    }
}