using System;
using Gisha.Islander.Core;
using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Totem : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;

        public float HealthPercentage => _health / maxHealth;
        private float _health;

        private void Awake()
        {
            _health = maxHealth;
        }

        public void GetDamage(PlayerController owner, float damage)
        {
            _health -= damage;

            if (_health <= 0)
                Debug.Log("Totem was destroyed");
        }
    }
}