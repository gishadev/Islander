using System.Linq;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        public static Vector3 FindBuildPositionAndRotation(Vector3 origin, out Quaternion rotation,
            out Transform raftTransform)
        {
            var nearestPoint = FindNearestPoint(origin, 1f, out var pointParent);
            if (nearestPoint == null)
            {
                raftTransform = null;
                rotation = Quaternion.identity;
                return origin;
            }

            var objectTransform = pointParent;

            raftTransform = objectTransform.parent;
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