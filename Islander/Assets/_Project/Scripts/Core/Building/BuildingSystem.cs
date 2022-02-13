using System.Linq;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        public static void Build(GameObject buildingObjectPrefab, RaycastHit hitInfo)
        {
            var position =
                FindBuildPositionAndRotation(hitInfo.point, out var rotation, out var targetConnectionPoint);

            if (targetConnectionPoint != null && targetConnectionPoint.IsBlocked)
                return;

            var buildingObject = Object.Instantiate(buildingObjectPrefab, position, rotation)
                .GetComponent<BuildingObject>();

            // Creating new raft.
            GameObject raft;
            if (targetConnectionPoint == null)
            {
                raft = new GameObject("Raft");
                raft.AddComponent<Rigidbody>();
            }
            // Building object for an old raft.
            else
            {
                raft = targetConnectionPoint.Parent.parent.gameObject;
                BlockConnectionPoints(targetConnectionPoint, buildingObject);
            }

            buildingObject.transform.SetParent(raft.transform);
        }

        private static void BlockConnectionPoints(ConnectionPoint targetPoint, BuildingObject newBuildingObject)
        {
            var oppositeEdge = targetPoint.GetOppositeEdge(targetPoint.Edge);
            var newConnectionPoint = newBuildingObject.GetPointByEdge(oppositeEdge);

            targetPoint.Block();
            newConnectionPoint?.Block();
        }

        private static Vector3 FindBuildPositionAndRotation(Vector3 origin, out Quaternion rotation,
            out ConnectionPoint nearestPoint)
        {
            nearestPoint = FindNearestPoint(origin, 1f, out var pointParent);
            if (nearestPoint == null)
            {
                rotation = Quaternion.identity;
                return origin;
            }

            var objectTransform = pointParent;

            rotation = objectTransform.rotation;

            return nearestPoint.WorldPosition + nearestPoint.LocalPosition;
        }

        private static ConnectionPoint FindNearestPoint(Vector3 origin, float searchRadius, out Transform parent)
        {
            var objects = Physics.OverlapSphere(origin, searchRadius)
                .Where(x => x.TryGetComponent(out BuildingObject buildingObject))
                .Select(x => x.GetComponent<BuildingObject>());

            ConnectionPoint nearestPoint = null;
            parent = null;

            float minDst = Mathf.Infinity;
            foreach (var buildingObject in objects)
            {
                foreach (var point in buildingObject.ConnectionPoints)
                {
                    float sqrDst = Vector3.SqrMagnitude(point.WorldPosition - origin);
                    if (sqrDst < minDst)
                    {
                        nearestPoint = point;
                        parent = buildingObject.transform;
                        minDst = sqrDst;
                    }
                }
            }

            return nearestPoint;
        }
    }
}