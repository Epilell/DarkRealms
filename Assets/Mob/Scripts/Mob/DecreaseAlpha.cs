using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseAlpha : MonoBehaviour
{
    private float duration=3f;
    private float alpha;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    public void SetUp(float Duration)
    {
        duration = Duration;
    }
    private void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        alpha = 0.6f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Color color = sr.color;
        color.a = color.a-0.01f;
        sr.color = color;
    }
}
