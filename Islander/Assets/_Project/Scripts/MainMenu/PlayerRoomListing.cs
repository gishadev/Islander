using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.MainMenu
{
    public class PlayerRoomListing : MonoBehaviour
    {
        [SerializeField] private TMP_Text playerName;
        [SerializeField] private Image readyIcon;
        
        public bool IsReady => _isReady;
        
        private bool _isReady;
        
        public void SetName(string name)
        {
            playerName.text = name;
        }

        public void SetReady(bool isReady)
        {
            if (isReady)
                readyIcon.color = Color.green;
            else
                readyIcon.color = Color.red;

            _isReady = isReady;
        }
    }
}