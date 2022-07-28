using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;

    [JsonConstructor]
    public InventoryItemData(int id)
    {
        this.id = id;
        this.position = Vector3.zero;
        this.rotation = Quaternion.identity;
    }

    public InventoryItemData(string json)
    {
        var data = JsonConvert.DeserializeObject<ShopItemData>(json);
        id = data.id;
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}