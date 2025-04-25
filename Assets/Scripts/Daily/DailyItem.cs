using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Daily
{
    public class DailyItem : MonoBehaviour
    {
        [SerializeField] private Image imageNot;
        [SerializeField] private Image imageWas;
        [SerializeField] private TextMeshProUGUI textCoins;
        public void SetInfo(bool isWas)
        {
            if (isWas)
            {
                imageNot.enabled = false;
                imageWas.enabled = true;
            }
            else
            {
                imageNot.enabled = true;
                imageWas.enabled = false;
            }
        }
        public void SetText(string infoCoins)
        {
            textCoins.text = infoCoins;
        }
    }
}
