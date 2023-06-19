using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropItemTooltips : MonoBehaviour
{
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        Destroy(this.gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTextAlpha();
    }
    private IEnumerator ChangeTextAlpha()
    {
        Color color = text.color;
        color.a = 0.9f;
        text.color = color;
        yield return new WaitForSeconds(0.1f);
    }
}
