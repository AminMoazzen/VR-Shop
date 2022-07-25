using System.Collections;
using UnityEngine;

public abstract class Shop : ScriptableObject
{
    public ShopData data;
    public AssetBundle assetBundle;

    public abstract IEnumerator Fetch();

    public abstract IEnumerator LoadBundle();

    public abstract bool Purchase(string itemId);
}