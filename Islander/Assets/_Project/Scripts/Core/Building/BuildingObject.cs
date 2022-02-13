using System;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingObject : MonoBehaviour
    {
        private ConnectionPoint[] _connectionPoints;

        public ConnectionPoint[] ConnectionPoints => _connectionPoints;

        private void Awake()
        {
            _connectionPoints = new[]
            {
                new ConnectionPoint(Vector3.forward / 2f, transform, Edge.Forward),
                new ConnectionPoint(Vector3.right / 2f, transform, Edge.Right),
                new ConnectionPoint(Vector3.back / 2f, transform, Edge.Back),
                new ConnectionPoint(Vector3.left / 2f, transform, Edge.Left)
            };
        }

        public ConnectionPoint GetPointByEdge(Edge edge)
        {
            foreach (var point in ConnectionPoints)
            {
                if (point.Edge == edge)
                    return point;
            }

            Debug.LogError("Appropriate edge was not found.");
            return ConnectionPoints[0];
        }

        private void OnDrawGizmos()
        {
            foreach (var point in ConnectionPoints)
            {
                if (point.IsBlocked)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(point.WorldPosition, .1f);
            }
        }
    }
}