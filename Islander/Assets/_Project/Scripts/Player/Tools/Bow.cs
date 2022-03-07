using System;
using Gisha.Islander.Environment;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Bow : Tool
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootForce;

        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner)
        {
            var arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
            var shootDir = direction;

            // Finding hit point to shoot correctly into crosshair.
            if (Physics.Raycast(origin, direction, out var hitInfo))
                shootDir = (hitInfo.point - shootPoint.position).normalized;
            
            arrow.GetComponent<Projectile>().Owner = transform;
            arrow.GetComponent<Rigidbody>().AddForce(shootDir * shootForce, ForceMode.Impulse);
        }
    }
}