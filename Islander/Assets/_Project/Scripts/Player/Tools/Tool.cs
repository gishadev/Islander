using System;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public abstract class Tool : MonoBehaviour
    {
        [SerializeField] private float delayInSeconds;

        public Action<bool> Equiped;

        private bool IsDelay => _currentDelay > 0f;
        private float _currentDelay;

        public virtual void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (IsDelay)
                return;
        }

        private void Update()
        {
            if (IsDelay)
                _currentDelay -= Time.deltaTime;
        }

        public void ResetDelay()
        {
            _currentDelay = delayInSeconds;
        }
    }
}