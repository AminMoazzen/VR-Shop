using Nouranium;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private Inventory inventory;
    [SerializeField] private MessageInventoryItemData[] spawnOn;
    [SerializeField] private Message[] loadOwnedItemsOn;

    private InventoryItemData _itemData;

    private void Awake()
    {
        foreach (var msg in spawnOn)
        {
            msg.StartListening(Spawn);
        }

        foreach (var msg in loadOwnedItemsOn)
        {
            msg.StartListening(LoadOwnedItems);
        }
    }

    public void LoadOwnedItems()
    {
        foreach (var item in inventory.data.items)
        {
            Spawn(item);
        }
    }

    public void Spawn(InventoryItemData itemData)
    {
        _itemData = itemData;
        ShopItemData shopItem = shop.FindItem(_itemData.id);
        Addressables.InstantiateAsync(shopItem.prefabAddress, itemData.position, itemData.rotation).Completed += OnInstantiatedPrefab;
    }

    private void OnInstantiatedPrefab(AsyncOperationHandle<GameObject> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                obj.Result.GetComponent<InventoryItem>().SetData(_itemData);
                break;

            case AsyncOperationStatus.Failed:
                Debug.LogWarning("Could not load the item!");
                break;

            default:
                break;
        }
    }
}