using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class ToolController : MonoBehaviour
    {
        [SerializeField] private Transform handTrans;

        private List<GameObject> _tools = new List<GameObject>();
        private Tool _equippedTool;
        private HotbarGUI _hotbar;
        private PhotonView _pv;
        private PlayerController _controller;

        private void Awake()
        {
            _controller = GetComponentInParent<PlayerController>();
            _hotbar = FindObjectOfType<HotbarGUI>();
            _pv = GetComponent<PhotonView>();

            if (_pv.IsMine)
                _hotbar.ResetGUI();
        }

        private void Start()
        {
            if (_pv.IsMine)
                Equip(0);
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;

            if (Input.GetMouseButtonDown(0))
                SendPrimaryUse(InteractType.Press);
            if (Input.GetMouseButton(0))
                SendPrimaryUse(InteractType.Hold);
            if (Input.GetMouseButtonUp(0))
                SendPrimaryUse(InteractType.Release);

            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown((KeyCode) (49 + i)) && _tools.Count > i)
                {
                    Equip(i);
                    break;
                }
            }
        }

        private void SendPrimaryUse(InteractType interactType)
        {
            _pv.RPC("RPC_PrimaryUse", RpcTarget.AllBuffered, _controller.FPSCamera.CameraRigTrans.position,
                _controller.FPSCamera.CameraRigTrans.forward, (int) interactType);
        }

        public void AddTool(string toolName)
        {
            var prefab = Resources.Load($"Tools/{toolName}") as GameObject;

            _tools.Add(prefab);

            if (_pv.IsMine)
                _hotbar.AddToolGUI(prefab, _tools.Count - 1);
        }

        private void Equip(int index)
        {
            _pv.RPC("RPC_Equip", RpcTarget.AllBuffered, index);
            _hotbar.ToolEquipGUI(index);
        }

        [PunRPC]
        private void RPC_PrimaryUse(Vector3 origin, Vector3 direction, int interactType)
        {
            var controller = GetComponentInParent<PlayerController>();
            _equippedTool.PrimaryUse(origin, direction, controller, (InteractType) interactType);
        }

        [PunRPC]
        private void RPC_Equip(int index)
        {
            if (_equippedTool != null)
            {
                _equippedTool.Equiped?.Invoke(false);
                Destroy(_equippedTool.gameObject);
            }

            var toolGO = Instantiate(_tools[index], handTrans);
            toolGO.transform.SetPositionAndRotation(handTrans.position, handTrans.rotation);

            _equippedTool = toolGO.GetComponent<Tool>();
            _equippedTool.Equiped?.Invoke(true);
        }
    }

    public enum InteractType
    {
        Press,
        Hold,
        Release
    }
}