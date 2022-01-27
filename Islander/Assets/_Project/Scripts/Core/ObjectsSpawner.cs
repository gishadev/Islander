using Gisha.Islander.Character;
using Photon.Pun;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Gisha.Islander.Core
{
    public class ObjectsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawnPrefab;
        [SerializeField] private float spawnOffset;

        private PhotonView _pv;

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

            InventoryManager.Instance.ChangeWoodCount(-woodCost);
            var position = transform.position + transform.forward * spawnOffset;
            var rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

            PhotonNetwork.Instantiate(objectToSpawnPrefab.name, position, rotation);
        }
    }
}