using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Inventory : ScriptableObject
{
    public InventoryData data;
    public UnityEvent<int> onWalletChanged;
    public UnityEvent<InventoryItemData> onItemBought;

    public abstract IEnumerator Fetch();

    public abstract void ChargeWallet(int amount);

    public abstract bool PurchaseItem(ShopItemData item);

    public abstract bool HasItem(ShopItemData item);

    public abstract void UpdateItem(InventoryItemData itemData);
}