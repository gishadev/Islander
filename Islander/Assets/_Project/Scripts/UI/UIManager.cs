using System;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager Instance { get; set; }

        [SerializeField] private TMP_Text woodCountText;

        private void Awake()
        {
            Instance = this;
        }

        public static void UpdateResourcesCount(int woodCount)
        {
            Instance.woodCountText.text = woodCount.ToString();
        }
    }
}