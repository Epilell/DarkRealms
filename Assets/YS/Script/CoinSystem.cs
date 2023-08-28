using UnityEngine;
using UnityEngine.UI;
using Rito.InventorySystem;

public class CoinSystem : MonoBehaviour
{
    public Inventory inventory;
    public Text coinValue;

    void Update()
    {
        int totalCoin = 0;

        if (inventory != null && inventory._Items != null)
        {
            for (int i = 0; i < inventory._Items.Length; i++)
            {
                if (inventory._Items[i] != null && inventory._Items[i].Data != null && inventory._Items[i].Data.Name == "Coin")
                {
                    totalCoin += inventory.GetCurrentAmount(i);
                }
            }
        }

        coinValue.text = totalCoin.ToString();
    }
}