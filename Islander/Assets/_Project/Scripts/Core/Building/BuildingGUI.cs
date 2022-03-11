using System;
using Gisha.Islander.Photon;
using Gisha.Islander.UI;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingGUI : GUIController
    {
        [SerializeField] private GameObject buildingPanel;
        [SerializeField] private TMP_Text buildOperationText;
        [SerializeField] private TMP_Text resourcesToBuildText;

        private bool _isInitialized;

        private void OnDisable()
        {
            PhotonManager.MyPhotonPlayer.PlayerRespawned -= ResetGUI;
        }

        public override void ResetGUI()
        {
            ChangePanelVisibility(false);
            _isInitialized = false;
        }

        public void ChangePanelVisibility(bool isVisible)
        {
            if (!_isInitialized)
            {
                PhotonManager.MyPhotonPlayer.PlayerRespawned += ResetGUI;
                _isInitialized = true;
            }

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