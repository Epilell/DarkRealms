using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

public class ItemContactToInven : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 자식 오브젝트들을 확인합니다.
        foreach (Transform child in collision.transform)
        {
            // 자식 오브젝트에서 Inventory 컴포넌트를 가져옵니다.
            Inventory inventoryComponent = child.GetComponent<Inventory>();

            // Inventory 컴포넌트가 있다면 해당 게임 오브젝트를 사용합니다.
            if (inventoryComponent != null)
            {
                // Inventory 컴포넌트를 사용하는 게임 오브젝트에 대한 작업을 수행합니다.
                // 예를 들어, 해당 게임 오브젝트의 아이템을 가져오거나 조작할 수 있습니다.
                //inventoryComponent.Add(this.itemData);
            }
        }
    }
}
