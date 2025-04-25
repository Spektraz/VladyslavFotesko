using UnityEngine;
namespace Daily
{
    [CreateAssetMenu(fileName = "DailyBonusData", menuName = "Daily/Bonus Data")]
    public class DailyBonusSO : ScriptableObject
    {
        public int coinCount;
    }
}