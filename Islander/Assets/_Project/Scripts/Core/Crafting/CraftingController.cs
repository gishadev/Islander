using System;
using System.Collections.Generic;
using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Core.Crafting
{
    public class CraftingController : MonoBehaviour
    {
        [SerializeField] private List<ItemCreationData> itemsCraftData;
        [SerializeField] private float spawnForwardOffset;

        public Action Crafted;

        public List<ItemCreationData> ItemsCraftData => itemsCraftData;

        public void Craft(ItemCreationData creationData, PlayerController playerController)
        {
            // Check if all resources are in an enough count to craft an item. 
            foreach (var resourceForCraft in creationData.Recipe.ResourcesForCreation)
                if (resourceForCraft.Count > InventoryManager.Instance.GetResourceCount(resourceForCraft.ResourceType))
                {
                    Debug.Log($"Not enough {resourceForCraft.ResourceType} to craft {creationData.name}");
                    return;
                }

            // Subtract resources count.
            foreach (var resourceForCraft in creationData.Recipe.ResourcesForCreation)
                InventoryManager.Instance.ChangeResourceCount(resourceForCraft.ResourceType, -resourceForCraft.Count);

            CraftTool(creationData, playerController);

            Crafted?.Invoke();
        }

        private void CraftTool(ItemCreationData creationData, PlayerController playerController)
        {
            playerController.AddTool(creationData.Prefab.name);
            itemsCraftData.Remove(creationData);
        }
    }
}