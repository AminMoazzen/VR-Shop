using UnityEngine;

[System.Serializable]
public class ShopData
{
    public string assetBundleAddress;
    public ShopItemData[] items;

    public ShopData(string json)
    {
        var data = JsonUtility.FromJson<ShopData>(json);
        assetBundleAddress = data.assetBundleAddress;
        items = data.items;
    }

    public ShopData(string assetBundleAddress, ShopItemData[] items)
    {
        this.assetBundleAddress = assetBundleAddress;
        this.items = items;
    }

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }
}