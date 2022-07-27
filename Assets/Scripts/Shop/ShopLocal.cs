using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Shop", menuName = "VR Shop / Local Shop")]
public class ShopLocal : Shop
{
    [SerializeField] private string jsonFile;

    public override IEnumerator Fetch()
    {
        var request = Resources.LoadAsync(jsonFile, typeof(TextAsset));
        yield return request;

        var txtAsset = (TextAsset)request.asset;
        string jsonContent = txtAsset.text;

        data = new ShopData(jsonContent);
    }
}