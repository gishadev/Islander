using System;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Player Settings")] [SerializeField]
        private int maxHealth = 100;

        [Header("Other")] [SerializeField] private float swimmingDamagePerSecond = 10;

        private float _health;
        private FPSMover _fpsMover;

        private void Awake()
        {
            _fpsMover = GetComponent<FPSMover>();
        }

        private void Start()
        {
            _health = maxHealth;
        }

        private void Update()
        {
            if (_fpsMover.IsSwimming)
                GetDamage(swimmingDamagePerSecond * Time.deltaTime);
        }

        public void GetDamage(float dmg)
        {
            _health -= dmg;

            if (_health < 0)
            {
                _health = 0;

                Debug.Log($"<color=red>{gameObject.name} is dead now :c</color>");
            }

            Debug.Log($"Current health: {_health}");
        }
    }
}