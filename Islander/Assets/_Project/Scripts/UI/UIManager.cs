using System;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private TMP_Text woodCountText;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateResourcesCount(int woodCount)
        {
            Instance.woodCountText.text = woodCount.ToString();
        }
    }
}