using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float damage = 25f;
        [SerializeField] private float raycastDistance = 0.5f;
        [SerializeField] private float raycastRadius = 0.15f;
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
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.SphereCast(ray, raycastRadius, out var hitInfo, raycastDistance))
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
            Gizmos.DrawWireSphere(transform.position, raycastRadius);
        }
    }
}