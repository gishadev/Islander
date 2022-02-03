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

            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown((KeyCode) (49 + i)) && tools.Count > i)
                {
                    _pv.RPC("Equip", RpcTarget.AllBuffered, i);
                    break;
                }
            }
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