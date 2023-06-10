using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject mobHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    public GameObject Mob;

    bool spawn_true;
    private void Awake()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }
    private void Start()
    {
        spawn_true = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (spawn_true == false)
            {
                //Debug.Log("Nope");
                GameObject mob = Instantiate(Mob, transform.position, transform.rotation);
                SpawnEnemyHPSlider(mob);
                spawn_true = true;
            }
        }
    }

    protected virtual void SpawnEnemyHPSlider(GameObject enemy)
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
