using TMPro;
using UnityEngine;

namespace Gisha.Islander.UI
{
    public class EnvironmentHUD : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private TMP_Text objectNameText;
        [SerializeField] private Transform healthBar;

        public void Hide()
        {
            infoPanel.SetActive(false);
        }

        public void Show()
        {
            infoPanel.SetActive(true);
        }

        public void UpdateHUD(string objName, float healthPercentage)
        {
            objectNameText.text = objName;
            healthBar.localScale = new Vector3(healthPercentage, 1f, 1f);
        }
    }
}