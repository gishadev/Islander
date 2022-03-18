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
            PhotonNetwork.NickName = "Player " + Random.Range(0, 99999);
        }

        public static void HostRoom()
        {
            if (!PhotonNetwork.IsConnectedAndReady)
                return;

            var random = new System.Random();
            var id = random.Next();

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 4;

            Debug.LogError(id.ToString());
            PhotonNetwork.CreateRoom(id.ToString(), roomOptions, TypedLobby.Default);
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN.");
        }

        // public override void OnJoinedRoom()
        // {
        //     // MyPhotonPlayer = PhotonNetwork.Instantiate("PhotonPlayer", Vector3.zero, Quaternion.identity, 0)
        //     //     .GetComponent<PhotonPlayer>();
        //     Debug.Log("We now in a room");
        // }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Failed to join the room: {message}");
        }
    }
}