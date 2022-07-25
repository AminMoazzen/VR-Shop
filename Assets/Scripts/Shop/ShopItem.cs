using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI priceTag;

    private bool _hasItem;
    private ShopItemData _data;
    private AssetBundle _bundle;

    public void Load(ShopItemData data, AssetBundle bundle, bool hasItem)
    {
        _data = data;
        _bundle = bundle;
        _hasItem = hasItem;

        StartCoroutine(LoadingIcon(data.iconName));

        itemName.text = _data.name;
        priceTag.text = _data.price.ToString();
    }

    private IEnumerator LoadingIcon(string name)
    {
        AssetBundleRequest assetRequest = _bundle.LoadAssetAsync<Sprite>(name);
        yield return assetRequest;

        icon.sprite = assetRequest.asset as Sprite;
    }

    public void Buy()
    {
    }
}