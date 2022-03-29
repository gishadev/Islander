using System;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class FPSCamera : MonoBehaviourPun
    {
        [SerializeField] private Transform cameraRigTrans;
        [SerializeField] private float cameraSensitivity = 1.6f;
        [Space] [SerializeField] private EnvironmentHUDController _hudController;


        private float _xRot, _yRot;

        public Transform CameraRigTrans => cameraRigTrans;

        private void Start()
        {
            if (!photonView.IsMine)
            {
                Destroy(CameraRigTrans.Find("Camera").gameObject);
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine)
                return;

            var mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.fixedDeltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.fixedDeltaTime;

            ApplyCameraRotation(mouseY);
            ApplyBodyRotation(mouseX);
        }

        private void Update()
        {
            _hudController.Raycast(CameraRigTrans.position, CameraRigTrans.forward);
        }

        private void ApplyCameraRotation(float deltaY)
        {
            _xRot -= deltaY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);
            CameraRigTrans.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
        }

        private void ApplyBodyRotation(float deltaX)
        {
            _yRot = deltaX;
            transform.Rotate(Vector3.up * _yRot);
        }
    }

    [Serializable]
    public class EnvironmentHUDController
    {
        [SerializeField] private GameObject environmentHUDCanvas;
        [SerializeField] private float maxRaycastDistance;

        public void Raycast(Vector3 origin, Vector3 direction)
        {
            if (Physics.Raycast(origin, direction, out var hitInfo, maxRaycastDistance))
            {
                if (hitInfo.collider.CompareTag("Mineable"))
                {
                    environmentHUDCanvas.SetActive(true);
                    Vector3 hudPosition = new Vector3(hitInfo.transform.position.x, hitInfo.point.y,
                        hitInfo.transform.position.z);
                    
                    environmentHUDCanvas.transform.position = hudPosition;
                    environmentHUDCanvas.transform.rotation = Quaternion.LookRotation(direction);
                    return;
                }
            }


            environmentHUDCanvas.SetActive(false);
        }
    }
}