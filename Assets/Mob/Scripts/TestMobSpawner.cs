using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestMobSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject mobHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    //public GameObject Mob;
    private MobStat mobStat;
    private float HPbarcorrection;

    [SerializeField]
    private GameObject Evileye;
    [SerializeField]
    private GameObject Ork;
    [SerializeField]
    private GameObject Goblin;
    [SerializeField]
    private GameObject Slime;
    [SerializeField]
    private GameObject Dopple;

    [SerializeField]
    private GameObject[] ButtonUIs;

    bool spawn_true;

    private void Awake()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }
    private void Start()
    {
        spawn_true = false;
        StartCoroutine(timeStop());
    }
    private IEnumerator timeStop()
    {
        yield return (2f);
        for (int i = 0; i < 5; i++)
        {
            ButtonUIs[i].SetActive(true);
        }
        Time.timeScale = 0f;
    }
    private IEnumerator timeGo()
    {
        yield return (0.1f); for (int i = 0; i < 5; i++)
        {
            ButtonUIs[i].SetActive(false);
        }
        Time.timeScale = 1f;
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (spawn_true == false)
            {
                //Debug.Log("Nope");
                GameObject mob = Instantiate(Mob, transform.position, transform.rotation);
                SpawnEnemyHPSlider(mob);
                //���ϰ�� �Ա�����
                spawn_true = true;
            }
        }
    }*/
    private void Spawnmob(GameObject mob)
    {

        mobStat = mob.GetComponent<MobStat>();
        HPbarcorrection = mobStat.HPbar_correction;
        //Debug.Log("Nope");
        GameObject monster = Instantiate(mob, transform.position, transform.rotation);
        SpawnEnemyHPSlider(monster);
        //���ϰ�� �Ա�����
        spawn_true = true;

    }
    public void EvileyeButton()
    {
        StartCoroutine(timeGo());
        Spawnmob(Evileye);
    }
    public void OrkButton()
    {
        StartCoroutine(timeGo());
        Spawnmob(Ork);
    }
    public void GoblinButton()
    {
        StartCoroutine(timeGo());
        Spawnmob(Goblin);
    }
    public void SlimeButton()
    {
        StartCoroutine(timeGo());
        Spawnmob(Slime);
    }
    public void DoppleButton()
    {
        StartCoroutine(timeGo());
        Spawnmob(Dopple);
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
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform, HPbarcorrection);
        //Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<MobHPViewer>().Setup(enemy.GetComponent<MobHP>());
    }
}
