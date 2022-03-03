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
            if (!playerController.InventoryManager.CheckIfEnoughResources(creationData))
                return;

            playerController.InventoryManager.SpendResources(creationData);

            CraftTool(creationData, playerController);

            Crafted?.Invoke();
        }

        private void CraftTool(ItemCreationData creationData, PlayerController playerController)
        {
            playerController.AddTool(creationData.Prefab.name);
            //itemsCraftData.Remove(creationData);
        }
    }
}