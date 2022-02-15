using System;
using Gisha.Islander.Core.Building;
using Gisha.Islander.Photon;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.Player.Tools
{
    public class Hammer : Tool
    {
        private BuildingGUI _buildingGUI;
        private PlayerController _myPlayerController;

        private void Awake()
        {
            _myPlayerController = PhotonManager.MyPhotonPlayer.PlayerController;
        }

        private void OnEnable()
        {
            Equiped += OnEquip;
        }

        private void OnDisable()
        {
            Equiped -= OnEquip;
        }

        private void Update()
        {
            PrebuildRaycastCheck();
        }

        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner)
        {
            if (Physics.Raycast(origin, direction, out var raycastHit))
                BuildingSystem.Build(raycastHit);
        }

        private void PrebuildRaycastCheck()
        {
            Vector3 origin = _myPlayerController.FPSCamera.CameraRigTrans.position;
            Vector3 direction = _myPlayerController.FPSCamera.CameraRigTrans.forward;

            if (Physics.Raycast(origin, direction, out var raycastHit))
            {
                var creationData = BuildingSystem.GetCreationDataFromRaycast(raycastHit);
                _buildingGUI.UpdateGUI(creationData);
            }
        }


        private void OnEquip(bool isEquip)
        {
            _buildingGUI = FindObjectOfType<BuildingGUI>();
            _buildingGUI.ChangePanelVisibility(isEquip);
        }
    }
}