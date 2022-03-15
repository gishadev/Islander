using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class Paddle : Tool
    {
        [SerializeField] private float maxDistance;

        public override void PrimaryUse(Vector3 origin, Vector3 direction, PlayerController owner,
            InteractType interactType)
        {
            if (interactType == InteractType.Press)
            {
                Debug.DrawRay(origin, direction * maxDistance, Color.red, 0.25f);

                var raycastHits = Physics.SphereCastAll(origin, 0.35f, direction, maxDistance);

                if (raycastHits.Length > 0)
                {
                    for (int i = 0; i < raycastHits.Length; i++)
                    {
                        var col = raycastHits[i].collider;

                        if (col.CompareTag("Player") &&
                            !transform.IsChildOf(col.transform))
                        {
                            col.GetComponent<PlayerController>().GetDamage(5);
                            col.GetComponent<Rigidbody>().AddForce(direction * 25f, ForceMode.Impulse);
                        }

                        if (col.CompareTag("Raft"))
                            col.GetComponent<Rigidbody>().AddForce(direction * 5f, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}