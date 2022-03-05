using Gisha.Islander.Photon;
using Gisha.Islander.Player;
using Gisha.Islander.Utilities;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class MineableResource : MonoBehaviour, IMineable
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int resourcesToGather = 5;

        private float _health = 5;

        public void Mine(PlayerController owner, float damage, float pickaxeEfficiency, float axeEfficiency)
        {
            float part = transform.localScale.x / 10f;
            TweenAnimator.Scale(transform, transform.localScale.x - part, 0.5f, true);

            float relDamage;
            if (resourceType == ResourceType.Wood)
                relDamage = damage * axeEfficiency;
            else
                relDamage = damage * pickaxeEfficiency;

            _health -= relDamage;
            if (_health <= 0)
                Gather(owner);
        }

        private void Gather(PlayerController owner)
        {
            owner.InventoryManager.ChangeResourceCount(resourceType, resourcesToGather);

            Destroy(gameObject);
        }

        // [PunRPC]
        // private void RPC_Destroy()
        // {
        //     Destroy(gameObject);
        // }
    }
}