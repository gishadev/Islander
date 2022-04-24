using Gisha.Islander.Core;
using UnityEngine;

namespace Gisha.Islander.Player.Tools.MeleeTools
{
    public abstract class MeleeTool : Tool, IDamager
    {
        [SerializeField] protected float damage;
        [Header("Raycast")] [SerializeField] protected float raycastDistance = 2f;
        [SerializeField] protected float raycastRadius = 0.35f;

        public float Damage => damage;

        protected abstract override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType);
        
        protected bool RaycastCheck(Vector3 origin, Vector3 direction, out RaycastHit[] raycastHits)
        {
            raycastHits = Physics.SphereCastAll(origin, raycastRadius, direction, raycastDistance);
            return raycastHits.Length > 0;
        }
    }
}