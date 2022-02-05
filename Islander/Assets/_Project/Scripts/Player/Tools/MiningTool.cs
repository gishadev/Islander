using Gisha.Islander.Environment;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class MiningTool : Tool
    {
        [SerializeField] private float maxDistance;

        public override void PrimaryUse()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 0.25f);

            var raycastHits = Physics.SphereCastAll(ray.origin, 0.35f, ray.direction, maxDistance);

            if (raycastHits.Length > 0)
            {
                for (int i = 0; i < raycastHits.Length; i++)
                {
                    if (raycastHits[i].collider.CompareTag("Mineable"))
                        raycastHits[i].collider.GetComponent<IMineable>().Mine();
                }
            }
        }
    }
}