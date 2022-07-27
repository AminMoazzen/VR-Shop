using System.Collections;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Inventory", menuName = "VR Shop / Local Inventory")]
public class InventoryLocal : Inventory
{
    [SerializeField] private string jsonFile;

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
    }

    public override bool PurchaseItem(ShopItemData item)
    {
        if (data.Wallet < item.price)
            return false;

        data.Wallet -= item.price;
        data.items.Add(item);
        SaveToFile();

        return true;
    }

    public override bool HasItem(ShopItemData item)
    {
        return data.items.Contains(item);
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
}