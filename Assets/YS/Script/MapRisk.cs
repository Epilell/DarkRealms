using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRisk : MonoBehaviour
{
    public List<GameObject> imageList; // 이미지 오브젝트 리스트
    public float activateDelay; // 활성화 딜레이 시간

    private void Start()
    {
        DisableAllImages(); // 모든 이미지들 비활성화(초기화)
        StartCoroutine(ActivateRandomImageRoutine()); // 랜덤하게 이미지를 활성화
    }

    private void DisableAllImages()
    {
        foreach (GameObject image in imageList)
        {
            image.SetActive(false); // 리스트 돌며 모두 비활성화
        }
    }

    private IEnumerator ActivateRandomImageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(activateDelay); // 딜레이 시간만큼 기다림

            DisableAllImages(); // 모든 이미지 비활성화

            int randomIndex = Random.Range(0, imageList.Count); // 랜덤한 값으로 인덱스 값 생성
            GameObject randomImage = imageList[randomIndex]; // 랜덤하게 이미지 선택
            randomImage.SetActive(true); // 선택한 이미지 활성화
        }
    }
}
