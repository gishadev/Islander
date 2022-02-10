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
                var position = BuildingSystem.FindBuildPositionAndRotation(raycastHit.point, out var rotation, out var raftTrans);
                var obj = Instantiate(woodPlank, position, rotation);

                if (raftTrans == null)
                {
                    raftTrans = new GameObject("Raft").transform;
                    raftTrans.gameObject.AddComponent<Rigidbody>();
                }

                obj.transform.SetParent(raftTrans);
            }
        }

        private void OnEquip()
        {
        }
    }
}