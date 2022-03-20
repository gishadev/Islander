using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.MainMenu
{
    public class RoomMenuGUI : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject hostRoomScreen;
        [SerializeField] private Transform playerRoomListingParent;
        [SerializeField] private GameObject playerRoomListingPrefab;
        [Space] [SerializeField] private TMP_Text readyBtnText;
        [SerializeField] private TMP_Text copyIdBtnText;

        private Dictionary<int, GameObject> _playerListEntries = new Dictionary<int, GameObject>();

        #region OnClick Entries

        public void OnClick_CopyID()
        {
            var te = new TextEditor();
            te.text = PhotonNetwork.CurrentRoom.Name;
            te.SelectAll();
            te.Copy();
        }

        public void OnClick_Ready()
        {
            if (_playerListEntries.TryGetValue(PhotonNetwork.LocalPlayer.ActorNumber, out var listingObj))
            {
                var playerListing = listingObj.GetComponent<PlayerRoomListing>();
                playerListing.SetReady(!playerListing.IsReady);

                var props = new Hashtable() {{"IsPlayerReady", playerListing.IsReady}};
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                readyBtnText.text = !playerListing.IsReady ? "Ready" : "Unready";
            }
        }

        public void OnClick_Close()
        {
            hostRoomScreen.SetActive(false);
            PhotonNetwork.LeaveRoom();
        }

        #endregion

        public override void OnJoinedRoom()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                CreatePlayerListing(PhotonNetwork.PlayerList[i]);

            copyIdBtnText.text = PhotonNetwork.CurrentRoom.Name;
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.AutomaticallySyncScene = false;

            foreach (GameObject entry in _playerListEntries.Values)
                Destroy(entry.gameObject);

            _playerListEntries.Clear();
        }

        public override void OnPlayerEnteredRoom(global::Photon.Realtime.Player newPlayer)
        {
            CreatePlayerListing(newPlayer);

            ResetPlayersReady();
        }

        public override void OnPlayerLeftRoom(global::Photon.Realtime.Player otherPlayer)
        {
            Destroy(_playerListEntries[otherPlayer.ActorNumber]);
            _playerListEntries.Remove(otherPlayer.ActorNumber);

            ResetPlayersReady();
        }

        public override void OnPlayerPropertiesUpdate(global::Photon.Realtime.Player targetPlayer,
            Hashtable changedProps)
        {
            if (_playerListEntries.TryGetValue(targetPlayer.ActorNumber, out var playerListing))
            {
                if (changedProps.TryGetValue("IsPlayerReady", out var isPlayerReady))
                    playerListing.GetComponent<PlayerRoomListing>().SetReady((bool) isPlayerReady);
            }

            if (CheckPlayersReady())
                PhotonNetwork.LoadLevel("Game");
        }

        private void CreatePlayerListing(global::Photon.Realtime.Player player)
        {
            var playerListing = Instantiate(playerRoomListingPrefab, playerRoomListingParent)
                .GetComponent<PlayerRoomListing>();

            playerListing.SetName(player.NickName);
            playerListing.SetReady(false);

            var initialProps = new Hashtable() {{"IsPlayerReady", playerListing.IsReady}};
            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);

            _playerListEntries.Add(player.ActorNumber, playerListing.gameObject);
        }

        private bool CheckPlayersReady()
        {
            foreach (global::Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            {
                if (p.CustomProperties.TryGetValue("IsPlayerReady", out var isPlayerReady))
                {
                    if (!(bool) isPlayerReady)
                        return false;
                }
                else
                    return false;
            }

            return true;
        }

        private void ResetPlayersReady()
        {
            foreach (GameObject entry in _playerListEntries.Values)
                entry.GetComponent<PlayerRoomListing>().SetReady(false);
        }
    }
}