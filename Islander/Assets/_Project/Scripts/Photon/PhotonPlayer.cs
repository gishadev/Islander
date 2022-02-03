using System;
using Gisha.Islander.Core;
using Gisha.Islander.Player;
using Gisha.Islander.UI;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Photon
{
    public class PhotonPlayer : MonoBehaviour
    {
        private PlayerController _playerController;
        private Transform _spawnpoint;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void OnEnable()
        {
            _playerController.Destroyed += OnDestroyPlayerController;
        }

        private void OnDisable()
        {
            if (_playerController != null)
                _playerController.Destroyed -= OnDestroyPlayerController;
        }

        private void Start()
        {
            if (!_photonView.IsMine)
                return;

            _spawnpoint = GameManager.Instance.Spawnpoints[PhotonNetwork.CurrentRoom.PlayerCount - 1];
            Respawn();
        }

        private void OnDestroyPlayerController()
        {
            _playerController.Destroyed -= OnDestroyPlayerController;
            
            PhotonNetwork.Destroy(_playerController.gameObject);
            Respawn();
        }

        private void Respawn()
        {
            _playerController = PhotonNetwork.Instantiate("Player", _spawnpoint.position, Quaternion.identity, 0)
                .GetComponent<PlayerController>();

            _playerController.Destroyed += OnDestroyPlayerController;
        }
    }
}