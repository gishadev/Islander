using System;
using Gisha.Islander.UI;
using UnityEngine;

namespace Gisha.Islander.Character
{
    public class InventoryManager : MonoBehaviour
    {
        private static InventoryManager Instance { get; set; }

        private int _woodCount;

        public int WoodCount => _woodCount;

        private void Awake()
        {
            Instance = this;
        }

        public static void ChangeWoodCount(int count)
        {
            Instance._woodCount += count;
            UIManager.UpdateResourcesCount(Instance._woodCount);
        }
    }
}