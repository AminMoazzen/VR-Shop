using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image thumbnail;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI priceTag;
    [SerializeField] private GameObject descriptions;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject checkMark;

    private bool _hasItem;
    private ShopItemData _itemData;
    private AssetBundle _bundle;

    public void Load(ShopItemData data, AssetBundle bundle, bool hasItem)
    {
        _itemData = data;
        _bundle = bundle;
        _hasItem = hasItem;

        StartCoroutine(LoadingIcon(data.iconName));

        itemName.text = _itemData.name;
        priceTag.text = _itemData.price.ToString();
        if (hasItem)
            SetAsOwned();
    }

    private IEnumerator LoadingIcon(string name)
    {
        AssetBundleRequest assetRequest = _bundle.LoadAssetAsync<Sprite>(name);
        yield return assetRequest;

        thumbnail.sprite = assetRequest.asset as Sprite;
    }

    public void Buy()
    {
        if (inventory.PurchaseItem(_itemData))
        {
            SetAsOwned();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptions.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptions.SetActive(false);
    }

    private void SetAsOwned()
    {
        buyButton.SetActive(false);
        checkMark.SetActive(true);
    }
}