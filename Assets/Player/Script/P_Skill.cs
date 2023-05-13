using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class P_Skill : MonoBehaviour
{
    /*--------------------------------------------------<회피>-----------------------------------------------------------*/
    public float dodgeDistance = 3;     //회피 거리
    public float dodgeTime = 3;         //회피 쿨타임
    private float dodgeTimeCheck = 0;   //시간 체크용
    private bool canDodge;              //회피 가능여부 체크

    private Vector3 DirectionVec;
    private float mx, my;

    private Animator ani;

    //회피 방향 지정
    private void GetDir()
    {
        if (mx > 0) { DirectionVec.x = 1; }
        else if (mx < 0) { DirectionVec.x = -1; }
        else { DirectionVec.x = 0; }

        if ( my > 0) { DirectionVec.y = 1; }
        else if ( my < 0) { DirectionVec.y = -1; }
        else { DirectionVec.y = 0; }
    }

    //회피 시간 체크
    private void DodgeTimeCheck()
    {
        if (dodgeTimeCheck <= dodgeTime)
        {
            dodgeTimeCheck += Time.deltaTime;
        }
        else
        {
            canDodge = true;
        }
    }

    //회피 기능
    private void Dodge()
    {
        RaycastHit hit;

        GetDir();

        if (Physics.Raycast(transform.position, DirectionVec.normalized, out hit, dodgeDistance))
        {
            GameObject hitTarget = hit.collider.gameObject;
            transform.position = hit.point;
            ani.SetBool("IsDash", true);
        }
        else
        {
            transform.position += DirectionVec.normalized * dodgeDistance;
            ani.SetBool("IsDash", true);
        }

        dodgeTimeCheck = 0; canDodge = false;
    }

    public void EndDash()
    {
        ani.SetBool("IsDash", false);
    }

    /*--------------------------------------------------<화염병>-----------------------------------------------------------*/

    public GameObject throwObj;

    public float throwCooltime = 1;
    private float throwCurtime = 0;
    private float throwDistance = 5;
    private bool canThrow;

    public Vector3 MousePos, throwVec, MousePosition;

    //화염병 스킬 쿨타임 체크
    private void ThrowTimeCheck()
    {
        if (throwCurtime < throwCooltime)
        {
            throwCurtime += Time.deltaTime;
        }
        else
        {
            canThrow = true;
        }
    }

    //화염병 던지기
    private void ThrowMolotov()
    {
        GameObject Molotov;

        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;

        throwVec = (MousePos - transform.position).normalized;

        if (Vector3.Distance(MousePos, transform.position) > throwDistance)
        {

            Molotov = Instantiate(throwObj, transform.position, transform.rotation);
            Molotov.GetComponent<Molotov>().SetCourse(transform.position + (throwVec * throwDistance));
            canThrow = false; throwCurtime = 0;
        }
        else
        {
            Molotov = Instantiate(throwObj, transform.position, transform.rotation);
            Molotov.GetComponent<Molotov>().SetCourse(MousePos);
            canThrow = false; throwCurtime = 0;
        }
    }

    /*--------------------------------------------------<시즈모드>-----------------------------------------------------------*/
    private Player player;
    public bool siegeIsActive = false;

    public float siegeCooltime = 3f;
    private float siegeCurtime = 0f;
    private bool canSiege;
    
    private void SiegeTimeCheck()
    {
        if( siegeCurtime < siegeCooltime && siegeIsActive == false)
        {
            siegeCurtime += Time.deltaTime;
        }
        else if (siegeCurtime >= siegeCooltime)
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

    private void Awake()
    {
        player = this.GetComponent<Player>();
        ani = this.GetComponent<Animator>();
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        DodgeTimeCheck();
        if (Input.GetKeyDown(KeyCode.Space) && canDodge == true)
        {
            if(siegeIsActive == false)
            {
                Dodge();
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
