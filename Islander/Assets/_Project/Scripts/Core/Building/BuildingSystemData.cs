using UnityEngine;

namespace Gisha.Islander.Core.Building
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Building/Building System Data",
        order = 0)]
    public class BuildingSystemData : ScriptableObject
    {
        [SerializeField] private ItemCreationData[] raftsCreationData;

        public ItemCreationData[] RaftsCreationData => raftsCreationData;
    }
}