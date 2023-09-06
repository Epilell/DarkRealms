using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class BossPattern : MonoBehaviour
{
    Rigidbody2D rigid;
    private Animator animator;
    SpriteRenderer spriteRenderer;
    private Transform player;
    public BossStat bossStat;
    private BossHP bossHP;
    private Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform
    [Header("패턴간 딜레이")]
    [SerializeField]
    private float BreathDelay = 7f;
    [SerializeField]
    private float TailDelay = 5f;
    [SerializeField]
    private float PopDelay = 10f;
    [SerializeField]
    private float CrossDelay = 10f;
    [SerializeField]
    private float SpawnDelay = 5f;

    private float _spawnDelay = 0.5f;

    [Header("패턴 오브젝트")]
    //Breath Pattren
    [SerializeField]
    private GameObject Breath;
    [SerializeField]
    private GameObject BreathPoint;
    //Tail Pattren
    [SerializeField]
    private GameObject TailAtk;
    //PopOutAtk
    [SerializeField]
    private GameObject PopOutAtk;
    //SpawnPattren
    [SerializeField]
    private GameObject mob;
    [SerializeField]
    private GameObject mobHPSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹

    [SerializeField]
    //어떤 몬스터를 소환할지 리스트
    private List<GameObject> mobList;  //List<자료형> 변수명 = new List<자료형>();
    public List<GameObject> MobList => mobList;
    public List<Transform> spawnPoint;
    public int MaxSpawn = 3;
    private int CurrentSpawn = 0;
    [SerializeField]
    private float detectionRange = 20;
    private bool _onAttackRange = false;


    private GameObject mainCamera;
    private Camera camera;
    private Transform cameraTransform;

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            if (!_onAttackRange)
            {
                StartCoroutine("BossPattrenStart");
                _onAttackRange = true;
            }
        }
        else
        {
            StopCoroutine("BossPattrenStart");
            _onAttackRange = false;
        }
    }
    void Start()
    {
        // player를 찾아서 설정합니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        /*
        mainCamera = GameObject.Find("Main Camera");
        camera= mainCamera.GetComponent<Camera>();

        camera.orthographicSize = 10;

        cameraTransform = mainCamera.transform;

        cameraTransform.localPosition = new Vector3(0, 5, -10);*/
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossHP = GetComponent<BossHP>();
    }
    public void SetCanvas(Transform Canvas)
    {
        this.canvasTransform = Canvas;
    }

    private IEnumerator BossPattrenStart()
    {
        int pattrenChoicer = Random.Range(1, 5);
        while (bossHP.IsDie == false)
        {

            if (bossHP.CurrentHP < (bossHP.MaxHP * 0.5))//보스HP40프로 이하시 패턴 추가 및 딜레이 감소
            {
                //현재 적의 색상을 color변수에 저장
                Color color = spriteRenderer.color;
                color.b = 0.8f;
                color.g = 0.8f;
                spriteRenderer.color = color;
                //차후 이미지 교체도 가능
                if (pattrenChoicer > 4)
                {
                    yield return StartCoroutine("BreathPattern");
                    yield return new WaitForSeconds(BreathDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else if (pattrenChoicer > 3)
                {
                    yield return StartCoroutine("TailPattern");
                    yield return new WaitForSeconds(TailDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else if (pattrenChoicer > 2)
                {
                    yield return StartCoroutine("PopOutPattern");
                    yield return new WaitForSeconds(PopDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else if (pattrenChoicer > 1)
                {
                    //yield return StartCoroutine("CrossPattern()");
                    yield return new WaitForSeconds(CrossDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else
                {
                    yield return StartCoroutine("SpawnPattern");
                    yield return new WaitForSeconds(SpawnDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
            }

            else
            {
                if (pattrenChoicer > 4)
                {
                    yield return StartCoroutine("TailPattern");
                    yield return new WaitForSeconds(TailDelay + 2f);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else if (pattrenChoicer > 2)
                {
                    yield return StartCoroutine("PopOutPattern");
                    yield return new WaitForSeconds(PopDelay + 2f);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else
                {
                    yield return StartCoroutine("BreathPattern");
                    yield return new WaitForSeconds(BreathDelay + 4f);
                    pattrenChoicer = Random.Range(1, 5);
                }
            }
        }
    }
    private IEnumerator BreathPattern()//브레스 공격
    {
        //애니메이션 브레스공격모션으로 바꾸고
        GetComponent<Animator>().SetTrigger("Breath_Ready");
        GetComponent<Animator>().SetBool("Breath_Atk", true);
        yield return new WaitForSeconds(3.75f);
        //브레스 공격 소환
        GameObject _breath = Instantiate(Breath, BreathPoint.transform.position, Quaternion.identity);
        //SpriteRenderer sr = _breath.GetComponent<SpriteRenderer>();
        for (int i = -50; i <= 50; i++)
        {
            _breath.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i));
            yield return new WaitForSeconds(0.08f);
        }
        GetComponent<Animator>().SetBool("Breath_Atk", false);
        Destroy(_breath);
        GetComponent<Animator>().SetTrigger("Breath_Out");

    }
    private IEnumerator TailPattern()//바닥에서 튀어나오는 꼬리 공격
    {
        //애니메이션 꼬리공격모션으로 바꾸고
        GetComponent<Animator>().SetTrigger("TailAttack");
        //플레이어 위치에 꼬리 공격 3개 정도 소환(몇초뒤 Destroy걸어놓기)
        Instantiate(TailAtk, player.transform.position + Vector3.left, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(TailAtk, player.transform.position + Vector3.right, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(TailAtk, player.transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator PopOutPattern()//보스가 바닥에서 튀어나오는 공격
    {
        //애니메이션 튀어나오기공격모션으로 바꾸고
        GetComponent<Animator>().SetTrigger("Ready");
        yield return new WaitForSeconds(3f);
        //this.gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Stay", true);
        bossHP.CanDamage = false;
        //튀어나오기 공격 범위 표시 소환, 2초뒤 공격
        yield return new WaitForSeconds(2f);
        Instantiate(PopOutAtk, player.transform.position, Quaternion.identity);
        PopOutAtk.GetComponent<BossPopDamage>().SetStats(bossStat.BossDamageMiddle);
        //튀어나오기 소환 이후 Boss gameObject.SetActive(true)하기
        yield return new WaitForSeconds(4.2f);
        //this.gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("Stay", false);
        bossHP.CanDamage = true;
        GetComponent<Animator>().SetTrigger("ReturnPop");
        Debug.Log("4");
    }
    private IEnumerator CrossPattern()//보스가 직접 맵을 가로지르는 공격
    {
        //애니메이션 몸통공격모션으로 바꾸고
        //맵에 몸통공격 소환
        yield return new WaitForSeconds(3f);
    }
    private IEnumerator SpawnPattern()//잡몹 소환 패턴
    {
        //애니메이션 잡몹소환모션으로 바꾸고
        GetComponent<Animator>().SetTrigger("SpawnMob");
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < mobList.Count; i++)
        {
            // i번째 몬스터를 i번째 spawnPoint에 생성
            //Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity);
            GameObject clone = Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity) as GameObject;
            SpawnEnemyHPSlider(clone);
        }
    }





    protected virtual IEnumerator SpawnMob()
    {
        while (CurrentSpawn < MaxSpawn)
        {
            CurrentSpawn++;
            for (int i = 0; i < mobList.Count; i++)
            {
                // i번째 몬스터를 i번째 spawnPoint에 생성
                //Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity);
                GameObject clone = Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity) as GameObject;
                SpawnEnemyHPSlider(clone);
                clone.transform.SetParent(this.gameObject.transform);
                yield return new WaitForSeconds(_spawnDelay);
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
