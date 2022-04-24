using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Player.Tools.MeleeTools
{
    public class MiningTool : MeleeTool
    {
        [SerializeField] [Range(0, 1)] private float axeEfficiency;
        [SerializeField] [Range(0, 1)] private float pickaxeEfficiency;
        public float AxeEfficiency => axeEfficiency;
        public float PickaxeEfficiency => pickaxeEfficiency;

        protected override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Press)
            {
                if (RaycastCheck(origin, direction, out var raycastHits))
                    for (var i = 0; i < raycastHits.Length; i++)
                    {
                        var damageable = raycastHits[i].transform.GetComponentInParent<IDamageable>();
                        damageable?.GetDamage(this, owner);
                    }

                ResetDelay(true);
            }
        }
    }
}