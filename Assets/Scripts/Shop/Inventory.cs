using System.Collections;
using UnityEngine;

public abstract class Inventory : ScriptableObject
{
    public InventoryData data;

    public abstract IEnumerator Fetch();

    public abstract void ChargeWallet(int amount);

    public abstract bool PurchaseItem(ShopItemData item);

    public abstract bool HasItem(ShopItemData item);
}