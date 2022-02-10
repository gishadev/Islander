using System.Linq;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        public static Vector3 FindBuildPositionAndRotation(Vector3 origin, out Quaternion rotation,
            out Transform raftTransform)
        {
            var nearestPoint = FindNearestPoint(origin, 1f);
            if (nearestPoint == null)
            {
                raftTransform = null;
                rotation = Quaternion.identity;
                return origin;
            }
            var objectTransform = nearestPoint.transform.parent;
            
            raftTransform = objectTransform.parent;
            rotation = nearestPoint.transform.rotation;
            
            return nearestPoint.transform.position + CalculateOffset(objectTransform.position, nearestPoint);
        }

        private static ConnectionPoint FindNearestPoint(Vector3 origin, float searchRadius)
        {
            var points = Physics.OverlapSphere(origin, searchRadius)
                .Where(x => x.TryGetComponent(out ConnectionPoint connectionPoint))
                .Select(x => x.GetComponent<ConnectionPoint>());
            ConnectionPoint nearestPoint = null;

            float minDst = Mathf.Infinity;
            foreach (var point in points)
            {
                float sqrDst = Vector3.SqrMagnitude(point.transform.position - origin);
                if (sqrDst < minDst)
                {
                    nearestPoint = point;
                    minDst = sqrDst;
                }
            }

            return nearestPoint;
        }

        private static Vector3 CalculateOffset(Vector3 center, ConnectionPoint targetPoint)
        {
            // switch (targetPoint.Edge)
            // {
            //     case Edge.Forward:
            //         return Vector3.back / 2f;
            //     case Edge.Right:
            //         return Vector3.left / 2f;
            //     case Edge.Back:
            //         return Vector3.forward / 2f;
            //     case Edge.Left:
            //         return Vector3.right / 2f;
            // }

            Debug.Log(targetPoint.transform.position - center);
            return targetPoint.transform.position - center;
        }
    }
}