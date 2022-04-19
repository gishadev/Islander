using System;
using Gisha.Islander.Environment;
using Gisha.Islander.UI;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class FPSCamera : MonoBehaviourPun
    {
        [SerializeField] private Transform cameraRigTrans;
        [SerializeField] private float cameraSensitivity = 1.6f;

        [Header("GUI Raycaster")] [SerializeField]
        private GUIRaycaster guiRaycaster;

        [SerializeField] private float maxRaycastDistance;
        [SerializeField] private float sphereCastRadius = 0.25f;

        public Transform CameraRigTrans => cameraRigTrans;

        private float _xRot, _yRot;

        private void Awake()
        {
            if (photonView.IsMine)
                guiRaycaster = new GUIRaycaster(FindObjectOfType<EnvironmentHUD>(), maxRaycastDistance,
                    sphereCastRadius);
        }

        private void Start()
        {
            if (!photonView.IsMine)
            {
                Destroy(CameraRigTrans.Find("Camera").gameObject);
                Destroy(CameraRigTrans.Find("GUICamera").gameObject);
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
            if (photonView.IsMine)
                guiRaycaster.Raycast(CameraRigTrans.position, CameraRigTrans.forward);
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
    public class GUIRaycaster
    {
        public static Action<bool> TotemOpened;
        //private bool _totemOpened;

        private float _maxRaycastDistance;
        private float _sphereCastRadius = 0.25f;

        private EnvironmentHUD _environmentHUD;
        private GameObject _lastRaycastTarget;
        private MineableResource _lastMineable;

        public GUIRaycaster(EnvironmentHUD environmentHUD, float maxRaycastDistance, float sphereCastRadius)
        {
            _environmentHUD = environmentHUD;
            _maxRaycastDistance = maxRaycastDistance;
            _sphereCastRadius = sphereCastRadius;
        }

        // TODO: OPTIMIZE THIS THING (TOO LAGGY)
        public void Raycast(Vector3 origin, Vector3 direction)
        {
            Ray ray = new Ray(origin, direction);

            if (Physics.SphereCast(ray, _sphereCastRadius, out var hitInfo, _maxRaycastDistance))
            {
                if (hitInfo.collider.CompareTag("Mineable"))
                {
                    ShowHUD();

                    Vector3 hudPosition = new Vector3(hitInfo.transform.position.x, hitInfo.point.y,
                        hitInfo.transform.position.z);
                    _environmentHUD.transform.position = hudPosition;
                    _environmentHUD.transform.rotation = Quaternion.LookRotation(direction);

                    if (_lastRaycastTarget == null || _lastRaycastTarget != hitInfo.collider.gameObject)
                    {
                        _lastRaycastTarget = hitInfo.collider.gameObject;
                        _lastMineable = hitInfo.collider.GetComponentInParent<MineableResource>();
                    }

                    if (_lastMineable != null)
                    {
                        _environmentHUD.UpdateHUD(_lastMineable.ResourceType.ToString(),
                            _lastMineable.HealthPercentage);
                    }

                    return;
                }

                if (hitInfo.collider.CompareTag("Totem"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                        TotemOpened?.Invoke(true);

                    return;
                }
            }

            TotemOpened?.Invoke(false);
            HideHUD();
        }

        public void HideHUD()
        {
            _environmentHUD.Hide();
        }

        public void ShowHUD()
        {
            _environmentHUD.Show();
        }
    }
}