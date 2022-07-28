using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Shop shop;
    [SerializeField] private UnityEvent onDataLoaded;

    private IEnumerator Start()
    {
        yield return inventory.Fetch();
        yield return shop.Fetch();

        onDataLoaded.Invoke();
    }
}