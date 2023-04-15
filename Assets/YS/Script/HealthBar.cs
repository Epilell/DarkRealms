using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider hpbar;
    private float maxHp = 100;
    private float currentHp = 100;
    public Text textObj;

    private void Start()
    {
        hpbar.value = (float)currentHp / (float)maxHp;
    }

    private void Update()
    {
        // Å×½ºÆ®
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentHp > 0)
            {
                currentHp -= 10;
            }
            else
            {
                currentHp = 0;
            }
        }
        ChangeHP();
    }

    public void ChangeHP()
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString();
        // hpbar.value = (float)currentHp / (float)maxHp;
        hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 10);
    }

    public void DecreaseHp(float amount)
    {
        currentHp -= amount;
        if (currentHp < 0)
        {
            currentHp = 0;
        }
        hpbar.value = currentHp;
    }

    public void IncreaseHp(float amount)
    {
        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpbar.value = currentHp;
    }
}