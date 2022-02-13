using UnityEditor;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        private static BuildingSystemData _buildingSystemData;

        private static bool _isInitialized;

        public static void Build(RaycastHit hitInfo)
        {
            if (!_isInitialized)
                Initialize();

            if (hitInfo.collider.TryGetComponent(out Raft raft))
                TryModify(raft);
            else
                Spawn(hitInfo.point, 0);
        }

        private static void Initialize()
        {
            _isInitialized = true;
            _buildingSystemData = Resources.FindObjectsOfTypeAll<BuildingSystemData>()[0];
        }

        private static void Spawn(Vector3 position, int level)
        {
            Object.Instantiate(_buildingSystemData.RaftPrefabs[level], position, Quaternion.identity);
        }

        private static void TryModify(Raft targetRaft)
        {
            if (targetRaft.Level >= _buildingSystemData.RaftPrefabs.Length - 1)
                return;

            Spawn(targetRaft.transform.position, targetRaft.Level + 1);
            Object.Destroy(targetRaft.gameObject);
        }
    }
}