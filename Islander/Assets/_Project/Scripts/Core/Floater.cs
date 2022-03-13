using UnityEngine;

namespace Gisha.Islander.Core
{
    public class Floater : MonoBehaviour
    {
        [SerializeField] private float waterLevel = 0f;

        private bool _isOnWater;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            ApplyDefaultSettings();
        }

        private void FixedUpdate()
        {
            if (transform.position.y < waterLevel)
            {
                FloatOnWater();

                if (!_isOnWater)
                    ApplyWaterSettings();
            }
            else if (_isOnWater)
                ApplyDefaultSettings();
        }

        private void FloatOnWater()
        {
            _rb.position = new Vector3(_rb.position.x, waterLevel, _rb.position.z);
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }

        private void ApplyWaterSettings()
        {
            _isOnWater = true;
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void ApplyDefaultSettings()
        {
            _isOnWater = false;
            _rb.freezeRotation = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            var center = new Vector3(transform.position.x, waterLevel, transform.position.z);
            var size = new Vector3(5f, 0.15f, 5f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}