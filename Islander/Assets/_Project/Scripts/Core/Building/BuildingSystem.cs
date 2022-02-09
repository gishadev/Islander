using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public static class BuildingSystem
    {
        private static Grid _currentGrid;

        public static Grid CreateGrid(Vector3 gridCenter)
        {
            _currentGrid = new Grid(25, 25, 1f, gridCenter);
            return _currentGrid;
        }
    }
}