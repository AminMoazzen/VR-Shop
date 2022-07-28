using Nouranium;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private Inventory inventory;
    [SerializeField] private MessageInventoryItemData[] spawnNewItemOn;
    [SerializeField] private Message[] loadOwnedItemsOn;

    private InventoryItemData _itemData;

    private void Awake()
    {
        foreach (var msg in spawnNewItemOn)
        {
            msg.StartListening(SpawnNewItem);
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
            Spawn(item, false);
        }
    }

    private void Spawn(InventoryItemData itemData, bool isNew)
    {
        _itemData = itemData;
        ShopItemData shopItem = shop.FindItem(_itemData.id);
        Vector3 position = itemData.position;
        Quaternion rotation = itemData.rotation;
        if (isNew)
        {
            position = transform.position;
            rotation = Quaternion.identity;
        }
        Addressables.InstantiateAsync(shopItem.prefabAddress, itemData.position, itemData.rotation).Completed += OnInstantiatedPrefab;
    }

    public void SpawnNewItem(InventoryItemData itemData)
    {
        Spawn(itemData, true);
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