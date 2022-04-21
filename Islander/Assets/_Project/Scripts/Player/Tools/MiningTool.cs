using Gisha.Islander.Core;
using Gisha.Islander.Environment;
using Gisha.Islander.Utilities;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class MiningTool : Tool
    {
        [SerializeField] private float maxDistance;
        [SerializeField] private float damage;
        [SerializeField] [Range(0, 1)] private float axeEfficiency;
        [SerializeField] [Range(0, 1)] private float pickaxeEfficiency;

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
                        var damageable = raycastHits[i].transform.GetComponentInParent<IDamageable>();
                        damageable?.GetDamage(owner, damage);
                    }
                }

                ResetDelay(true);
            }
        }
    }
}