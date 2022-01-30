using System;
using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Core.Crafting
{
    [CreateAssetMenu(fileName = "ItemRecipe", menuName = "Scriptable Objects/Crafting/Item Recipe", order = 0)]
    public class ItemRecipe : ScriptableObject
    {
        [SerializeField] private CraftRecipe craftRecipe;
        [SerializeField] private GameObject prefab;
        
        public CraftRecipe Recipe => craftRecipe;
        public GameObject Prefab => prefab;
    }

    [Serializable]
    public class CraftRecipe
    {
        [SerializeField] private ResourceForCraft[] resourcesForCraft;
        public ResourceForCraft[] ResourcesForCraft => resourcesForCraft;
    }

    [Serializable]
    public class ResourceForCraft
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int count;

        public int Count => count;

        public ResourceType ResourceType => resourceType;
    }
}