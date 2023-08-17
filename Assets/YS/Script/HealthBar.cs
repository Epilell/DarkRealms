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

    public void ChangeHP() // 체력바 갱신
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // 체력 갱신
        // hpbar.value = (float)currentHp / (float)maxHp; ← 시간 정지 시에도 작동함

        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString(); // 체력 텍스트 갱신
        hpbar2.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // 체력 갱신
    }
}