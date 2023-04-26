using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상 클래스
public abstract class ItemEffect : ScriptableObject  // 아이템 효과를 나타내는 추상 클래스
{
    public abstract bool ExecuteRole();  // 파생 클래스에서 구현, 아이템 효과 실행
}