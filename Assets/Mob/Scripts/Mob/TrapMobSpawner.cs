using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMobSpawner : MonoBehaviour
{
    private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    [SerializeField]
    private GameObject mobHPSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������
    [Header("mob")]
    [SerializeField]
    //� ���͸� ��ȯ���� ����Ʈ
    private List<GameObject> mobList;
    private Transform spawnPoint;

    [Header("spawn")]
    [SerializeField]
    private float spawnDelay = 0.5f;
    [SerializeField]
    private List<GameObject> Wall;
    [SerializeField]
    private List<GameObject> WallPoint;

    private bool isPlayerOn = false;

    //���� �÷��̾� ��ġ ������ ����
    //���� ���� ����
    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount == 1&&isPlayerOn)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&isPlayerOn==false)
        {
            spawnPoint = collision.transform;
            closeMap();
            StartCoroutine(SpawnMob());
            isPlayerOn = true;
        }
    }
    private void closeMap()
    {
        for(int i = 0; i < WallPoint.Count; i++)
        {
            GameObject wallClone = Instantiate(Wall[i], WallPoint[i].transform.position, Quaternion.identity) as GameObject;
            wallClone.transform.SetParent(this.transform.GetChild(0).transform);
        }
    }
    private IEnumerator SpawnMob()
    {
        for (int i = 0; i < mobList.Count; i++)
        {
            // i��° ���͸� i��° spawnPoint�� ����
            GameObject clone = Instantiate(mobList[i], spawnPoint.position , Quaternion.identity) as GameObject;
            clone.transform.SetParent(this.transform);
            SpawnEnemyHPSlider(clone);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(mobHPSliderPrefab);

        //Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ���� ��, UI�� ĵ������ �ڽ����� �����Ǿ� �־�� ȭ�鿡 ����
        sliderClone.transform.SetParent(canvasTransform);

        //���� �������� �ٲ� ũ�⸦ �缳��
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<MobHPViewer>().Setup(enemy.GetComponent<MobHP>());
    }
}
