using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.MainMenu
{
    public class HostRoomMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform playerRoomListingParent;
        [SerializeField] private GameObject playerRoomListingPrefab;


        public void OnClick_CopyID()
        {
        }

        public void OnClick_Ready()
        {
        }

        public override void OnEnable()
        {
            for (int i = 0; i < playerRoomListingParent.childCount; i++)
                Destroy(playerRoomListingParent.GetChild(i).gameObject);

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                CreatePlayerListing(PhotonNetwork.PlayerList[i]);
        }

        public void OnClick_Close()
        {
            gameObject.SetActive(false);
            PhotonNetwork.LeaveRoom();
        }

        private void CreatePlayerListing(global::Photon.Realtime.Player player)
        {
            var playerListing = Instantiate(playerRoomListingPrefab, playerRoomListingParent)
                .GetComponent<PlayerRoomListing>();
            playerListing.SetName(player.NickName);
            playerListing.SetReady(false);
        }
    }
}