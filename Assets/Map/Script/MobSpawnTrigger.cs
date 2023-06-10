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
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(mobHPSliderPrefab);

        //Slider UI 프로젝트를 parent("Canvas" 오브젝트)의 자식으로 설정 단, UI는 캔버스의 자식으로 설정되어 있어야 화면에 보임
        sliderClone.transform.SetParent(canvasTransform);

        //계층 설정으로 바뀐 크기를 재설정
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<MobHPViewer>().Setup(enemy.GetComponent<MobHP>());
    }
}
