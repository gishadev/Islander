using Gisha.Islander.Player;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Core.Crafting
{
    public class CraftingController : MonoBehaviour
    {
        [SerializeField] private ItemBlueprint[] itemBlueprints;
        [SerializeField] private float spawnForwardOffset;

        private PhotonView _pv;

        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;
        }

        private void Craft(ItemBlueprint blueprintToCraft)
        {
            if (!_pv.IsMine)
                return;

            // Check if all resources are in an enough count to craft an item. 
            foreach (var resourceForCraft in blueprintToCraft.Recipe.ResourcesForCraft)
                if (resourceForCraft.Count < InventoryManager.Instance.GetResourceCount(resourceForCraft.ResourceType))
                {
                    Debug.Log($"Not enough {resourceForCraft.ResourceType} to craft {blueprintToCraft.name}");
                    return;
                }

            // Subtract resources count.
            foreach (var resourceForCraft in blueprintToCraft.Recipe.ResourcesForCraft)
                InventoryManager.Instance.ChangeResourceCount(resourceForCraft.ResourceType, resourceForCraft.Count);

            var position = transform.position + transform.forward * spawnForwardOffset;
            var rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

            PhotonNetwork.Instantiate(blueprintToCraft.Prefab.name, position, rotation);
        }
    }
}