using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> MapCover;
    private Image coverImage;
    private void Start()
    {
        StartCoroutine(setup());
    }
    public void MapCoverOpen(int CoverNum)
    {
        coverImage = MapCover[CoverNum].GetComponent<Image>();
        Color color = coverImage.color;
        color.a = 0f;
        coverImage.color = color;
    }
    private IEnumerator setup()
    {
        yield return new WaitForSeconds(0.001f);
        //this.gameObject.SetActive(false);
    }
}
