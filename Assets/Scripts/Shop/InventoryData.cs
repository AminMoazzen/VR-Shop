using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int Wallet;
    public ShopItemData[] items;

    public InventoryData(string json)
    {
        var data = JsonUtility.FromJson<InventoryData>(json);
        Wallet = data.Wallet;
        items = data.items;
    }

    public InventoryData(int Wallet, ShopItemData[] items)
    {
        this.Wallet = Wallet;
        this.items = items;
    }

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }
}