using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        public static void Build(RaycastHit hitInfo, GameObject raftPrefab)
        {
            if (hitInfo.collider.CompareTag("Raft"))
                TryModify();
            else
                SpawnRaft(raftPrefab, hitInfo.point);
        }

        private static void SpawnRaft(GameObject raftPrefab, Vector3 position)
        {
            Object.Instantiate(raftPrefab, position, Quaternion.identity);
        }

        private static void TryModify()
        {
            Debug.Log("Try modify raft");
        }
    }
}