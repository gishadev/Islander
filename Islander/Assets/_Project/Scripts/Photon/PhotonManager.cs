using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Gisha.Islander.Photon
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        private static PhotonManager Instance { get; set; }

        public static PhotonPlayer MyPhotonPlayer;

        private void Awake()
        {
            CreateInstance();

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NickName = "Player " + Random.Range(0, 99999);
        }

        public override void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;
            else
            {
                if (Instance != this)
                    Destroy(gameObject);
            }
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
        {
            if (arg0.name == "Game")
                MyPhotonPlayer = PhotonNetwork.Instantiate("PhotonPlayer", Vector3.zero, Quaternion.identity, 0)
                    .GetComponent<PhotonPlayer>();
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