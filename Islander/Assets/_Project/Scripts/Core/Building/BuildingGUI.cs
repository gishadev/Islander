using TMPro;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingGUI : MonoBehaviour
    {
        [SerializeField] private GameObject buildingPanel;
        [SerializeField] private TMP_Text woodCountText, stoneCountText, ironCountText, titaniumCountText;

        public void ChangePanelVisibility(bool isVisible)
        {
            buildingPanel.SetActive(isVisible);
        }
    }
}