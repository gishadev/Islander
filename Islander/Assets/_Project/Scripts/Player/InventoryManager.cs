using System;
using Gisha.Islander.Core;
using Gisha.Islander.UI;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class InventoryManager : MonoBehaviourPun
    {
        public static InventoryManager My { get; private set; }

        private int _woodCount, _stoneCount, _ironCount, _titaniumCount;

        public int WoodCount => _woodCount;
        public int StoneCount => _stoneCount;
        public int IronCount => _ironCount;
        public int TitaniumCount => _titaniumCount;
        
        private void Awake()
        {
            if (photonView.IsMine)
                My = this;
        }

        public bool CheckIfEnoughResources(ItemCreationData creationData)
        {
            // Check if all resources are in an enough count to craft an item. 
            foreach (var resourceForCraft in creationData.Recipe.ResourcesForCreation)
                if (resourceForCraft.Count > GetResourceCount(resourceForCraft.ResourceType))
                {
                    Debug.Log($"Not enough {resourceForCraft.ResourceType} to create {creationData.name}");
                    return false;
                }

            return true;
        }

        public void SpendResources(ItemCreationData creationData)
        {
            // Subtract resources count.
            foreach (var resourceForCraft in creationData.Recipe.ResourcesForCreation)
                ChangeResourceCount(resourceForCraft.ResourceType, -resourceForCraft.Count);
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

            if (this == My)
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