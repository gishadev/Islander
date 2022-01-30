using Gisha.Islander.Player;
using Photon.Pun;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Gisha.Islander.Core.Crafting
{
    public class CraftingController : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawnPrefab;
        [SerializeField] private float spawnOffset;

        private PhotonView _pv;

        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;

            if (Input.GetKeyDown(KeyCode.E))
                CraftObject(25);
        }

        private void CraftObject(int woodCost)
        {
            if (!_pv.IsMine)
                return;

            if (InventoryManager.Instance.WoodCount < woodCost)
                return;

            InventoryManager.Instance.ChangeResourceCount(ResourceType.Wood, -woodCost);
            var position = transform.position + transform.forward * spawnOffset;
            var rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

            PhotonNetwork.Instantiate(objectToSpawnPrefab.name, position, rotation);
        }
    }
}