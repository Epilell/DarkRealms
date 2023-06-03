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
    private int CurrentSpawn = 1;
    [SerializeField]
    private float detectionRange = 20;
    private bool _onAttackRange=false;
    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer< detectionRange)
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
        while (true)
        {
            if (bossHP.CurrentHP<(bossHP.MaxHP*0.4))//����HP40���� ���Ͻ� ���� �߰� �� ������ ����
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
                    yield return StartCoroutine("PopOutPattern");
                    yield return StartCoroutine("PopOutPattern");
                    yield return new WaitForSeconds(PopDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else if (pattrenChoicer > 1)
                {
                    yield return StartCoroutine("CrossPattern()");
                    yield return new WaitForSeconds(CrossDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else
                {
                    yield return StartCoroutine("SpawnPattern");
                    yield return new WaitForSeconds(3f);
                    pattrenChoicer = Random.Range(1, 5);
                }
            }
            
            else
            {
                if (pattrenChoicer > 4)
                {
                    yield return StartCoroutine("TailPattern");
                    yield return StartCoroutine("TailPattern");
                    yield return StartCoroutine("TailPattern");
                    yield return new WaitForSeconds(TailDelay);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else if (pattrenChoicer > 2)
                {
                    yield return StartCoroutine("PopOutPattern");
                    yield return StartCoroutine("PopOutPattern");
                    yield return StartCoroutine("PopOutPattern");
                    yield return new WaitForSeconds(7f);
                    pattrenChoicer = Random.Range(1, 5);
                }
                else
                {
                    yield return StartCoroutine("SpawnPattern");
                    yield return new WaitForSeconds(5f);
                    pattrenChoicer = Random.Range(1, 5);
                }
            }
        }
    }
    private IEnumerator BreathPattern()//�극�� ����
    {
        //�ִϸ��̼� �극�����ݸ������ �ٲٰ�
        GetComponent<Animator>().SetTrigger("BreathAttack");
        //�극�� ���� ��ȯ
        Instantiate(Breath, BreathPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
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
        yield return new WaitForSeconds(2f);
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
        yield return SpawnMob();
    }





    protected virtual IEnumerator SpawnMob()
    {
        while (true)
        {
            CurrentSpawn++;
            if (CurrentSpawn > MaxSpawn)
            {
                yield break;
            }
            for (int i = 0; i < mobList.Count; i++)
            {
                // i��° ���͸� i��° spawnPoint�� ����
                //Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity);
                GameObject clone = Instantiate(mobList[i], spawnPoint[i].position, Quaternion.identity) as GameObject;
                SpawnEnemyHPSlider(clone);
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
