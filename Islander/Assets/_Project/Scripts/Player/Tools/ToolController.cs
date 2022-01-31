using System;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class ToolController : MonoBehaviour
    {
        [SerializeField] private Tool[] tools;

        private Tool _selectedTool;
        private PhotonView _pv;


        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
            _selectedTool = tools[0];
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;

            if (Input.GetMouseButtonDown(0))
                _selectedTool.PrimaryUse();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                _pv.RPC("SelectTool", RpcTarget.AllBuffered, 0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                _pv.RPC("SelectTool", RpcTarget.AllBuffered, 1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                _pv.RPC("SelectTool", RpcTarget.AllBuffered, 2);
        }

        [PunRPC]
        private void SelectTool(int index)
        {
            _selectedTool.gameObject.SetActive(false);
            _selectedTool = tools[index];
            _selectedTool.gameObject.SetActive(true);
        }
    }
}