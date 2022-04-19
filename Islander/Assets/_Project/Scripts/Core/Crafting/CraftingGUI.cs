using System.Collections.Generic;
using Gisha.Islander.Photon;
using Gisha.Islander.Player;
using Gisha.Islander.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.Core.Crafting
{
    public class CraftingGUI : GUIController
    {
        [SerializeField] private GameObject craftPanel;
        [Space] [SerializeField] private GameObject recipeElementPrefab;
        [SerializeField] private Transform recipesParent;

        private CraftingController _craftingController;
        private List<GameObject> _craftingGUIElements = new List<GameObject>();

        private bool _isInitialized;

        private void Awake()
        {
            _craftingController = FindObjectOfType<CraftingController>();
        }

        private void Start()
        {
            UpdateCraftingGUI();
        }

        private void OnEnable()
        {
            _craftingController.Crafted += UpdateCraftingGUI;
            GUIRaycaster.TotemOpened += ChangeCraftPanelVisibility;
        }

        private void OnDisable()
        {
            _craftingController.Crafted -= UpdateCraftingGUI;
            PhotonManager.MyPhotonPlayer.PlayerRespawned -= UpdateCraftingGUI;
            GUIRaycaster.TotemOpened -= ChangeCraftPanelVisibility;
        }

        public override void ResetGUI()
        {
            UpdateCraftingGUI();
        }
        
        private void UpdateCraftingGUI()
        {
            if (_craftingGUIElements.Count > 0)
            {
                foreach (var guiElement in _craftingGUIElements)
                    Destroy(guiElement);
            }

            foreach (var recipe in _craftingController.ItemsCraftData)
                CreateRecipeElement(recipe);
        }

        private void CreateRecipeElement(ItemCreationData creationData)
        {
            var recipeGO = Instantiate(recipeElementPrefab, recipesParent);

            string costText = "";
            foreach (var resourceForCraft in creationData.Recipe.ResourcesForCreation)
                costText += $"{resourceForCraft.Count} {resourceForCraft.ResourceType}, \n";

            // Changing UI Text.
            recipeGO.transform.Find("Name").GetComponent<TMP_Text>().text = creationData.name;
            recipeGO.transform.Find("Cost").GetComponent<TMP_Text>().text = costText;

            // Updating listeners of the button.
            var craftButton = recipeGO.transform.Find("Btn").GetComponent<Button>();
            craftButton.onClick.AddListener(
                () => _craftingController.Craft(creationData, PhotonManager.MyPhotonPlayer.PlayerController));

            _craftingGUIElements.Add(recipeGO);
        }

        public void ChangeCraftPanelVisibility(bool isVisible)
        {
            if (!_isInitialized)
            {
                PhotonManager.MyPhotonPlayer.PlayerRespawned += ResetGUI;
                _isInitialized = true;
            }

            craftPanel.SetActive(isVisible);
            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}