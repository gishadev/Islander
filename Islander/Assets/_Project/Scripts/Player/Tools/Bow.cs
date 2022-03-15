using Gisha.Islander.Environment;
using Gisha.Islander.UI;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Bow : Tool
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float maxShootForce;

        private float _charge;

        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Press)
                _charge = 0f;

            if (interactType == InteractType.Hold)
            {
                _charge += Time.deltaTime / 2f;
                _charge = Mathf.Clamp01(_charge);
            }

            if (interactType == InteractType.Release)
            {
                var arrow = Instantiate(arrowPrefab, origin, shootPoint.rotation);

                arrow.GetComponent<Projectile>().Owner = transform;
                arrow.GetComponent<Rigidbody>().AddForce(direction * maxShootForce * _charge, ForceMode.Impulse);

                _charge = 0f;
            }

            ProgressCircle.Instance.SetProgress(_charge);
        }
    }
}