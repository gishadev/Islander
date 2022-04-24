using UnityEngine;

namespace Gisha.Islander.Player.Tools.RangedTools
{
    public abstract class RangedTool : Tool
    {
        [Header("Ranged")] [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] protected Transform shootPoint;
        [SerializeField] protected float maxShootForce;

        protected abstract override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType);
    }
}