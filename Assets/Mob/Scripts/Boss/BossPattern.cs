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
    private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    [Header("���ϰ� ������")]
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

    [Header("���� ������Ʈ")]
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
    private GameObject mobHPSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������

    [SerializeField]
    //� ���͸� ��ȯ���� ����Ʈ
    private List<GameObject> mobList;  //List<�ڷ���> ������ = new List<�ڷ���>();
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
        // player�� ã�Ƽ� �����մϴ�.
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

            if (bossHP.CurrentHP < (bossHP.MaxHP * 0.5))//����HP40���� ���Ͻ� ���� �߰� �� ������ ����
            {
                //���� ���� ������ color������ ����
                Color color = spriteRenderer.color;
                color.b = 0.8f;
                color.g = 0.8f;
                spriteRenderer.color = color;
                //���� �̹��� ��ü�� ����
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
    private IEnumerator BreathPattern()//�극�� ����
    {
        //�ִϸ��̼� �극�����ݸ������ �ٲٰ�
        GetComponent<Animator>().SetTrigger("Breath_Ready");
        GetComponent<Animator>().SetBool("Breath_Atk", true);
        yield return new WaitForSeconds(3.75f);
        //�극�� ���� ��ȯ
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
    private IEnumerator TailPattern()//�ٴڿ��� Ƣ����� ���� ����
    {
        //�ִϸ��̼� �������ݸ������ �ٲٰ�
        GetComponent<Animator>().SetTrigger("TailAttack");
        //�÷��̾� ��ġ�� ���� ���� 3�� ���� ��ȯ(���ʵ� Destroy�ɾ����)
        Instantiate(TailAtk, player.transform.position + Vector3.left, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(TailAtk, player.transform.position + Vector3.right, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(TailAtk, player.transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator PopOutPattern()//������ �ٴڿ��� Ƣ����� ����
    {
        //�ִϸ��̼� Ƣ�������ݸ������ �ٲٰ�
        GetComponent<Animator>().SetTrigger("Ready");
        yield return new WaitForSeconds(3f);
        //this.gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Stay", true);
        bossHP.CanDamage = false;
        //Ƣ����� ���� ���� ǥ�� ��ȯ, 2�ʵ� ����
        yield return new WaitForSeconds(2f);
        Instantiate(PopOutAtk, player.transform.position, Quaternion.identity);
        PopOutAtk.GetComponent<BossPopDamage>().SetStats(bossStat.BossDamageMiddle);
        //Ƣ����� ��ȯ ���� Boss gameObject.SetActive(true)�ϱ�
        yield return new WaitForSeconds(4.2f);
        //this.gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("Stay", false);
        bossHP.CanDamage = true;
        GetComponent<Animator>().SetTrigger("ReturnPop");
        Debug.Log("4");
    }
    private IEnumerator CrossPattern()//������ ���� ���� ���������� ����
    {
        //�ִϸ��̼� ������ݸ������ �ٲٰ�
        //�ʿ� ������� ��ȯ
        yield return new WaitForSeconds(3f);
    }
    private IEnumerator SpawnPattern()//��� ��ȯ ����
    {
        //�ִϸ��̼� �����ȯ������� �ٲٰ�
        GetComponent<Animator>().SetTrigger("SpawnMob");
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < mobList.Count; i++)
        {
            // i��° ���͸� i��° spawnPoint�� ����
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
                // i��° ���͸� i��° spawnPoint�� ����
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
