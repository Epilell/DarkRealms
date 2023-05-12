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
    private BossStat bossStat;
    //Breath Pattren
    [SerializeField]
    private GameObject BreathAlert;
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
    //Delay
    [SerializeField]
    private float BreathDelay = 3f;
    //SpawnPattren
    [SerializeField]
    private GameObject mob;
    [SerializeField]
    private GameObject mobHPSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform

    [SerializeField]
    //� ���͸� ��ȯ���� ����Ʈ
    private List<GameObject> mobList;  //List<�ڷ���> ������ = new List<�ڷ���>();
    public List<GameObject> MobList => mobList;
    public List<Transform> spawnPoint;
    public float spawnDelay = 0.5f;
    public int MaxSpawn = 3;
    private int CurrentSpawn = 1;

    void Start()
    {
        // player�� ã�Ƽ� �����մϴ�.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("BossPattrenStart");
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossStat = GetComponent<BossStat>();
    }
    private void FixedUpdate()
    {

    }
    private IEnumerator BossPattrenStart()
    {
        while (true)
        {
            /*
            int pattrenChoicer = Random.Range(1, 5);
            if (pattrenChoicer >4)
            {
                yield return StartCoroutine("BreathPattern()");
                yield return new WaitForSeconds(10f);
            }
            else if (pattrenChoicer > 3)
            {
                yield return StartCoroutine("TailPattern()");
                yield return StartCoroutine("TailPattern()");
                yield return StartCoroutine("TailPattern()");
                yield return new WaitForSeconds(5f);
            }
            else if (pattrenChoicer > 2)
            {
                yield return StartCoroutine("PopOutPattern()");
                yield return StartCoroutine("PopOutPattern()");
                yield return StartCoroutine("PopOutPattern()");
                yield return new WaitForSeconds(8f);
            }
            else if (pattrenChoicer > 1)
            {
                yield return StartCoroutine("CrossPattern()");
                yield return new WaitForSeconds(10f);
            }
            else
            {
                yield return StartCoroutine("SpawnPattern()");
                yield return new WaitForSeconds(20f);
            }*/
            yield return TailPattern();//test
            yield return new WaitForSeconds(10f);
        }
    }
    private IEnumerator BreathPattern()//�극�� ����
    {
        //�ִϸ��̼� �극�����ݸ������ �ٲٰ�
        GetComponent<Animator>().SetTrigger("BreathAttack");
        //�극�� ���ݹ��� ǥ�� ��ȯ(���ʵ� Destroy�ɾ����)
        Instantiate(BreathAlert, BreathPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(BreathDelay);//�극�� ������ ���� ��� ��
        //���� ���鿡 �극�� ��ȯ(���ʵ� Destroy�ɾ����)
        Instantiate(Breath, BreathPoint.transform.position, Quaternion.identity);
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
        yield return new WaitForSeconds(1.2f);
        //this.gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Stay", true);
        //Ƣ����� ���� ���� ǥ�� ��ȯ, 2�ʵ� ����
        yield return new WaitForSeconds(2f);
        Instantiate(PopOutAtk, player.transform.position, Quaternion.identity);
        PopOutAtk.GetComponent<BossPopDamage>().SetStats(bossStat.BossDamageB);
        //Ƣ����� ��ȯ ���� Boss gameObject.SetActive(true)�ϱ�
        yield return new WaitForSeconds(4.2f);
        //this.gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("Stay", false);
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
                yield return new WaitForSeconds(spawnDelay);
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
