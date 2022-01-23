using UnityEngine;

namespace Gisha.Islander.Character
{
    public class FPSCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTrans;
        [SerializeField] private float cameraSensitivity = 1.6f;

        private float _xRot, _yRot;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            var mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.fixedDeltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.fixedDeltaTime;

            ApplyCameraRotation(mouseY);
            ApplyBodyRotation(mouseX);
        }

        private void ApplyCameraRotation(float deltaY)
        {
            _xRot -= deltaY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);
            cameraTrans.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
        }

        private void ApplyBodyRotation(float deltaX)
        {
            _yRot = deltaX;
            transform.Rotate(Vector3.up * _yRot);
        }
    }
}