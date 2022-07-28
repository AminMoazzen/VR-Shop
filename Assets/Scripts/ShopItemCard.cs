using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ShopItemCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image thumbnail;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI priceTag;
    [SerializeField] private GameObject metadataGameObject;
    [SerializeField] private TextMeshProUGUI metadata;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject checkMark;

    private ShopItemData _itemData;

    public void Load(ShopItemData data, bool hasItem)
    {
        _itemData = data;

        Addressables.LoadAssetAsync<Sprite>(_itemData.thumbnailAddress).Completed += OnThumbnailLoaded;

        itemName.text = _itemData.name;
        priceTag.text = string.Format("{0:C}", _itemData.price);
        foreach (var row in _itemData.metadata)
        {
            metadata.text += $"{row.Key} : {row.Value}\n";
        }

        if (hasItem)
            SetAsOwned();
    }

    private void OnThumbnailLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                thumbnail.sprite = obj.Result;
                break;

            case AsyncOperationStatus.Failed:
                Debug.LogWarning($"Could not load the thumbnail for {_itemData.name}");
                break;

            default:
                break;
        }
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
        metadataGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        metadataGameObject.SetActive(false);
    }

    private void SetAsOwned()
    {
        buyButton.SetActive(false);
        checkMark.SetActive(true);
    }
}