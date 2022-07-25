using System.Collections;
using UnityEngine;

public class InventoryLocal : Inventory
{
    [SerializeField] private string jsonFile;

    public override IEnumerator Fetch()
    {
        var request = Resources.LoadAsync(jsonFile, typeof(TextAsset));
        yield return request;

        var txtAsset = (TextAsset)request.asset;
        string jsonContent = txtAsset.text;

        base.data = new InventoryData(jsonContent);
    }

    public override void ChargeWallet(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override void StoreItem(ShopItemData item)
    {
        throw new System.NotImplementedException();
    }

    public override bool HasItem(ShopItemData item)
    {
        throw new System.NotImplementedException();
    }
}