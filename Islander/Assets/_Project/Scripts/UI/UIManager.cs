using System;
using Gisha.Islander.Player;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private TMP_Text woodCountText, stoneCountText, ironCountText, titaniumCountText;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateResourcesCount()
        {
            woodCountText.text = InventoryManager.Instance.WoodCount.ToString();
            stoneCountText.text = InventoryManager.Instance.StoneCount.ToString();
            ironCountText.text = InventoryManager.Instance.IronCount.ToString();
            titaniumCountText.text = InventoryManager.Instance.TitaniumCount.ToString();
        }
    }
}