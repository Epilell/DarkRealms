using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;  // ItemDB 클래스의 싱글톤 인스턴스
    public List<Item> itemDB;  // 아이템 리스트
    public GameObject fieldItemPrefab;  // 아이템 오브젝트 프리팹
    Vector3[] pos;  // 생성되는 위치의 배열
    public int posNum; // pos 값의 수(== 드랍할 아이템의 수)

    private void Awake()
    {
        instance = this;  // 싱글톤 인스턴스를 이 객체로 설정
        pos = new Vector3[posNum];  // pos 초기화

        //
        PItem fieldItemPItem = fieldItemPrefab.GetComponent<PItem>();
        if (fieldItemPItem != null)
        {
            itemDB.AddRange(fieldItemPItem.items);
        }

        for (int i = 0; i < posNum; i++)  // pos에 posNum개의 랜덤한 벡터 값을 입력
        {
            pos[i] = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 5.0f), 0.0f);
        }

    }

    private void Start()
    {
        // 아이템 n개 무작위 위치로 생성
        for (int i = 0; i < pos.Length; i++)
        {
            // 아이템 오브젝트 생성
            GameObject gameObject = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            // FieldItem 컴포넌트의 아이템을 아이템 데이터베이스에서 무작위로 선택한 아이템으로 설정
            gameObject.GetComponent<PItem>().SetItem(itemDB[Random.Range(0, itemDB.Count)]);
        }
    }
}