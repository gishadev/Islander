using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class Grid
    {
        private int _width;
        private int _length;
        private float _cellSize;
        private Vector3 _center;
        private int[,] _gridArray;

        public Grid(int width, int length, float cellSize, Vector3 center)
        {
            _length = length;
            _width = width;
            _cellSize = cellSize;

            _center = center;
            _center.y = 0f;

            _gridArray = new int[width, length];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < _gridArray.GetLength(1); z++)
                {
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                }

                Debug.DrawLine(GetWorldPosition(0, length), GetWorldPosition(width, length), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, length), Color.white, 100f);
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            int xOffset = Mathf.FloorToInt(_width / 2f - _center.x);
            int zOffset = Mathf.FloorToInt(_length / 2f - _center.z);

            return new Vector3(x - xOffset, 0, z - zOffset) * _cellSize;
        }

        public void GetXZFromPosition(Vector3 worldPosition, out int x, out int z)
        {
            int xOffset = Mathf.FloorToInt(_width / 2f - _center.x);
            int zOffset = Mathf.FloorToInt(_length / 2f - _center.z);
            
            x = Mathf.FloorToInt(worldPosition.x / _cellSize + xOffset);
            z = Mathf.FloorToInt(worldPosition.z / _cellSize + zOffset);
        }
    }
}