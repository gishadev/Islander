using System.Linq;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        public static Vector3 FindBuildPosition(Vector3 origin)
        {
            var nearestPoint = FindNearestPoint(origin, 5f);
            if (nearestPoint == null)
                return origin;

            return nearestPoint.transform.position;
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
    }
}