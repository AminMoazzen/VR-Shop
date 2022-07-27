using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int Wallet;
    public List<ShopItemData> items;

    public InventoryData()
    {
        Wallet = 0;
        items = new List<ShopItemData>();
    }

    public InventoryData(string json)
    {
        var data = JsonUtility.FromJson<InventoryData>(json);
        Wallet = data.Wallet;
        items = data.items;
    }

    public InventoryData(int Wallet, List<ShopItemData> items)
    {
        this.Wallet = Wallet;
        this.items = items;
    }

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }
}