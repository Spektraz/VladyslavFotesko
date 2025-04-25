using SkinMenu;
using System.Collections.Generic;
using UnityEngine;
namespace MainGame
{
    [CreateAssetMenu(fileName = "SkinLibrary", menuName = "Game/Skin Library")]
    public class SkinLibrary : ScriptableObject
    {
        public List<SkinData> skins;
    }
}