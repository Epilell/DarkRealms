using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpbar, hpbar2; // 체력바
    public float maxHp;
    public float currentHp;
    public Text textObj, textObj2; // 체력 텍스트
    public Player player; // 플레이어 객체

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        // 체력바 초기화
        hpbar.value = (float)player.CurrentHp / (float)player.MaxHP;
        hpbar2.value = (float)player.CurrentHp / (float)player.MaxHP;
    }

    private void Update()
    {
        if (player != null)
        {
            maxHp = player.MaxHP;
            currentHp = player.CurrentHp;

            // 현재 체력이 최대 체력을 넘지 않게
            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }

            // 현재 체력이 0 아래로 내려가지 않게
            if (currentHp < 0)
            {
                currentHp = 0;
            }

            ChangeHP();
        }
        else
        {
            Debug.LogError("Player 스크립트 컴포넌트를 찾을 수 없습니다.");
        }
    }


    /*private void Start()
    {
        Debug.Log("스타트!");
        // 체력바 초기화
        GameObject playerObject = GameObject.Find("Player(Clone)");
        player = FindObjectOfType<PlayerSpawnSystem>().playerObject.GetComponent<Player>();
        Debug.Log("플레이어:" + player);
        hpbar.value = (float)currentHp / (float)maxHp;
        hpbar2.value = (float)currentHp / (float)maxHp;
        Debug.Log("체력:" + hpbar2.value);
    }

    private void Update()
    {
        maxHp = player.MaxHP;
        currentHp = player.CurrentHp;
        Debug.Log("현재 체력:" + currentHp);
        if (currentHp > maxHp) // 현재 체력이 최대 체력을 넘지 않게
        {
            currentHp = maxHp;
        }

        if (currentHp < 0) // 현재 체력이 0 아래로 내려가지 않게
        {
            currentHp = 0;
        }

        ChangeHP();
    }*/

    public void ChangeHP() // 체력바 갱신
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // 체력 갱신

        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar2.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // 체력 갱신
    }
}