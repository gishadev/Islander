using System;
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

        private void Awake()
        {
            _craftingController = FindObjectOfType<CraftingController>();
        }

        private void Start()
        {
            foreach (var recipe in _craftingController.ItemRecipes)
                CreateRecipeElement(recipe);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                ChangeCraftPanelVisibility(!craftPanel.activeSelf);
        }

        public void ChangeCraftPanelVisibility(bool isVisible)
        {
            craftPanel.SetActive(isVisible);
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
        }
    }
}