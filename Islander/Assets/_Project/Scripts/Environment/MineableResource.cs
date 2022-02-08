using Gisha.Islander.Player;
using Gisha.Islander.Utilities;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class MineableResource : MonoBehaviour, IMineable
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int resourcesToGather = 5;

        private int _health = 5;
        
        public void Mine()
        {
            float part = transform.localScale.x / 10f;
            TweenAnimator.Scale(transform, transform.localScale.x - part, 0.5f, true);
            _health--;
            if (_health <= 0)
                Gather();
        }

        private void Gather()
        {
            InventoryManager.Instance.ChangeResourceCount(resourceType, resourcesToGather);
            Destroy(gameObject);
        }

        // [PunRPC]
        // private void RPC_Destroy()
        // {
        //     Destroy(gameObject);
        // }
    }
}