using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject bossSpawnPoint;
    [SerializeField]
    private GameObject bossHPSliderPrefab;
    public Transform canvasTransform;
    private void Awake()
    {
        StartCoroutine("BossSpawn");
    }
    private IEnumerator BossSpawn()
    {
        GameObject clone = Instantiate(boss, bossSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        clone.GetComponent<BossPattern>().SetCanvas(canvasTransform);
        SpawnBossHPSlider(clone);
        yield break;
    }
    private void SpawnBossHPSlider(GameObject Boss)
    {
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(bossHPSliderPrefab);

        //Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ���� ��, UI�� ĵ������ �ڽ����� �����Ǿ� �־�� ȭ�鿡 ����
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -40);
        sliderClone.GetComponent<BossHPViewer>().Setup(Boss.GetComponent<BossHP>());
    }
}
