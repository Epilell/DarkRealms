using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject bossSpawningEffect;
    [SerializeField]
    private GameObject bossSpawnPoint;
    [SerializeField]
    private GameObject bossHPSliderPrefab;
    private Transform canvasTransform;
    private bool _isSpawn = false;
    private void Awake()
    {
        //StartCoroutine("BossSpawn");
        canvasTransform = GameObject.Find("Canvas").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (_isSpawn == false)
            {
                StartCoroutine("BossSpawn");
                _isSpawn = true;
            }

        }

    }

    private IEnumerator BossSpawn()
    {
        yield return new WaitForSeconds(1f);
        GameObject bossSpawn = Instantiate(bossSpawningEffect, bossSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        Destroy(bossSpawn, 4f);
        yield return new WaitForSeconds(4f);
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
        sliderClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(480, -110);
        sliderClone.GetComponent<BossHPViewer>().Setup(Boss.GetComponent<BossHP>());
    }
}
