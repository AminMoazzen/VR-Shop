using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StorefrontCategoryTab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI categoryName;
    [SerializeField] private Button categoryButton;
    [SerializeField] private Color selected;
    [SerializeField] private Color unselected;
    [SerializeField] private UnityEvent<StorefrontCategoryTab> onSelected;

    private string _name;

    public string Name => _name;

    public void Initialize(string name)
    {
        _name = name;
        categoryName.text = _name;
        Deselect();
    }

    public void Select()
    {
        onSelected.Invoke(this);
        categoryButton.image.color = selected;
    }

    public void Deselect()
    {
        categoryButton.image.color = unselected;
    }
}