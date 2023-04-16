using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상 클래스
public abstract class ItemEffect : ScriptableObject
{
    public abstract bool ExecuteRole();
}