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

            if (Physics.Raycast(ray, out var raycastHit, maxDistance))
            {
                if (raycastHit.collider.CompareTag("Mineable")) 
                    raycastHit.collider.GetComponent<IMineable>().Mine();
            }
        }
    }
}