using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.MainMenu
{
    public class HostRoomMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject hostRoomScreen;
        [SerializeField] private Transform playerRoomListingParent;
        [SerializeField] private GameObject playerRoomListingPrefab;

        private Dictionary<int, GameObject> _playerListEntries = new Dictionary<int, GameObject>();
        
        public void OnClick_CopyID()
        {
        }

        public void OnClick_Ready()
        {
        }

        public void OnClick_Close()
        {
            hostRoomScreen.SetActive(false);
            PhotonNetwork.LeaveRoom();
        }

        public override void OnJoinedRoom()
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                CreatePlayerListing(PhotonNetwork.PlayerList[i]);
        }
        
        public override void OnLeftRoom()
        {
            foreach (GameObject entry in _playerListEntries.Values)
                Destroy(entry.gameObject);

            _playerListEntries.Clear();
        }

        public override void OnPlayerEnteredRoom(global::Photon.Realtime.Player newPlayer)
        {
            CreatePlayerListing(newPlayer);
        }

        public override void OnPlayerLeftRoom(global::Photon.Realtime.Player otherPlayer)
        {
            Destroy(_playerListEntries[otherPlayer.ActorNumber]);

            _playerListEntries.Remove(otherPlayer.ActorNumber);
        }

        private void CreatePlayerListing(global::Photon.Realtime.Player player)
        {
            var playerListing = Instantiate(playerRoomListingPrefab, playerRoomListingParent)
                .GetComponent<PlayerRoomListing>();
            playerListing.SetName(player.NickName);
            playerListing.SetReady(false);

            _playerListEntries.Add(player.ActorNumber, playerListing.gameObject);
        }
    }
}