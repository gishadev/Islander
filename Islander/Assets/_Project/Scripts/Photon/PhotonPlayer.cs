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
            Debug.Log(_photonView.IsMine);
            
            if (!_photonView.IsMine)
                return;
            
            PhotonNetwork.Instantiate("Player",
                GameManager.Instance.Spawnpoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position,
                Quaternion.identity, 0);
        }
    }
}