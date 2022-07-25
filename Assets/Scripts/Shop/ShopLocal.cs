using System.Collections;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Shop", menuName = "Shop / Local Shop")]
public class ShopLocal : Shop
{
    [SerializeField] private string jsonFile;

    public override IEnumerator Fetch()
    {
        var request = Resources.LoadAsync(jsonFile, typeof(TextAsset));
        yield return request;

        var txtAsset = (TextAsset)request.asset;
        string jsonContent = txtAsset.text;

        base.data = new ShopData(jsonContent);
    }

    public override bool Purchase(string itemId)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator LoadBundle()
    {
        var asyncBundleRequest =
            AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, data.assetBundleAddress));
        yield return asyncBundleRequest;

        assetBundle = asyncBundleRequest.assetBundle;
        if (assetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }
    }
}