/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRisk : MonoBehaviour
{
    public List<GameObject> imageList; // �̹��� ������Ʈ ����Ʈ
    // public float activateDelay; // Ȱ��ȭ ������ �ð�

    private void Start()
    {
        foreach (GameObject image in imageList) { image.SetActive(false); }
        int randomIndex = Random.Range(0, imageList.Count);
        imageList[randomIndex].SetActive(true);
        // DisableAllImages(); // ��� �̹����� ��Ȱ��ȭ
        // StartCoroutine(ActivateRandomImageRoutine()); // ������ �̹����� Ȱ��ȭ
    }

    private void DisableAllImages()
    {
        foreach (GameObject image in imageList)
        {
            image.SetActive(false); // ����Ʈ ���� ��� ��Ȱ��ȭ
        }
    }

    private IEnumerator ActivateRandomImageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(activateDelay); // ������ �ð���ŭ ��ٸ�

            DisableAllImages(); // ��� �̹��� ��Ȱ��ȭ

            int randomIndex = Random.Range(0, imageList.Count); // ������ ������ �ε��� �� ����
            GameObject randomImage = imageList[randomIndex]; // �����ϰ� �̹��� ����
            randomImage.SetActive(true); // ������ �̹��� Ȱ��ȭ
        }
    }
}*/