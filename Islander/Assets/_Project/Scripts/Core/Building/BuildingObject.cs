using System;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingObject : MonoBehaviour
    {
        private ConnectionPoint[] _connectionPoints;

        private void Start()
        {
            _connectionPoints = GetComponentsInChildren<ConnectionPoint>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (var point in _connectionPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, .1f);
            }
        }
    }
}