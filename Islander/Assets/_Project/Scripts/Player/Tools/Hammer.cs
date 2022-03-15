using Gisha.Islander.Core.Building;
using Gisha.Islander.Photon;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Hammer : Tool
    {
        private BuildingGUI _buildingGUI;
        private PlayerController _myPlayerController;
        private PlayerController _parentController;

        private void Awake()
        {
            _myPlayerController = PhotonManager.MyPhotonPlayer.PlayerController;
            _parentController = GetComponentInParent<PlayerController>();
        }

        private void OnEnable()
        {
            if (_myPlayerController == _parentController)
                Equiped += OnEquip;
        }

        private void OnDisable()
        {
            if (_myPlayerController == _parentController)
                Equiped -= OnEquip;
        }

        private void Update()
        {
            if (_myPlayerController != _parentController)
                return;

            PrebuildRaycastCheck();
        }

        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Press)
            {
                if (Physics.Raycast(origin, direction, out var raycastHit))
                    BuildingSystem.Build(raycastHit, owner);
            }
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