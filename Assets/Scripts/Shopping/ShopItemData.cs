using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class ShopItemData
{
    public int id;
    public string name;
    public string prefabAddress;
    public string thumbnailAddress;
    public string category;
    public int price;
    public Dictionary<string, string> metadata;

    [JsonConstructor]
    public ShopItemData(int id, string name, string prefabAddress, string thumbnailAddress, string category, int price, Dictionary<string, string> metadata)
    {
        this.id = id;
        this.name = name;
        this.prefabAddress = prefabAddress;
        this.thumbnailAddress = thumbnailAddress;
        this.category = category;
        this.price = price;
        this.metadata = metadata;
    }

    public ShopItemData(string json)
    {
        var data = JsonConvert.DeserializeObject<ShopItemData>(json);
        id = data.id;
        name = data.name;
        prefabAddress = data.prefabAddress;
        thumbnailAddress = data.thumbnailAddress;
        category = data.category;
        price = data.price;
        metadata = data.metadata;
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
}