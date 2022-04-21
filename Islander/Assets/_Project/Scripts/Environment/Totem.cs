using Gisha.Islander.Core;
using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Totem : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private Transform spawnpoint;

        public Vector3 SpawnPosition => spawnpoint.position;
        public float HealthPercentage => _health / maxHealth;

        private float _health;
        private PlayerController _owner;

        private void Awake()
        {
            _health = maxHealth;
        }

        public void Initialize(PlayerController owner)
        {
            _owner = owner;
        }

        public void GetDamage(IDamager damager, PlayerController owner)
        {
            if (owner == _owner)
                return;

            _health -= damager.Damage;

            if (_health <= 0)
                Debug.Log("Totem was destroyed");
        }
    }
}