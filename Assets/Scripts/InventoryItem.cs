using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Shop shop;

    private InventoryItemData _itemData;

    public void SetData(InventoryItemData data)
    {
        _itemData = data;
    }

    private void OnApplicationQuit()
    {
        _itemData.position = transform.position;
        _itemData.rotation = transform.rotation;
        inventory.UpdateItem(_itemData);
    }
}