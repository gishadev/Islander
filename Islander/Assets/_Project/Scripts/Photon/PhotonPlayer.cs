using Gisha.Islander.Core;
using Gisha.Islander.Player;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Photon
{
    public class PhotonPlayer : MonoBehaviour
    {
        private PlayerController _playerController;
        private Transform _spawnpoint;
        private PhotonView _photonView;

        public PlayerController PlayerController => _playerController;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }
        
        private void OnDisable()
        {
            if (PlayerController != null)
                PlayerController.Destroyed -= OnDestroyPlayerController;
        }

        private void Start()
        {
            if (!_photonView.IsMine)
                return;

            _spawnpoint = GameManager.Instance.Spawnpoints[PhotonNetwork.CurrentRoom.PlayerCount - 1];
            Respawn();
            
            PlayerController.Destroyed += OnDestroyPlayerController;
        }

        private void OnDestroyPlayerController()
        {
            PlayerController.Destroyed -= OnDestroyPlayerController;
            
            PhotonNetwork.Destroy(PlayerController.gameObject);
            Respawn();
        }

        private void Respawn()
        {
            _playerController = PhotonNetwork.Instantiate("Player", _spawnpoint.position, Quaternion.identity, 0)
                .GetComponent<PlayerController>();

            PlayerController.Destroyed += OnDestroyPlayerController;
        }
    }
}