using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    //[SerializeField]
    // private GameObject enemyPrefab; //�� ������
    //[SerializeField]
    //private float spawnTime; //�� �����ֱ�
    //[SerializeField]
    //private Transform[] spawnPoints; // �� ���� ����Ʈ

    [SerializeField]
    private GameObject mobHPSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform

    [SerializeField]
    //private PlayerHP playerHP;
    //private Vector3 vec = new Vector3(0, 0.5f, 0);
    //� ���͸� ��ȯ���� ����Ʈ
    private List<GameObject> mobList;  //List<�ڷ���> ������ = new List<�ڷ���>();
    public List<GameObject> MobList => mobList;
    public List<Transform> spawnPoint;
    public float spawnDelay = 0.5f;

    private void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        //mobList = new List<MobAI>();
        //�� ���� �ڷ�ƾ �Լ� ȣ�� -> �ڷ�ƾ : �ð��� ����� ���� ����� �ְ���� �� ��� 
        StartCoroutine("SpawnMob");
    }

    private IEnumerator SpawnMob()
    {
        while (true)
        {
            for (int i = 0; i < mobList.Count; i++)
            {
                // i��° ���͸� i��° spawnPoint�� ����
                //Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity);
                GameObject clone = Instantiate(mobList[i],spawnPoint[i].position, Quaternion.identity) as GameObject;
                SpawnEnemyHPSlider(clone);
                yield return new WaitForSeconds(spawnDelay);
            }
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
