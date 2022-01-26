using System;
using Gisha.Islander.UI;
using UnityEngine;

namespace Gisha.Islander.Character
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private int _woodCount;

        public int WoodCount => Instance._woodCount;

        private void Awake()
        {
            Instance = this;
        }

        public void ChangeWoodCount(int count)
        {
            Instance._woodCount += count;
            UIManager.Instance.UpdateResourcesCount(Instance._woodCount);
        }
    }
}