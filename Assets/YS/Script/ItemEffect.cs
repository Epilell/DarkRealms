using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߻� Ŭ����
public abstract class ItemEffect : ScriptableObject  // ������ ȿ���� ��Ÿ���� �߻� Ŭ����
{
    public abstract bool ExecuteRole();  // �Ļ� Ŭ�������� ����, ������ ȿ�� ����
}