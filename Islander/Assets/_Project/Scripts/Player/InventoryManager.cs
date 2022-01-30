using System;
using Gisha.Islander.UI;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private int _woodCount, _stoneCount, _ironCount, _titaniumCount;

        public int WoodCount => _woodCount;
        public int StoneCount => _stoneCount;
        public int IronCount => _ironCount;
        public int TitaniumCount => _titaniumCount;


        private void Awake()
        {
            Instance = this;
        }

        public void ChangeResourceCount(ResourceType resourceType, int count)
        {
            switch (resourceType)
            {
                case ResourceType.Wood:
                    _woodCount += count;
                    break;
                case ResourceType.Stone:
                    _stoneCount += count;
                    break;
                case ResourceType.Iron:
                    _ironCount += count;
                    break;
                case ResourceType.Titanium:
                    _titaniumCount += count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
            }

            UIManager.Instance.UpdateResourcesCount();
        }

        public int GetResourceCount(ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Wood => _woodCount,
                ResourceType.Stone => _stoneCount,
                ResourceType.Iron => _ironCount,
                ResourceType.Titanium => _titaniumCount,
                _ => throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null)
            };
        }
    }

    public enum ResourceType
    {
        Wood,
        Stone,
        Iron,
        Titanium
    }
}