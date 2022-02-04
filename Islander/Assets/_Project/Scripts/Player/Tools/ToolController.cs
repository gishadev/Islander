using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class ToolController : MonoBehaviour
    {
        private List<GameObject> _tools = new List<GameObject>();
        private Tool _equippedTool;
        private PhotonView _pv;
        private HotbarGUI _hotbar;

        private void Awake()
        {
            _hotbar = FindObjectOfType<HotbarGUI>();
            _pv = GetComponent<PhotonView>();
        }

        private void Start()
        {
            Equip(0);
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
                    _pv.RPC("Equip", RpcTarget.AllBuffered, i);
                    break;
                }
            }
        }

        public void AddTool(GameObject toolPrefab)
        {
            _tools.Add(toolPrefab);

            _hotbar.AddToolGUI(toolPrefab, _tools.Count - 1);
        }

        [PunRPC]
        private void Equip(int index)
        {
            if (_equippedTool != null)
                Destroy(_equippedTool.gameObject);

            var hand = Camera.main.transform.GetChild(0);
            var toolGO = Instantiate(_tools[index], hand);
            toolGO.transform.SetPositionAndRotation(hand.position, hand.rotation);

            _equippedTool = toolGO.GetComponent<Tool>();

            _hotbar.ToolEquipGUI(index);
        }
    }
}