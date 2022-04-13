using DG.Tweening;
using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class MineableResource : MonoBehaviour, IMineable
    {
        [SerializeField] private ResourceType resourceResourceType;
        [SerializeField] private int resourcesToGather = 5;
        [SerializeField] private float maxHealth = 5;

        private float _health;

        public ResourceType ResourceType => resourceResourceType;
        public float HealthPercentage => _health / maxHealth;

        private void Awake()
        {
            _health = maxHealth;
        }

        public void Mine(PlayerController owner, float damage, float pickaxeEfficiency, float axeEfficiency)
        {
            transform.DOPunchScale(transform.localScale / 10f, 0.5f);

            float relDamage;
            if (ResourceType == ResourceType.Wood)
                relDamage = damage * axeEfficiency;
            else
                relDamage = damage * pickaxeEfficiency;

            _health -= relDamage;
            if (_health <= 0)
                Gather(owner);
        }

        private void Gather(PlayerController owner)
        {
            owner.InventoryManager.ChangeResourceCount(ResourceType, resourcesToGather);

            Destroy(gameObject);
        }

        // [PunRPC]
        // private void RPC_Destroy()
        // {
        //     Destroy(gameObject);
        // }
    }
}