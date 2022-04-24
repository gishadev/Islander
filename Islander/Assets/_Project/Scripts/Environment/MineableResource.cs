using DG.Tweening;
using Gisha.Islander.Core;
using Gisha.Islander.Player;
using Gisha.Islander.Player.Tools.MeleeTools;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class MineableResource : MonoBehaviour, IDamageable
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

        public void GetDamage(IDamager damager, PlayerController owner)
        {
            transform.DOPunchScale(transform.localScale / 10f, 0.5f);

            float relDamage = damager.Damage;
            if (damager is MiningTool)
            {
                var miningTool = (MiningTool) damager;

                if (ResourceType == ResourceType.Wood)
                    relDamage = miningTool.Damage * miningTool.AxeEfficiency;
                else
                    relDamage = miningTool.Damage * miningTool.PickaxeEfficiency;
            }
            else
                relDamage *= 0.1f;

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