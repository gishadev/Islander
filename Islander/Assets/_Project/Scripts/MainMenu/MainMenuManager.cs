using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.MainMenu
{
    public class MainMenuManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject hostScreen;
        [SerializeField] private Button hostBtn, enterRoomBtn;
        [SerializeField] private TMP_InputField roomIDInput;

        private void Awake()
        {
            hostBtn.interactable = false;
            enterRoomBtn.interactable = false;
        }

        public override void OnConnectedToMaster()
        {
            hostBtn.interactable = true;
            enterRoomBtn.interactable = true;
        }

        public override void OnLeftRoom()
        {
            hostBtn.interactable = false;
            enterRoomBtn.interactable = false;
        }

        public override void OnJoinedRoom()
        {
            hostScreen.SetActive(true);
        }

        public void OnClick_Host()
        {
            if (!PhotonNetwork.IsConnectedAndReady)
                return;

            var random = new System.Random();
            string id = random.Next(0, 9999999).ToString("D7");

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 4;
            Debug.LogError(id);
            PhotonNetwork.CreateRoom(id, roomOptions, TypedLobby.Default);
        }

        public void OnClick_EnterRoom()
        {
            PhotonNetwork.JoinRoom(roomIDInput.text);
        }

        public void OnClick_Quit()
        {
            Application.Quit();
        }
    }
}