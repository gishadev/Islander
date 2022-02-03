using System;
using System.Collections.Generic;
using Gisha.Islander.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Islander.Core.Crafting
{
    public class CraftingGUI : MonoBehaviour
    {
        [SerializeField] private GameObject craftPanel;
        [Space] [SerializeField] private GameObject recipeElementPrefab;
        [SerializeField] private Transform recipesParent;

        private CraftingController _craftingController;
        private List<GameObject> _craftingGUIElements = new List<GameObject>();

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
        }

        private void OnDisable()
        {
            _craftingController.Crafted -= UpdateCraftingGUI;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                ChangeCraftPanelVisibility(!craftPanel.activeSelf);
        }

        public void UpdateCraftingGUI()
        {
            if (_craftingGUIElements.Count > 0)
            {
                foreach (var guiElement in _craftingGUIElements)
                    Destroy(guiElement);
            }

            foreach (var recipe in _craftingController.ItemsCraftData)
                CreateRecipeElement(recipe);
        }

        private void CreateRecipeElement(ItemCraftData craftData)
        {
            var recipeGO = Instantiate(recipeElementPrefab, recipesParent);

            string costText = "";
            foreach (var resourceForCraft in craftData.Recipe.ResourcesForCraft)
                costText += $"{resourceForCraft.Count} {resourceForCraft.ResourceType}, \n";

            // Changing UI Text.
            recipeGO.transform.Find("Name").GetComponent<TMP_Text>().text = craftData.name;
            recipeGO.transform.Find("Cost").GetComponent<TMP_Text>().text = costText;

            // Updating listeners of the button.
            var craftButton = recipeGO.transform.Find("Btn").GetComponent<Button>();
            craftButton.onClick.AddListener(
                () => _craftingController.Craft(craftData, FindObjectOfType<PlayerController>()));

            _craftingGUIElements.Add(recipeGO);
        }

        public void ChangeCraftPanelVisibility(bool isVisible)
        {
            craftPanel.SetActive(isVisible);
        }
    }
}