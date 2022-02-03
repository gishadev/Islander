using System;
using System.Collections.Generic;
using Gisha.Islander.Player;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Core.Crafting
{
    public class CraftingController : MonoBehaviour
    {
        [SerializeField] private List<ItemCraftData> itemsCraftData;
        [SerializeField] private float spawnForwardOffset;

        public Action Crafted;

        public List<ItemCraftData> ItemsCraftData => itemsCraftData;

        public void Craft(ItemCraftData craftData, PlayerController playerController)
        {
            // Check if all resources are in an enough count to craft an item. 
            foreach (var resourceForCraft in craftData.Recipe.ResourcesForCraft)
                if (resourceForCraft.Count > InventoryManager.Instance.GetResourceCount(resourceForCraft.ResourceType))
                {
                    Debug.Log($"Not enough {resourceForCraft.ResourceType} to craft {craftData.name}");
                    return;
                }

            // Subtract resources count.
            foreach (var resourceForCraft in craftData.Recipe.ResourcesForCraft)
                InventoryManager.Instance.ChangeResourceCount(resourceForCraft.ResourceType, -resourceForCraft.Count);

            switch (craftData.ItemCraftType)
            {
                case ItemCraftType.Object:
                    CraftObject(craftData, playerController);
                    break;
                case ItemCraftType.Tool:
                    CraftTool(craftData, playerController);
                    break;
            }

            Crafted?.Invoke();
        }

        private void CraftObject(ItemCraftData craftData, PlayerController playerController)
        {
            var position = playerController.transform.position +
                           playerController.transform.forward * spawnForwardOffset;
            var rotation = Quaternion.Euler(0f, playerController.transform.rotation.eulerAngles.y, 0f);

            PhotonNetwork.Instantiate("Floaters/" + craftData.Prefab.name, position, rotation);
        }

        private void CraftTool(ItemCraftData craftData, PlayerController playerController)
        {
            playerController.AddTool(craftData.Prefab);
            itemsCraftData.Remove(craftData);
        }
    }
}