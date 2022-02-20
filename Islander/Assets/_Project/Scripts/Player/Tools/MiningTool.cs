using Gisha.Islander.Environment;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class MiningTool : Tool
    {
        [SerializeField] private float maxDistance;

        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner)
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.red, 0.25f);

            var raycastHits = Physics.SphereCastAll(origin, 0.35f, direction, maxDistance);

            if (raycastHits.Length > 0)
            {
                for (int i = 0; i < raycastHits.Length; i++)
                {
                    if (raycastHits[i].collider.CompareTag("Mineable"))
                        raycastHits[i].collider.GetComponentInParent<IMineable>().Mine(owner);
                    if (raycastHits[i].collider.CompareTag("Player") &&
                        !transform.IsChildOf(raycastHits[i].collider.transform))
                        raycastHits[i].collider.GetComponent<PlayerController>().GetDamage(21);
                }
            }
        }
    }
}