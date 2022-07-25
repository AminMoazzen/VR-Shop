using UnityEngine;

[System.Serializable]
public class ShopItemData
{
    public int id;
    public string name;
    public string assetName;
    public string iconName;
    public string description;
    public int price;

    public ShopItemData(int id, string name, string assetName, string iconName, string description, int price)
    {
        this.id = id;
        this.name = name;
        this.assetName = assetName;
        this.iconName = iconName;
        this.description = description;
        this.price = price;
    }

    public ShopItemData(string json)
    {
        JsonUtility.FromJson<ShopItem>(json);
    }

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }
}