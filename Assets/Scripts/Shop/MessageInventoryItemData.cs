using UnityEngine;

namespace Nouranium
{
    [CreateAssetMenu(fileName = "NewInventoryItemDataMessage", menuName = "Nouranium Messaging / Custom types / Inventory Item Data Message")]
    public class MessageInventoryItemData : MessageWithData<InventoryItemData>
    { }
}