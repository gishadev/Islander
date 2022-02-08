using Gisha.Islander.Environment;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Bow : Tool
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootForce;

        public override void PrimaryUse(Vector3 origin, Vector3 direction)
        {
            var arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
            arrow.GetComponent<Projectile>().Owner = transform;
            arrow.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}