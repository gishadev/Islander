using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Player.Tools.MeleeTools
{
    public class Paddle : MeleeTool
    {
        protected override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Hold)
            {
                if (RaycastCheck(origin, direction, out var raycastHits))
                    for (var i = 0; i < raycastHits.Length; i++)
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

                ResetDelay(true);
            }
        }
    }
}