using System;
using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    public class BuildingSystem : MonoBehaviour
    {
        private void Start()
        {
            Grid grid = new Grid(5, 5, 5);
        }
    }
}