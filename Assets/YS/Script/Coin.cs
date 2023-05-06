using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    static int sumCoin = 0;

    public void SetCoin(int val)
    {
        // Debug.Log(val);
        sumCoin += val;
        // Debug.Log(sumCoin);
    }

    public int GetCoin()
    {
        // Debug.Log(sumCoin);
        return sumCoin;
    }
}