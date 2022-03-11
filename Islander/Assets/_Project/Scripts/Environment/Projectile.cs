using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Environment
{
    public class Projectile : MonoBehaviour
    {
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
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player") && !Owner.IsChildOf(other.transform))
                other.collider.GetComponent<PlayerController>().GetDamage(30);

            Destroy(gameObject);
        }
    }
}