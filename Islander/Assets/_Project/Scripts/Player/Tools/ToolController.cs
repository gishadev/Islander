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

        private void Awake()
        {
            _hotbar = FindObjectOfType<HotbarGUI>();
            _pv = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (_pv.IsMine)
                _pv.RPC("RPC_Equip", RpcTarget.AllBuffered, 0);
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;

            if (Input.GetMouseButtonDown(0))
                _equippedTool.PrimaryUse();

            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown((KeyCode) (49 + i)) && _tools.Count > i)
                {
                    _pv.RPC("RPC_Equip", RpcTarget.AllBuffered, i);
                    break;
                }
            }
        }

        public void AddTool(string toolName)
        {
            var prefab = Resources.Load($"Tools/{toolName}") as GameObject;

            _tools.Add(prefab);

            if (_pv.IsMine)
                _hotbar.AddToolGUI(prefab, _tools.Count - 1);
        }

        [PunRPC]
        private void RPC_Equip(int index)
        {
            if (_equippedTool != null)
                Destroy(_equippedTool.gameObject);

            var toolGO = Instantiate(_tools[index], handTrans);
            toolGO.transform.SetPositionAndRotation(handTrans.position, handTrans.rotation);

            _equippedTool = toolGO.GetComponent<Tool>();

            _hotbar.ToolEquipGUI(index);
        }
    }
}