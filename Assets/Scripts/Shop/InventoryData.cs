using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public int Wallet;
    public List<InventoryItemData> items;

    public InventoryData()
    {
        Wallet = 0;
        items = new List<InventoryItemData>();
    }

    public InventoryData(string json)
    {
        var data = JsonConvert.DeserializeObject<InventoryData>(json);
        Wallet = data.Wallet;
        items = data.items;
    }

    [JsonConstructor]
    public InventoryData(int Wallet, List<InventoryItemData> items)
    {
        this.Wallet = Wallet;
        this.items = items;
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}