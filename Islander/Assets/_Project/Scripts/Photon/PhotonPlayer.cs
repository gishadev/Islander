using Gisha.Islander.Core;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Photon
{
    public class PhotonPlayer : MonoBehaviour
    {
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (!_photonView.IsMine)
                return;
            
            PhotonNetwork.Instantiate("Player",
                GameManager.Instance.Spawnpoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position,
                Quaternion.identity, 0);
        }
    }
}