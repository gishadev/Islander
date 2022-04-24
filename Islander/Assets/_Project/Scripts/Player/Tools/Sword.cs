using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Sword : Tool, IDamager
    {
        [SerializeField] private float maxDistance;
        [SerializeField] private float damage;
        public float Damage => damage;

        protected override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            var raycastHits = Physics.SphereCastAll(origin, 0.35f, direction, maxDistance);

            if (raycastHits.Length > 0)
            {
                for (int i = 0; i < raycastHits.Length; i++)
                {
                    var damageable = raycastHits[i].transform.GetComponentInParent<IDamageable>();
                    if (damageable != null)
                        damageable.GetDamage(this, owner);
                }
            }

            ResetDelay(true);
        }
    }
}