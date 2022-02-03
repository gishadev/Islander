using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player.Tools
{
    public class ToolController : MonoBehaviour
    {
        [SerializeField] private GameObject[] toolsPrefabs;

        private Tool _selectedTool;
        private PhotonView _pv;


        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
            SelectTool(0);
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
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                _pv.RPC("SelectTool", RpcTarget.AllBuffered, 3);
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                _pv.RPC("SelectTool", RpcTarget.AllBuffered, 4);
        }

        [PunRPC]
        private void SelectTool(int index)
        {
            if (_selectedTool != null)
                Destroy(_selectedTool.gameObject);

            var hand = Camera.main.transform.GetChild(0);
            var toolGO = Instantiate(toolsPrefabs[index], hand);
            toolGO.transform.SetPositionAndRotation(hand.position, hand.rotation);

            _selectedTool = toolGO.GetComponent<Tool>();
        }
    }
}