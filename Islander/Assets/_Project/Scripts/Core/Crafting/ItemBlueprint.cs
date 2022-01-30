using System;
using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Core.Crafting
{
    [CreateAssetMenu(fileName = "ItemBlueprint", menuName = "Scriptable Objects/Crafting/Item Blueprint", order = 0)]
    public class ItemBlueprint : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private CraftRecipe craftRecipe;
    }

    [Serializable]
    public class CraftRecipe
    {
        [SerializeField] private ResourceForCraft[] resourceForCraft;

        [Serializable]
        private class ResourceForCraft
        {
            [SerializeField] private ResourceType resourceType;
            [SerializeField] private int count;
        }
    }
}