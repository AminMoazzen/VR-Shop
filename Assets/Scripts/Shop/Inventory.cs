using System.Collections;

public abstract class Inventory
{
    public InventoryData data;

    public abstract IEnumerator Fetch();

    public abstract void ChargeWallet(int amount);

    public abstract void StoreItem(ShopItemData item);

    public abstract bool HasItem(ShopItemData item);
}