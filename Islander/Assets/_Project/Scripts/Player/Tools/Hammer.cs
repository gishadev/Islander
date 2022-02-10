using System;
using Gisha.Islander.Core.Building;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Hammer : Tool
    {
        [SerializeField] private GameObject woodPlank;

        private void OnEnable()
        {
            Equiped += OnEquip;
        }

        private void OnDisable()
        {
            Equiped -= OnEquip;
        }

        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner)
        {
            if (Physics.Raycast(origin, direction, out var raycastHit))
            {
                Instantiate(woodPlank, BuildingSystem.FindBuildPosition(raycastHit.point), Quaternion.identity);
            }
        }

        private void OnEquip()
        {
        }
    }
}