using System.Collections;
using TMPro;
using UnityEngine;

public class Storefront : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ShopItem shopItemPrefab;
    [SerializeField] private Transform grid;
    [SerializeField] private TextMeshProUGUI wallet;

    private void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return inventory.Fetch();
        yield return shop.Fetch();
        yield return shop.LoadBundle();

        wallet.text = inventory.data.Wallet.ToString();

        var items = shop.data.items;

        for (int i = 0; i < items.Length; i++)
        {
            ShopItem item = Instantiate(shopItemPrefab, grid);
            item.Load(items[i], shop.assetBundle, inventory.HasItem(items[i]));
        }
    }
}