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
                var grid = BuildingSystem.CreateGrid(transform.position);
                grid.GetXZFromPosition(raycastHit.point, out int x, out int z);
                var position = grid.GetWorldPosition(x, z) + Vector3.up * 6f;
                Instantiate(woodPlank, position, Quaternion.identity);
            }
        }

        private void OnEquip()
        {
        }
    }
}