using UnityEngine;
using UnityEngineInternal;

namespace Gisha.Islander.Core.Building
{
    public class ConnectionPoint
    {
        public bool IsBlocked => _isBlocked;
        public Edge Edge => _edge;
        public Transform Parent => _parent;
        public Vector3 LocalPosition => Parent.rotation * _localPosition;
        public Vector3 WorldPosition => Parent.position + LocalPosition;

        private Transform _parent;
        private bool _isBlocked;
        private Vector3 _localPosition;
        private Edge _edge;

        public ConnectionPoint(Vector3 localPosition, Transform parent, Edge edge)
        {
            _localPosition = localPosition;
            _parent = parent;
            _edge = edge;
        }
        
        public Edge GetOppositeEdge(Edge edge)
        {
            switch (edge)
            {
                case Edge.Forward:
                    return Edge.Back;
                case Edge.Right:
                    return Edge.Left;
                case Edge.Back:
                    return Edge.Forward;
                case Edge.Left:
                    return Edge.Right;
                default:
                    Debug.LogError("Error in getting opposite edge. Target edge is null!");
                    return Edge.Forward;
            }
        }

        public void Block()
        {
            _isBlocked = true;
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