using System;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingObject : MonoBehaviour
    {
        private ConnectionPoint[] _connectionPoints;

        public ConnectionPoint[] ConnectionPoints => _connectionPoints;

        private void Start()
        {
            _connectionPoints = new[]
            {
                new ConnectionPoint(Vector3.forward / 2f, transform),
                new ConnectionPoint(Vector3.right / 2f, transform),
                new ConnectionPoint(Vector3.back / 2f, transform),
                new ConnectionPoint(Vector3.left / 2f, transform)
            };
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (var point in ConnectionPoints)
            {
                Gizmos.DrawWireSphere(point.WorldPosition, .1f);
            }
        }
    }
}