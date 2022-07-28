using System.Collections;
using UnityEngine;

public abstract class Shop : ScriptableObject
{
    public ShopData data;

    public abstract IEnumerator Fetch();

    public abstract ShopItemData FindItem(int id);
}