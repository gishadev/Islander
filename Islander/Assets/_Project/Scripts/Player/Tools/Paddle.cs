using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Paddle : Tool, IDamager
    {
        [SerializeField] private float maxDistance;
        [SerializeField] private float damage = 5;
        public float Damage => damage;

        protected override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Press)
            {
                Debug.DrawRay(origin, direction * maxDistance, Color.red, 0.25f);

                var raycastHits = Physics.SphereCastAll(origin, 0.35f, direction, maxDistance);

                if (raycastHits.Length > 0)
                {
                    for (int i = 0; i < raycastHits.Length; i++)
                    {
                        var col = raycastHits[i].collider;

                        if (col.CompareTag("Raft"))
                            col.GetComponent<Rigidbody>().AddForce(direction * 5f, ForceMode.Impulse);
                        else if (col.CompareTag("Player") && !transform.IsChildOf(col.transform))
                            col.GetComponent<Rigidbody>().AddForce(direction * 25f, ForceMode.Impulse);
                        else
                        {
                            var damageable = raycastHits[i].transform.GetComponentInParent<IDamageable>();
                            if (damageable != null)
                                damageable.GetDamage(this, owner);
                        }
                    }
                }

                ResetDelay(true);
            }
        }
    }
}