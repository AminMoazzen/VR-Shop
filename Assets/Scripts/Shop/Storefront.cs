using Nouranium;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Storefront : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ShopItem shopItemCard;
    [SerializeField] private Transform grid;
    [SerializeField] private StorefrontCategoryTab categoryTab;
    [SerializeField] private Transform categoryTabBar;
    [SerializeField] private TextMeshProUGUI wallet;
    [SerializeField] private MessageCategoryTab[] selectCategoryOn;
    [SerializeField] private MessageInt[] updateWalletOn;

    private Dictionary<string, List<ShopItemData>> _itemsByCategory;
    private List<ShopItem> _itemCards;
    private StorefrontCategoryTab _activeCategory;

    private void Awake()
    {
        foreach (var msg in selectCategoryOn)
        {
            msg.StartListening(ShowCategory);
        }

        foreach (var msg in updateWalletOn)
        {
            msg.StartListening(UpdateWalletText);
        }
    }

    private void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return inventory.Fetch();
        yield return shop.Fetch();

        UpdateWalletText(inventory.data.Wallet);

        var items = shop.data.items;

        _itemsByCategory = new Dictionary<string, List<ShopItemData>>();

        // Extract categories
        StorefrontCategoryTab firstCategory = null;
        for (int i = 0; i < items.Count; i++)
        {
            var catName = items[i].category;
            if (!_itemsByCategory.ContainsKey(catName))
            {
                _itemsByCategory[catName] = new List<ShopItemData>();
                StorefrontCategoryTab cat = Instantiate(categoryTab, categoryTabBar);
                cat.Initialize(catName);
                if (firstCategory == null)
                    firstCategory = cat;
            }
            _itemsByCategory[catName].Add(items[i]);
        }

        firstCategory.Select();
    }

    public void UpdateWalletText(int amount)
    {
        wallet.text = string.Format("{0:C}", amount);
    }

    public void ChargeWallet(int amount)
    {
        inventory.ChargeWallet(amount);
    }

    public void ShowCategory(StorefrontCategoryTab category)
    {
        if (category == _activeCategory)
            return;

        if (_activeCategory != null)
            _activeCategory.Deselect();

        _activeCategory = category;

        if (_itemCards == null)
            _itemCards = new List<ShopItem>(_itemsByCategory[category.Name].Count);

        DestroyCurrentItems();

        List<ShopItemData> thisCategoryItems = _itemsByCategory[category.Name];

        for (int i = 0; i < thisCategoryItems.Count; i++)
        {
            ShopItem item = Instantiate(shopItemCard, grid);
            item.Load(thisCategoryItems[i], inventory.HasItem(thisCategoryItems[i]));
            _itemCards.Add(item);
        }
    }

    private void DestroyCurrentItems()
    {
        float delay = 0;
        for (int i = 0; i < _itemCards.Count; i++)
        {
            Destroy(_itemCards[i].gameObject, delay);
            delay += Time.fixedDeltaTime;
        }
        _itemCards.Clear();
    }
}