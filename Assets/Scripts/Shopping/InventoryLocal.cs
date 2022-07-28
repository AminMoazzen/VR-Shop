using System.Collections;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Inventory", menuName = "VR Shop / Local Inventory")]
public class InventoryLocal : Inventory
{
    public override IEnumerator Fetch()
    {
        var inventoryJsonFile = GetSavePath();
        if (File.Exists(inventoryJsonFile))
        {
            var readRequest = File.ReadAllTextAsync(inventoryJsonFile);
            yield return readRequest;
            if (readRequest.IsCanceled || readRequest.IsFaulted)
            {
                data = new InventoryData();
            }
            else
            {
                data = new InventoryData(readRequest.Result);
            }
        }
        else
        {
            data = new InventoryData();
        }
    }

    public override void ChargeWallet(int amount)
    {
        data.Wallet += amount;
        SaveToFile();
        onWalletChanged.Invoke(data.Wallet);
    }

    public override bool PurchaseItem(ShopItemData item)
    {
        if (data.Wallet < item.price)
            return false;

        data.Wallet -= item.price;
        InventoryItemData invItem = new InventoryItemData(item.id);
        data.items.Add(invItem);
        SaveToFile();

        onWalletChanged.Invoke(data.Wallet);
        onItemBought.Invoke(invItem);

        return true;
    }

    public override bool HasItem(ShopItemData item)
    {
        return data.items.Find((x) => x.id == item.id) != null;
    }

    private void SaveToFile()
    {
        string invJson = JsonUtility.ToJson(data);
        var inventoryJsonFile = GetSavePath();
        File.WriteAllText(inventoryJsonFile, invJson);
    }

    private string GetSavePath()
    {
        return Application.persistentDataPath + "/inventory.json";
    }

    public override void UpdateItem(InventoryItemData itemData)
    {
        var item = data.items.Find((x) => x.id == itemData.id);
        if (item != null)
        {
            item.position = itemData.position;
            item.rotation = itemData.rotation;
            SaveToFile();
        }
    }
}