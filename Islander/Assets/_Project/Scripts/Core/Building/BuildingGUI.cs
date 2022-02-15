using System;
using Gisha.Islander.Core.Crafting;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingGUI : MonoBehaviour
    {
        [SerializeField] private GameObject buildingPanel;
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

            resourcesToBuildText.text = costText;
        }
    }
}