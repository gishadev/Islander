using System;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class ConnectionPoint : MonoBehaviour
    {
        [SerializeField] private Edge edge;

        public Edge Edge => edge;

        private void Start()
        {
            GetCurrentEdge();
        }

        private void GetCurrentEdge()
        {
            if (transform.position.x > transform.parent.position.x)
                edge = Edge.Right;
            if (transform.position.x < transform.parent.position.x)
                edge = Edge.Left;
            if (transform.position.z > transform.parent.position.z)
                edge = Edge.Forward;
            if (transform.position.z < transform.parent.position.z)
                edge = Edge.Back;
        }
    }

    public enum Edge
    {
        Forward,
        Right,
        Back,
        Left
    }
}