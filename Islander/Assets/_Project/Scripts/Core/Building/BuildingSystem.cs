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

            if (hitInfo.collider.CompareTag("Raft"))
                TryModify();
            else
                SpawnRaft(hitInfo.point);
        }

        private static void Initialize()
        {
            _isInitialized = true;
            _buildingSystemData = Resources.FindObjectsOfTypeAll<BuildingSystemData>()[0];
        }

        private static void SpawnRaft(Vector3 position)
        {
            Object.Instantiate(_buildingSystemData.RaftPrefabs[0], position, Quaternion.identity);
        }

        private static void TryModify()
        {
            Debug.Log("Try modify raft");
        }
    }
}