using Gisha.Islander.Player;
using TMPro;
using UnityEngine;

namespace Gisha.Islander.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private TMP_Text woodCountText, stoneCountText, ironCountText, titaniumCountText;
        [SerializeField] private Transform healthBar;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateResourcesCount()
        {
            woodCountText.text = InventoryManager.My.WoodCount.ToString();
            stoneCountText.text = InventoryManager.My.StoneCount.ToString();
            ironCountText.text = InventoryManager.My.IronCount.ToString();
            titaniumCountText.text = InventoryManager.My.TitaniumCount.ToString();
        }

        public void UpdateHealthBar(float health, float maxHealth)
        {
            var xScale = health / maxHealth;
            if (xScale < 0)
                xScale = 0f;
            
            healthBar.localScale = new Vector3(xScale, 1f, 1f);
        }
    }
}