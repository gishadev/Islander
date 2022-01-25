using System;
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
            Debug.Log($"Current wood count: <color=brown>{Instance._woodCount}</color>");
        }
    }
}