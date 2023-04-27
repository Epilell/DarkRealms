using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;  // ItemDB 클래스의 싱글톤 인스턴스
    public List<Item> itemDB = new List<Item>();  // 아이템 리스트
    public GameObject fieldItemPrefab;  // 아이템 오브젝트 프리팹
    public Vector3[] pos;  // 생성되는 위치의 배열

    private void Awake()
    {
        instance = this;  // // 싱글톤 인스턴스를 이 객체로 설정
    }

    private void Start()
    {
        // 아이템 5개 무작위 위치로 생성
        for (int i = 0; i < 5; i++)
        {
            // 아이템 오브젝트 생성
            GameObject gameObject = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            // FieldItem 컴포넌트의 아이템을 아이템 데이터베이스에서 무작위로 선택한 아이템으로 설정
            gameObject.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }
}