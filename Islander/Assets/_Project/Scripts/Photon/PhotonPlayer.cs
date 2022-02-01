using System;
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

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }
        
        private void OnDisable()
        {
            _playerController.Destroyed -= Respawn;
        }

        private void Start()
        {
            if (!_photonView.IsMine)
                return;

            _spawnpoint = GameManager.Instance.Spawnpoints[PhotonNetwork.CurrentRoom.PlayerCount - 1];
            Respawn();
        }

        private void Respawn()
        {
            _playerController = PhotonNetwork.Instantiate("Player", _spawnpoint.position, Quaternion.identity, 0)
                .GetComponent<PlayerController>();
            
            _playerController.Destroyed += Respawn;
        }
    }
}