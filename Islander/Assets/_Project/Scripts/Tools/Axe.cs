using Gisha.Islander.Environment;
using UnityEngine;

namespace Gisha.Islander.Tools
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] private float maxDistance;

        private void Update()
        {
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