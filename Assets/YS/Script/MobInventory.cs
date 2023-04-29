using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInventory : MonoBehaviour
{
  public GameObject inventory;  // �κ��丮 ����
  private bool isInven = false;

  void Update()
  {
    // ESCŰ�� ������ �κ��丮�� ����
    if (isInven == true && Input.GetKeyDown(KeyCode.Escape))
    {
      inventory.SetActive(false);
      isInven = false;
    }
  }

  void OnMouseDown()
  {
    // ���콺�� Ŭ���ߴµ� �±װ� DeadMob �̸� �κ��丮 ����
    if (isInven == false && gameObject.tag == "DeadMob")
      {
        inventory.SetActive(true);
        isInven = true;
      }
  }

  /*
  void OnTriggerStay2D(Collider2D other)
  {
    // �浹�� ������Ʈ�� "DeadMob" �±׸� ���� ����̰�, FŰ�� ���� ��
    if (other.CompareTag("DeadMob") && Input.GetKeyDown(KeyCode.F))
    {
      Debug.Log("col");
      if (isInven == false)
      {
        inventory.SetActive(true);
        isInven = true;
      }
    }
  }
  */
}