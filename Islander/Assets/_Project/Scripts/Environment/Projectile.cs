using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float damage = 25f;
        [SerializeField] private float raycastDistance = 0.5f;
        public Transform Owner { get; set; }

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Invoke("Destroy", 60f);
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(_rb.velocity.normalized);

            Raycast();
        }

        private void Raycast()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, raycastDistance))
            {
                if (hitInfo.collider.CompareTag("Player") && !Owner.IsChildOf(hitInfo.transform) ||
                    hitInfo.collider.CompareTag("Raft"))
                    hitInfo.collider.GetComponent<IDamageable>().GetDamage(damage);

                Destroy(gameObject);
            }
        }
        
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * raycastDistance);
        }
    }
}