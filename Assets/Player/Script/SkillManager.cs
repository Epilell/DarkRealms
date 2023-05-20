using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager : MonoBehaviour
{
    private Player player;
    private Animator ani;
    private Rigidbody2D rb;

    private Vector3 mousePos, mouseVec;

    private void GetMousePos()
    {
        Vector3 calVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        calVec.z = 0;
        mousePos = calVec;
    }

    private void GetMouseVec()
    {
        mouseVec = (mousePos - transform.position).normalized;
    }

    /*--------------------------------------------------<ȸ��>-----------------------------------------------------------*/
    public DodgeData dodgedata;

    private float dodgeTimeCheck = 0;   //�ð� üũ��
    public bool canDodge;              //ȸ�� ���ɿ��� üũ

    private Vector3 dirVec;
    private float mx, my;

    //ȸ�� ����
    private void getDir()
    {
        if (mx > 0) { dirVec.x = 1; }
        else if (mx < 0) { dirVec.x = -1; }
        else { dirVec.x = 0; }

        if (my > 0) { dirVec.y = 1; }
        else if (my < 0) { dirVec.y = -1; }
        else { dirVec.y = 0; }
    }

    //ȸ�� �ð� üũ
    private void DodgeTimeCheck()
    {
        if (dodgeTimeCheck <= dodgedata.upgradeList[dodgedata.upgradeNum].coolTime)
        {
            dodgeTimeCheck += Time.deltaTime;
        }
        else
        {
            canDodge = true;
        }
    }

    private IEnumerator Dodge()
    {
        getDir();
        rb.AddForce(dirVec * 20f, ForceMode2D.Impulse);
        ani.SetBool("IsDash", true);

        yield return new WaitForSeconds(1/6f);

        rb.velocity = Vector3.zero;
        dodgeTimeCheck = 0; canDodge = false;
    }

    /*--------------------------------------------------<ȭ����>-----------------------------------------------------------*/
    public MolotovData molotovdata;

    public bool canThrow;
    private float throwCurtime = 0;

    //ȭ���� ��ų ��Ÿ�� üũ
    private void ThrowTimeCheck()
    {
        if (throwCurtime < molotovdata.upgradeList[molotovdata.upgradeNum].coolTime)
        {
            throwCurtime += Time.deltaTime;
        }
        else
        {
            canThrow = true;
        }
    }

    //ȭ���� ������
    private void ThrowMolotov()
    {
        GameObject Molotov;

        if (Vector3.Distance(mousePos, transform.position) > molotovdata.upgradeList[molotovdata.upgradeNum].throwDistance)
        {

            Molotov = Instantiate(molotovdata.Molotov, transform.position, transform.rotation);
            Molotov.GetComponent<ThrowMolotov>().
                SetCourse(transform.position + (mouseVec * molotovdata.upgradeList[molotovdata.upgradeNum].throwDistance));
            canThrow = false; throwCurtime = 0;
        }
        else
        {
            Molotov = Instantiate(molotovdata.Molotov, transform.position, transform.rotation);
            Molotov.GetComponent<ThrowMolotov>().SetCourse(mousePos);
            canThrow = false; throwCurtime = 0;
        }
    }

    /*--------------------------------------------------<������>-----------------------------------------------------------*/
    public SiegeModeData siegemodedata;

    public bool canSiege;
    public bool siegeIsActive = false;
    private float siegeCurtime = 0f;
    
    
    private void SiegeTimeCheck()
    {
        if( siegeCurtime < siegemodedata.upgradeList[siegemodedata.upgradeNum].coolTime && siegeIsActive == false)
        {
            siegeCurtime += Time.deltaTime;
        }
        else if (siegeCurtime >= siegemodedata.upgradeList[siegemodedata.upgradeNum].coolTime)
        {
            canSiege = true;
        }
    }

    private void SiegeMode()
    {
        if (siegeIsActive)
        {
            player.ChangeArmorReduction(0f);
            player.ChangeSpeedReduction(0f);
            siegeIsActive = false;
            canSiege = false;
        }
        else
        {
            player.ChangeArmorReduction(0.7f);
            player.ChangeSpeedReduction(1f);
            siegeIsActive = true;
            siegeCurtime = 0f;
        }
    }

    /*--------------------------------------------------<ȸ�ǻ��>-----------------------------------------------------------*/
    public EvdshotData evdshotdata;

    public bool canEvdshot;
    private float evdshotCurtime = 0f;

    private IEnumerator Evdshot()
    {
        rb.AddForce(mouseVec * -20f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        canEvdshot = false; evdshotCurtime = 0f;
    }

    private void EvdshotTimeCheck()
    {
        if (evdshotCurtime < evdshotdata.upgradeList[evdshotdata.upgradeNum].coolTime)
        {
            evdshotCurtime += Time.deltaTime;
        }
        else if (evdshotCurtime >= evdshotdata.upgradeList[evdshotdata.upgradeNum].coolTime)
        {
            canEvdshot = true;
        }
    }

    /*--------------------------------------------------<����>-----------------------------------------------------------*/

    private void Awake()
    {
        player = this.GetComponent<Player>();
        ani = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        GetMousePos();
        GetMouseVec();

        DodgeTimeCheck();
        if (Input.GetKeyDown(KeyCode.Space) && canDodge == true )
        {
            if(siegeIsActive == false)
            {
                StartCoroutine(Dodge());
            }
        }

        EvdshotTimeCheck();
        if (Input.GetMouseButtonDown(1) && canEvdshot)
        {
            if(siegeIsActive == false)
            {
                StartCoroutine(Evdshot());
            }
        }
        ThrowTimeCheck();
        if (Input.GetKeyDown(KeyCode.Q) && canThrow == true)
        {
            ThrowMolotov();
        }

        SiegeTimeCheck();
        if(Input.GetKeyDown(KeyCode.E) && canSiege == true)
        {
            SiegeMode();
        }
    }
}
