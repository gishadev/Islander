using System;
using Gisha.Islander.Player;
using Gisha.Islander.Utilities;
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
            float part = transform.localScale.x / 10f;
            TweenAnimator.Scale(transform, transform.localScale.x - part, 0.5f, true);

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