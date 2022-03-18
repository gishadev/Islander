using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Gisha.Islander.Photon
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        public static PhotonPlayer MyPhotonPlayer;

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
            MyPhotonPlayer = PhotonNetwork.Instantiate("PhotonPlayer", Vector3.zero, Quaternion.identity, 0)
                .GetComponent<PhotonPlayer>();
            Debug.Log("We now in a room");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Failed to join the room: {message}");
        }
    }
}