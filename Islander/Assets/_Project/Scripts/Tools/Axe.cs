using Gisha.Islander.Environment;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Tools
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private float maxDistance;

        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (!_photonView.IsMine)
                return;

            if (Input.GetMouseButtonDown(0))
                Attack();
        }
        
        private void Attack()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 0.25f);

            if (Physics.Raycast(ray, out var raycastHit, maxDistance))
            {
                if (raycastHit.collider.CompareTag("Mineable"))
                {
                    Debug.Log("Mine");
                    raycastHit.collider.GetComponent<IMineable>().Mine();
                }
            }
        }
    }
}