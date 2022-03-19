using Gisha.Islander.Photon;
using Photon.Pun;
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
            Debug.LogError(PhotonNetwork.CurrentRoom.Name);
        }

        public void OnClick_Host()
        {
            PhotonManager.HostRoom();
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