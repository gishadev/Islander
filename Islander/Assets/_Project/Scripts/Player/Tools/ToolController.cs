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
                SelectTool(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SelectTool(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                SelectTool(2);
        }

        private void SelectTool(int index)
        {
            _selectedTool.gameObject.SetActive(false);
            _selectedTool = tools[index];
            _selectedTool.gameObject.SetActive(true);
        }
    }
}