using System;
using System.Collections;
using Gisha.Islander.UI;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public abstract class Tool : MonoBehaviour
    {
        [SerializeField] private float delayInSeconds;

        public Action<bool> Equiped;

        private bool _isDelay;
        private float _currentDelay;

        public void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (_isDelay)
                return;

            InitiatePrimaryUse(origin, direction, owner, interactType);
        }

        protected abstract void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType);

        private IEnumerator DelayCoroutine(bool updateProgressCircle)
        {
            _isDelay = true;
            _currentDelay = delayInSeconds;

            while (_currentDelay > 0)
            {
                yield return null;
                _currentDelay -= Time.deltaTime;

                if (updateProgressCircle)
                    ProgressCircle.Instance.SetProgress(_currentDelay / delayInSeconds);
            }

            _isDelay = false;
        }

        public void ResetDelay(bool updateProgressCircle)
        {
            StartCoroutine(DelayCoroutine(updateProgressCircle));
        }
    }
}