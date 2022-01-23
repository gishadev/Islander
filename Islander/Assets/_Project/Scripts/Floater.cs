using UnityEngine;

namespace Gisha.Islander
{
    public class Floater : MonoBehaviour
    {
        [SerializeField] private Transform[] actionPoints;
        
        [SerializeField] private float waterLevel = 4f;
        [SerializeField] private float floatHeight = 2f;
        [SerializeField] private float waterDensity = 0.05f;
        [SerializeField] private float downForce;
        
        
        private float _forceFactor;
        private Vector3 _floatForce;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            for (int i = 0; i < actionPoints.Length; i++)
            {
                _forceFactor = 1f - (actionPoints[i].position.y - waterLevel) / floatHeight;

                if (_forceFactor > 0f)
                {
                    _floatForce = -Physics.gravity * (_forceFactor - _rb.velocity.y * waterDensity) / actionPoints.Length;
                    _floatForce -= Vector3.up * downForce;
                    
                    _rb.AddForceAtPosition(_floatForce, actionPoints[i].position);
                }
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var actionPoint in actionPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(actionPoint.position, 0.2f);
            }

            Gizmos.color = Color.blue;
            var center = new Vector3(transform.position.x, waterLevel, transform.position.z);
            var size = new Vector3(5f, 0.15f, 5f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}