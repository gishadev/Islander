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
                var position =
                    BuildingSystem.FindBuildPositionAndRotation(raycastHit.point, out var rotation, out var point);

                if (point != null && point.IsBlocked)
                    return;

                var obj = Instantiate(woodPlank, position, rotation);

                // Creating new raft.
                GameObject raft;
                if (point == null)
                {
                    raft = new GameObject("Raft");
                    raft.AddComponent<Rigidbody>();
                }
                else
                {
                    raft = point.Parent.parent.gameObject;
                    //TODO: ALSO BLOCK CONNECTED POINT
                    point.Block();
                }


                obj.transform.SetParent(raft.transform);
            }
        }

        private void OnEquip()
        {
        }
    }
}