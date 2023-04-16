using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    //[SerializeField]
    // private GameObject enemyPrefab; //적 프리팹
    //[SerializeField]
    //private float spawnTime; //적 생성주기
    //[SerializeField]
    //private Transform[] spawnPoints; // 몹 스폰 포인트

    [SerializeField]
    private GameObject mobHPSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹
    [SerializeField]
    private Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform

    [SerializeField]
    //private PlayerHP playerHP;
    //private Vector3 vec = new Vector3(0, 0.5f, 0);
    //어떤 몬스터를 소환할지 리스트
    private List<GameObject> mobList;  //List<자료형> 변수명 = new List<자료형>();
    public List<GameObject> MobList => mobList;
    public List<Transform> spawnPoint;
    public float spawnDelay = 0.5f;

    private void Awake()
    {
        //적 리스트 메모리 할당
        //mobList = new List<MobAI>();
        //적 생성 코루틴 함수 호출 -> 코루틴 : 시간의 경과에 따른 명령을 주고싶을 때 사용 
        StartCoroutine("SpawnMob");
    }

    private IEnumerator SpawnMob()
    {
        while (true)
        {
            for (int i = 0; i < mobList.Count; i++)
            {
                // i번째 몬스터를 i번째 spawnPoint에 생성
                //Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity);
                GameObject clone = Instantiate(mobList[i],spawnPoint[i].position, Quaternion.identity) as GameObject;
                SpawnEnemyHPSlider(clone);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }


    private void SpawnEnemyHPSlider(GameObject enemy)
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
