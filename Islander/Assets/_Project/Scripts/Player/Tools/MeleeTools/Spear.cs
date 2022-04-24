using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Player.Tools.MeleeTools
{
    public class Spear : MeleeTool
    {
        protected override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Press)
            {
                if (RaycastCheck(origin, direction, out var raycastHits))
                    for (var i = 0; i < raycastHits.Length; i++)
                    {
                        var damageable = raycastHits[i].transform.GetComponentInParent<IDamageable>();
                        if (damageable != null)
                            damageable.GetDamage(this, owner);
                    }

                ResetDelay(true);
            }
        }
    }
}