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
        // player를 찾아서 설정합니다.
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
    private IEnumerator BreathPattern()//브레스 공격
    {
        //애니메이션 브레스공격모션으로 바꾸고
        GetComponent<Animator>().SetTrigger("BreathAttack");
        //브레스 공격범위 표시 소환(몇초뒤 Destroy걸어놓기)
        Instantiate(BreathAlert, BreathPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(BreathDelay);//브레스 딜레이 동안 대기 후
        //보스 정면에 브레스 소환(몇초뒤 Destroy걸어놓기)
        Instantiate(Breath, BreathPoint.transform.position, Quaternion.identity);
    }
    private IEnumerator TailPattern()//바닥에서 튀어나오는 꼬리 공격
    {
        //애니메이션 꼬리공격모션으로 바꾸고
        GetComponent<Animator>().SetTrigger("TailAttack");
        //꼬리 공격 범위 표시 소환(몇초뒤 Destroy걸어놓기)
        Instantiate(TailAlert, player.transform.position + Vector3.left, Quaternion.identity);
        Instantiate(TailAlert, player.transform.position + Vector3.right, Quaternion.identity);
        Instantiate(TailAlert, player.transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(TailAtkDelay);
        //플레이어 위치에 꼬리 공격 3개 정도 소환(몇초뒤 Destroy걸어놓기)
        Instantiate(TailAtk, player.transform.position + Vector3.left, Quaternion.identity);
        Instantiate(TailAtk, player.transform.position + Vector3.right, Quaternion.identity);
        Instantiate(TailAtk, player.transform.position + Vector3.up, Quaternion.identity);
    }
    private IEnumerator PopOutPattern()//보스가 바닥에서 튀어나오는 공격
    {
        //애니메이션 튀어나오기공격모션으로 바꾸고
        GetComponent<Animator>().SetTrigger("Ready");
        yield return new WaitForSeconds(1.2f);
        //this.gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Stay", true);
        //튀어나오기 공격 범위 표시 소환(몇초뒤 Destroy걸어놓기)
        Instantiate(PopOutAlert, player.position, Quaternion.identity);
        OutPoint = PopOutAlert.transform;
        Debug.Log("1");
        //플레이어 위치에 튀어나오기 공격 소환
        yield return new WaitForSeconds(2f);
        Debug.Log("2");
        Instantiate(PopOutAtk, PopOutAlert.transform.position, Quaternion.identity);
        PopOutAtk.GetComponent<BossPopDamage>().SetStats(bossStat.BossDamageB);
        //튀어나오기 소환 이후 Boss gameObject.SetActive(true)하기
        Debug.Log("3");
        yield return new WaitForSeconds(PopOutDelay);
        //this.gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("Stay", false);
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
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            Instantiate(mob, spawnPoint.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(3f);
    }
}
