using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private void EndEffect()
    {
        Destroy(gameObject,0.4f);
    }
}
