using Gisha.Islander.Player;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Core.Crafting
{
    public class CraftingController : MonoBehaviour
    {
        [SerializeField] private ItemRecipe[] itemRecipes;
        [SerializeField] private float spawnForwardOffset;

        public ItemRecipe[] ItemRecipes => itemRecipes;

        public void Craft(ItemRecipe recipeToCraft, PlayerController playerController)
        {
            // Check if all resources are in an enough count to craft an item. 
            foreach (var resourceForCraft in recipeToCraft.Recipe.ResourcesForCraft)
                if (resourceForCraft.Count > InventoryManager.Instance.GetResourceCount(resourceForCraft.ResourceType))
                {
                    Debug.Log($"Not enough {resourceForCraft.ResourceType} to craft {recipeToCraft.name}");
                    return;
                }

            // Subtract resources count.
            foreach (var resourceForCraft in recipeToCraft.Recipe.ResourcesForCraft)
                InventoryManager.Instance.ChangeResourceCount(resourceForCraft.ResourceType, -resourceForCraft.Count);

            var position = playerController.transform.position + playerController.transform.forward * spawnForwardOffset;
            var rotation = Quaternion.Euler(0f, playerController.transform.rotation.eulerAngles.y, 0f);

            PhotonNetwork.Instantiate(recipeToCraft.Prefab.name, position, rotation);
        }
    }
}