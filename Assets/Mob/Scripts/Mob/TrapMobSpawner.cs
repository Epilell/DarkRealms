using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMobSpawner : MonoBehaviour
{
    private Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform
    [SerializeField]
    private GameObject mobHPSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹
    [Header("mob")]
    [SerializeField]
    //어떤 몬스터를 소환할지 리스트
    private List<GameObject> mobList;
    private Transform spawnPoint;

    [Header("spawn")]
    [SerializeField]
    private float spawnDelay = 0.5f;
    [SerializeField]
    private List<GameObject> Walls;
    [SerializeField]
    private Vector2 wallSize=new Vector2(1f,5f);

    private bool isPlayerOn = false;
    [SerializeField]
    private SkillUpgradeSelectionSystem selectionSystem;
    //몹은 플레이어 위치 주위로 낙하
    //결계로 주위 막힘
    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount == Walls.Count&&isPlayerOn)
        {
            for(int i = 0; i < Walls.Count; i++)
            {
                Animator animatorf = Walls[i].GetComponent<Animator>();
                animatorf.SetBool("On", false);

                BoxCollider2D boxCollider = Walls[i].GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                {
                    Destroy(boxCollider);
                    selectionSystem.OnOffPanel();
                }
            }
            //Destroy(this.gameObject);
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
        for(int i = 0; i < Walls.Count; i++)
        {
            //GameObject wallClone = Instantiate(Wall, WallPoint[i].transform.position, Quaternion.identity) as GameObject;
            //wallClone.transform.SetParent(this.transform.GetChild(0).transform);
            Animator animatort = Walls[i].GetComponent<Animator>();
            animatort.SetBool("On", true);        
            
            // BoxCollider2D 컴포넌트 추가
            BoxCollider2D boxCollider = Walls[i].AddComponent<BoxCollider2D>();

            // BoxCollider2D 사이즈 설정
            Vector2 colliderSize = wallSize; // 원하는 사이즈로 변경
            boxCollider.size = colliderSize;
        }
    }
    private IEnumerator SpawnMob()
    {
        for (int i = 0; i < mobList.Count; i++)
        {
            // i번째 몬스터를 i번째 spawnPoint에 생성
            GameObject clone = Instantiate(mobList[i], spawnPoint.position , Quaternion.identity) as GameObject;
            clone.transform.SetParent(this.transform);
            SpawnEnemyHPSlider(clone);
            yield return new WaitForSeconds(spawnDelay);
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
