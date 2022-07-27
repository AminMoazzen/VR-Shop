using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class ShopData
{
    public List<ShopItemData> items;

    [JsonConstructor]
    public ShopData(List<ShopItemData> items)
    {
        this.items = items;
    }

    public ShopData(string json)
    {
        var data = JsonConvert.DeserializeObject<ShopData>(json);
        items = data.items;
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}