using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Strengthen : MonoBehaviour
{
    public int currentLevel = 1; // 현재 장비 레벨
    public int maxLevel = 3; // 최대 장비 레벨
    public float successRateStep = 0.3f; // 강화 성공 확률 증가량

    public Button strengthenBtn; // 강화 버튼
    public StrengthenInventory inventory; // 강화 인벤토리

    private void Start()
    {
        strengthenBtn.onClick.AddListener(EnhanceEquipment);
        inventory = FindObjectOfType<StrengthenInventory>(); // StrengthenInventory 타입의 인스턴스를 찾아서 할당
    }

    private void EnhanceEquipment()
    {
        // 슬롯 내부에 태그가 "Material"인 강화 재료 아이템이 있는지 확인
        bool hasMaterials = CheckIfMaterialsExist();

        if (hasMaterials)
        {
            int materialCount = inventory.GetMaterialCount(); // 인벤토리에서 강화 재료 아이템 개수 가져오기
            float successRate = (materialCount * successRateStep) + 0.1f; // 강화 성공 확률 계산

            // 강화 성공 여부 결정
            bool isEnhancementSuccessful = Random.Range(0f, 1f) < successRate;

            if (isEnhancementSuccessful)
            {
                currentLevel++;
                Debug.Log("장비 강화 성공! 현재 레벨: " + currentLevel);
            }
            else
            {
                Debug.Log("강화 실패!");
            }

            // 슬롯 내부의 모든 강화 재료 아이템 삭제
            inventory.RemoveMaterials(materialCount);
        }
        else
        {
            Debug.Log("강화 재료가 부족합니다.");
        }
    }

    private bool CheckIfMaterialsExist()
    {
        // 강화 인벤토리 내의 모든 슬롯 확인
        foreach (Transform slot in inventory.transform)
        {
            // 슬롯 내부에 태그가 "Material"인 객체가 있는지 확인
            if (slot.CompareTag("Material"))
            {
                return true; // 강화 재료가 존재함
            }
        }

        return false; // 강화 재료가 존재하지 않음
    }

    /*private void Start()
    {
        strengthenBtn.onClick.AddListener(EnhanceEquipment); // 강화 버튼에 강화 기능 추가
    }

    private void EnhanceEquipment()
    {
        int materialCount = inventory.GetMaterialCount(); // 인벤토리에서 재료 개수 가져오기
        float successRate = (materialCount * successRateStep) + 0.1f; // 재료 개수에 따른 성공 확률 계산

        // 강화 성공 여부 결정
        bool isEnhancementSuccessful = Random.Range(0f, 1f) < successRate;

        if (isEnhancementSuccessful)
        {
            currentLevel++;
            Debug.Log("장비 강화 성공! 현재 레벨: " + currentLevel);
        }
        else
        {
            Debug.Log("장비 강화 실패.");
        }

        // 인벤토리에서 재료 아이템 삭제
        inventory.RemoveMaterials(materialCount);
    }*/
}