using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class ConnectionPoint
    {
        public Edge Edge => _edge;

        public Vector3 LocalPosition => _parent.rotation * _localPosition;
        public Vector3 WorldPosition => _parent.position + LocalPosition;

        private Vector3 _localPosition;
        private Transform _parent;
        private Edge _edge;

        public ConnectionPoint(Vector3 localPosition, Transform parent)
        {
            _localPosition = localPosition;
            _parent = parent;

            GetCurrentEdge();
        }

        private void GetCurrentEdge()
        {
            if (LocalPosition.x > _parent.position.x)
                _edge = Edge.Right;
            if (LocalPosition.x < _parent.position.x)
                _edge = Edge.Left;
            if (LocalPosition.z > _parent.position.z)
                _edge = Edge.Forward;
            if (LocalPosition.z < _parent.position.z)
                _edge = Edge.Back;
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