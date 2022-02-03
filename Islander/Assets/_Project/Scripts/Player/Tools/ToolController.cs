using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class ToolController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tools = new List<GameObject>();

        private Tool _equippedTool;
        private PhotonView _pv;

        public List<GameObject> Tools
        {
            get => tools;
            set => tools = value;
        }


        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
            Equip(0);
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;

            if (Input.GetMouseButtonDown(0))
                _equippedTool.PrimaryUse();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                _pv.RPC("Equip", RpcTarget.AllBuffered, 0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                _pv.RPC("Equip", RpcTarget.AllBuffered, 1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                _pv.RPC("Equip", RpcTarget.AllBuffered, 2);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                _pv.RPC("Equip", RpcTarget.AllBuffered, 3);
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                _pv.RPC("Equip", RpcTarget.AllBuffered, 4);
        }

        [PunRPC]
        private void Equip(int index)
        {
            if (_equippedTool != null)
                Destroy(_equippedTool.gameObject);

            var hand = Camera.main.transform.GetChild(0);
            var toolGO = Instantiate(Tools[index], hand);
            toolGO.transform.SetPositionAndRotation(hand.position, hand.rotation);

            _equippedTool = toolGO.GetComponent<Tool>();
        }
    }
}