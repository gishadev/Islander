using Gisha.Islander.Character;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Gisha.Islander.Core
{
    public class ObjectsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawnPrefab;
        [SerializeField] private float spawnOffset;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                CraftObject(25);
        }

        private void CraftObject(int woodCost)
        {
            if (InventoryManager.Instance.WoodCount < woodCost)
                return;

            InventoryManager.Instance.ChangeWoodCount(-woodCost);
            var position = transform.position + transform.forward * spawnOffset;
            var rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

            Instantiate(objectToSpawnPrefab, position, rotation);
        }
    }
}