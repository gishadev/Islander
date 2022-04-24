using Gisha.Islander.Environment;
using Gisha.Islander.UI;
using UnityEngine;

namespace Gisha.Islander.Player.Tools.RangedTools
{
    public class Bow : RangedTool
    {
        [SerializeField] private float fullChargeTime = 2f;

        private float _charge;

        protected override void InitiatePrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Press)
                _charge = 0f;

            if (interactType == InteractType.Hold)
            {
                _charge += Time.deltaTime / fullChargeTime;
                _charge = Mathf.Clamp01(_charge);
            }

            if (interactType == InteractType.Release)
            {
                var arrow = Instantiate(projectilePrefab, origin, shootPoint.rotation);

                arrow.GetComponent<Projectile>().Owner = transform.GetComponentInParent<PlayerController>();
                arrow.GetComponent<Rigidbody>().AddForce(direction * maxShootForce * _charge, ForceMode.Impulse);

                _charge = 0f;

                ResetDelay(false);
            }

            ProgressCircle.Instance.SetProgress(_charge, owner);
        }
    }
}