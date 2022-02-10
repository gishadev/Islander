using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingObject : MonoBehaviour
    {
        [SerializeField] private ConnectionPoint[] _connectionPoints;

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