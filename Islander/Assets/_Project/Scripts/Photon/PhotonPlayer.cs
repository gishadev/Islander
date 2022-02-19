using Gisha.Islander.Core;
using Gisha.Islander.Player;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Photon
{
    public class PhotonPlayer : MonoBehaviourPun
    {
        private PlayerController _playerController;
        private Transform _spawnpoint;

        public PlayerController PlayerController => _playerController;
        
        private void OnDisable()
        {
            if (PlayerController != null)
                PlayerController.Destroyed -= OnDestroyPlayerController;
        }

        private void Start()
        {
            if (!photonView.IsMine)
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