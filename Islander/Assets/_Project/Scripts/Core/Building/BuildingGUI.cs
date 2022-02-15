using System;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingGUI : MonoBehaviour
    {
        [SerializeField] private GameObject buildingPanel;
        [SerializeField] private TMP_Text buildOperationText;
        [SerializeField] private TMP_Text resourcesToBuildText;

        public void ChangePanelVisibility(bool isVisible)
        {
            buildingPanel.SetActive(isVisible);
        }

        public void UpdateGUI(ItemCreationData creationData)
        {
            string costText = String.Empty;

            foreach (var resourceForCraft in creationData.Recipe.ResourcesForCreation)
                costText += $"{resourceForCraft.Count} {resourceForCraft.ResourceType}, \n";

            buildOperationText.text = $"BUILD: {creationData.Prefab.name}";
            resourcesToBuildText.text = costText;
        }
    }
}