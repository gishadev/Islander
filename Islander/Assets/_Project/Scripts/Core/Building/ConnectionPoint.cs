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

        public ConnectionPoint(Vector3 localPosition, Transform parent)
        {
            _localPosition = localPosition;
            _parent = parent;

            GetCurrentEdge();
        }

        private void GetCurrentEdge()
        {
            if (LocalPosition.x > Parent.position.x)
                _edge = Edge.Right;
            if (LocalPosition.x < Parent.position.x)
                _edge = Edge.Left;
            if (LocalPosition.z > Parent.position.z)
                _edge = Edge.Forward;
            if (LocalPosition.z < Parent.position.z)
                _edge = Edge.Back;
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