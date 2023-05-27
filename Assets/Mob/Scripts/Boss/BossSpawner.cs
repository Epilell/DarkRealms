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
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(bossHPSliderPrefab);

        //Slider UI 프로젝트를 parent("Canvas" 오브젝트)의 자식으로 설정 단, UI는 캔버스의 자식으로 설정되어 있어야 화면에 보임
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -40);
        sliderClone.GetComponent<BossHPViewer>().Setup(Boss.GetComponent<BossHP>());
    }
}
