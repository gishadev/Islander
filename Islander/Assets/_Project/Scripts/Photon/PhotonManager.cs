using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Gisha.Islander.Photon
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN.");

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.JoinOrCreateRoom("Hello", roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.Instantiate("PhotonPlayer", Vector3.zero, Quaternion.identity, 0);
            Debug.Log("We now in a room");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Failed to join the room: {message}");
        }
    }
}