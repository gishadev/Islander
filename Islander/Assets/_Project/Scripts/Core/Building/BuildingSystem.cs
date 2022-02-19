using Gisha.Islander.Photon;
using Gisha.Islander.Player;
using UnityEditor;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        private static BuildingSystemData _buildingSystemData;

        private static bool _isInitialized;

        public static void Build(RaycastHit hitInfo, PlayerController owner)
        {
            if (!_isInitialized)
                Initialize();

            // Modification just changes raft level.
            int raftLevel = 0;
            if (hitInfo.collider.TryGetComponent(out Raft raft))
                TryModifyRaft(raft, out raftLevel);

            // Spawning of the raft (zero level or modified one)
            var creationData = _buildingSystemData.RaftsCreationData[raftLevel];
            if (owner.InventoryManager.CheckIfEnoughResources(creationData))
            {
                if (raft != null)
                    Object.Destroy(raft.gameObject);

                owner.SpawnRaft(creationData.Prefab, hitInfo.point);
                // SpawnRaft(creationData, hitInfo.point);
                owner.InventoryManager.SpendResources(creationData);
            }
        }

        public static ItemCreationData GetCreationDataFromRaycast(RaycastHit hitInfo)
        {
            if (!_isInitialized)
                Initialize();

            int raftLevel = 0;
            if (hitInfo.collider.TryGetComponent(out Raft raft))
                TryModifyRaft(raft, out raftLevel);

            return _buildingSystemData.RaftsCreationData[raftLevel];
        }


        private static void Initialize()
        {
            _isInitialized = true;
            _buildingSystemData =
                (BuildingSystemData) Resources.Load("BuildingData", typeof(BuildingSystemData));
        }

        // private static void SpawnRaft(ItemCreationData creationData, Vector3 position)
        // {
        //     Object.Instantiate(creationData.Prefab, position + Vector3.up, Quaternion.identity);
        // }

        private static void TryModifyRaft(Raft targetRaft, out int raftLevel)
        {
            raftLevel = 0;
            if (targetRaft.Level >= _buildingSystemData.RaftsCreationData.Length - 1)
                return;

            raftLevel = targetRaft.Level + 1;
        }
    }
}