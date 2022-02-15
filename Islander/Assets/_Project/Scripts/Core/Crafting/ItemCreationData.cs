using System;
using Gisha.Islander.Player;
using UnityEngine;

namespace Gisha.Islander.Core.Crafting
{
    [CreateAssetMenu(fileName = "ItemCreationData", menuName = "Scriptable Objects/Item Creation Data", order = 0)]
    public class ItemCreationData : ScriptableObject
    {
        [SerializeField] private Recipe recipe;
        [SerializeField] private GameObject prefab;

        public Recipe Recipe => recipe;
        public GameObject Prefab => prefab;
    }

    [Serializable]
    public class Recipe
    {
        [SerializeField] private ResourceForCreation[] resourcesForCreation;
        public ResourceForCreation[] ResourcesForCreation => resourcesForCreation;
    }

    [Serializable]
    public class ResourceForCreation
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int count;

        public int Count => count;

        public ResourceType ResourceType => resourceType;
    }
}