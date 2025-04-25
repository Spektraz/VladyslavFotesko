using Assets.Scripts.Application;
using UnityEngine;

namespace Fortune
{
    [CreateAssetMenu(fileName = "CardBonusData", menuName = "Cards/Bonus Data")]
    public class CardBonusDataSO : ScriptableObject
    {
        public TypeSkinItems typeSkinItems;
        public BonusType bonusType;
        public float dropChance;
        public int coinCount;
        public Sprite icon;
    }
}