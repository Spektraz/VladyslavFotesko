using UnityEngine;
using UnityEngine.AddressableAssets;
namespace SkinMenu
{
    [CreateAssetMenu(menuName = "Skins/SkinData")]
    public class SkinData : ScriptableObject
    {
        public string Id;
        public string DisplayName;

        public AssetReferenceGameObject ModelPrefab;
        public AssetReference Material;
    }
}