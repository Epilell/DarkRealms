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
    [SerializeField]
    private GameObject BreathAlert;
    [SerializeField]
    private GameObject Breath;
    [SerializeField]
    private GameObject BreathPoint;
    [SerializeField]
    private GameObject TailAlert;
    [SerializeField]
    private GameObject TailAtk;
    [SerializeField]
    private GameObject PopOutAlert;
    [SerializeField]
    private GameObject PopOutAtk;
    [SerializeField]
    private float BreathDelay = 3f;
    [SerializeField]
    private float TailAtkDelay = 2f;
    [SerializeField]
    private float PopOutDelay = 2f;
    [SerializeField]
    private GameObject mob;
    [SerializeField]
    private GameObject spawnPoint;
    Transform OutPoint;
    //private BossHP bossHP;
    // Start is called before the first frame update
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
            }else if (pattrenChoicer > 3)
            {
                yield return StartCoroutine("TailPattern()");
            }
            else if (pattrenChoicer > 2)
            {
                yield return StartCoroutine("PopOutPattern()");
            }
            else if (pattrenChoicer > 1)
            {
                yield return StartCoroutine("CrossPattern()");
            }
            else
            {
                yield return StartCoroutine("SpawnPattern()");
            }*/
            yield return PopOutPattern();//test
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
        //���� ���� ���� ǥ�� ��ȯ(���ʵ� Destroy�ɾ����)
        Instantiate(TailAlert, player.transform.position + Vector3.left, Quaternion.identity);
        Instantiate(TailAlert, player.transform.position + Vector3.right, Quaternion.identity);
        Instantiate(TailAlert, player.transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(TailAtkDelay);
        //�÷��̾� ��ġ�� ���� ���� 3�� ���� ��ȯ(���ʵ� Destroy�ɾ����)
        Instantiate(TailAtk, player.transform.position + Vector3.left, Quaternion.identity);
        Instantiate(TailAtk, player.transform.position + Vector3.right, Quaternion.identity);
        Instantiate(TailAtk, player.transform.position + Vector3.up, Quaternion.identity);
    }
    private IEnumerator PopOutPattern()//������ �ٴڿ��� Ƣ����� ����
    {
        //�ִϸ��̼� Ƣ�������ݸ������ �ٲٰ�
        GetComponent<Animator>().SetTrigger("Ready");
        yield return new WaitForSeconds(1.2f);
        //this.gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Stay", true);
        //Ƣ����� ���� ���� ǥ�� ��ȯ(���ʵ� Destroy�ɾ����)
        Instantiate(PopOutAlert, player.position, Quaternion.identity);
        OutPoint = PopOutAlert.transform;
        Debug.Log("1");
        //�÷��̾� ��ġ�� Ƣ����� ���� ��ȯ
        yield return new WaitForSeconds(2f);
        Debug.Log("2");
        Instantiate(PopOutAtk, PopOutAlert.transform.position, Quaternion.identity);
        PopOutAtk.GetComponent<BossPopDamage>().SetStats(bossStat.BossDamageB);
        //Ƣ����� ��ȯ ���� Boss gameObject.SetActive(true)�ϱ�
        Debug.Log("3");
        yield return new WaitForSeconds(PopOutDelay);
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
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            Instantiate(mob, spawnPoint.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(3f);
    }
}
